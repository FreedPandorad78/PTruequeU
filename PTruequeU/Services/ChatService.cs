using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Chats;
using PTruequeU.Interfaces;
using PTruequeU.Models;

namespace PTruequeU.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ChatThreadResponseDto?> StartChat(string currentUserId, StartChatRequestDto dto)
        {
            var listing = await _context.Listings
                .FirstOrDefaultAsync(l => l.Listing_id == dto.ListingId);

            if (listing == null)
            {
                return null;
            }

            var sellerId = listing.User_Id;

            if (sellerId == currentUserId)
            {
                throw new InvalidOperationException("No puedes iniciar un chat sobre tu propia publicación.");
            }

            var existingThread = await _context.ChatThreads
                .Include(t => t.Listing)
                .Include(t => t.Messages)
                .FirstOrDefaultAsync(t =>
                    t.Listing_Id == dto.ListingId &&
                    t.Buyer_Id == currentUserId &&
                    t.Seller_Id == sellerId);

            if (existingThread != null)
            {
                return MapThread(existingThread);
            }

            var thread = new ChatThread
            {
                ChatThread_Id = Guid.NewGuid(),
                Listing_Id = dto.ListingId,
                Buyer_Id = currentUserId,
                Seller_Id = sellerId,
                CreatedAt = DateTime.UtcNow
            };

            _context.ChatThreads.Add(thread);
            await _context.SaveChangesAsync();

            thread.Listing = listing;
            thread.Messages = new List<ChatMessage>();

            return MapThread(thread);
        }

        public async Task<List<ChatThreadResponseDto>> GetMyChats(string currentUserId)
        {
            var threads = await _context.ChatThreads
                .Include(t => t.Listing)
                .Include(t => t.Messages)
                .Where(t => t.Buyer_Id == currentUserId || t.Seller_Id == currentUserId)
                .ToListAsync();

            var orderedThreads = threads
                .OrderByDescending(t => t.Messages.Any()
                    ? t.Messages.Max(m => m.SentAt)
                    : t.CreatedAt)
                .Select(MapThread)
                .ToList();

            return orderedThreads;
        }

        public async Task<List<ChatMessageResponseDto>?> GetMessages(Guid threadId, string currentUserId)
        {
            var thread = await _context.ChatThreads
                .Include(t => t.Messages)
                .FirstOrDefaultAsync(t => t.ChatThread_Id == threadId);

            if (thread == null)
            {
                return null;
            }

            var isParticipant = thread.Buyer_Id == currentUserId || thread.Seller_Id == currentUserId;
            if (!isParticipant)
            {
                return null;
            }

            var messages = thread.Messages
                .OrderBy(m => m.SentAt)
                .Select(MapMessage)
                .ToList();

            return messages;
        }

        public async Task<ChatMessageResponseDto?> SendMessage(Guid threadId, string currentUserId, SendChatMessageRequestDto dto)
        {
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            if (currentUser == null)
            {
                return null;
            }

            if (currentUser.IsSuspended)
            {
                throw new UnauthorizedAccessException("Tu cuenta está suspendida y no puede enviar mensajes.");
            }

            var thread = await _context.ChatThreads
                .FirstOrDefaultAsync(t => t.ChatThread_Id == threadId);

            if (thread == null)
            {
                return null;
            }

            var isParticipant = thread.Buyer_Id == currentUserId || thread.Seller_Id == currentUserId;
            if (!isParticipant)
            {
                return null;
            }

            var cleanText = dto.Text?.Trim();

            if (string.IsNullOrWhiteSpace(cleanText))
            {
                throw new InvalidOperationException("El mensaje no puede estar vacío.");
            }

            var message = new ChatMessage
            {
                ChatMessage_Id = Guid.NewGuid(),
                Thread_Id = threadId,
                Sender_Id = currentUserId,
                Text = cleanText,
                SentAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return MapMessage(message);
        }

        private static ChatThreadResponseDto MapThread(ChatThread thread)
        {
            var lastMessage = thread.Messages?
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefault();

            return new ChatThreadResponseDto
            {
                ChatThreadId = thread.ChatThread_Id,
                ListingId = thread.Listing_Id,
                BuyerId = thread.Buyer_Id,
                SellerId = thread.Seller_Id,
                CreatedAt = thread.CreatedAt,
                ListingTitle = thread.Listing?.Title ?? string.Empty,
                ListingIsHidden = thread.Listing?.IsHidden ?? false,
                LastMessageText = lastMessage?.Text,
                LastMessageSentAt = lastMessage?.SentAt
            };
        }

        private static ChatMessageResponseDto MapMessage(ChatMessage message)
        {
            return new ChatMessageResponseDto
            {
                ChatMessageId = message.ChatMessage_Id,
                ThreadId = message.Thread_Id,
                SenderId = message.Sender_Id,
                Text = message.Text,
                SentAt = message.SentAt
            };
        }
    }
}