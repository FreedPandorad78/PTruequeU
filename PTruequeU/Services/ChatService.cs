using Microsoft.EntityFrameworkCore;
using PTruequeU.Data;
using PTruequeU.DTOs.Chat;
using PTruequeU.Interfaces;
using PTruequeU.Models;

namespace PTruequeU.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChatRoomResponseDto?> StartOrGetChatAsync(string buyerId, int listingId)
        {
            var listing = await _context.Listings.FindAsync(listingId);
            if (listing == null) return null;

            // Cannot chat with yourself
            if (listing.UserId == buyerId) return null;

            var chatRoom = await _context.ChatRooms
                .Include(c => c.Listing)
                .Include(c => c.Buyer)
                .Include(c => c.Seller)
                .Include(c => c.Messages)
                    .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(c => c.ListingId == listingId && c.BuyerId == buyerId);

            if (chatRoom == null)
            {
                chatRoom = new ChatRoom
                {
                    ListingId = listingId,
                    BuyerId = buyerId,
                    SellerId = listing.UserId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.ChatRooms.Add(chatRoom);
                await _context.SaveChangesAsync();

                chatRoom = await _context.ChatRooms
                    .Include(c => c.Listing)
                    .Include(c => c.Buyer)
                    .Include(c => c.Seller)
                    .FirstOrDefaultAsync(c => c.Id == chatRoom.Id);
            }

            return MapToDto(chatRoom!);
        }

        public async Task<ChatMessageDto?> SendMessageAsync(int chatRoomId, string senderId, CreateChatMessageDto dto)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(chatRoomId);
            if (chatRoom == null) return null;

            // Only buyer or seller can send messages
            if (chatRoom.BuyerId != senderId && chatRoom.SellerId != senderId) return null;

            var message = new ChatMessage
            {
                ChatRoomId = chatRoomId,
                SenderId = senderId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            var sender = await _context.Users.FindAsync(senderId);

            return new ChatMessageDto
            {
                Id = message.Id,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead,
                SenderId = message.SenderId,
                SenderName = sender?.FullName ?? string.Empty
            };
        }

        public async Task<List<ChatMessageDto>> GetMessagesAsync(int chatRoomId, string userId)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(chatRoomId);
            if (chatRoom == null || (chatRoom.BuyerId != userId && chatRoom.SellerId != userId))
                return new List<ChatMessageDto>();

            return await _context.ChatMessages
                .Include(m => m.Sender)
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .Select(m => new ChatMessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SentAt = m.SentAt,
                    IsRead = m.IsRead,
                    SenderId = m.SenderId,
                    SenderName = m.Sender.FullName
                })
                .ToListAsync();
        }

        public async Task<List<ChatRoomResponseDto>> GetUserChatsAsync(string userId)
        {
            var chatRooms = await _context.ChatRooms
                .Include(c => c.Listing)
                .Include(c => c.Buyer)
                .Include(c => c.Seller)
                .Include(c => c.Messages)
                    .ThenInclude(m => m.Sender)
                .Where(c => c.BuyerId == userId || c.SellerId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return chatRooms.Select(MapToDto).ToList();
        }

        private static ChatRoomResponseDto MapToDto(ChatRoom chatRoom)
        {
            var lastMessage = chatRoom.Messages?.OrderByDescending(m => m.SentAt).FirstOrDefault();

            return new ChatRoomResponseDto
            {
                Id = chatRoom.Id,
                ListingId = chatRoom.ListingId,
                ListingTitle = chatRoom.Listing?.Title ?? string.Empty,
                BuyerId = chatRoom.BuyerId,
                BuyerName = chatRoom.Buyer?.FullName ?? string.Empty,
                SellerId = chatRoom.SellerId,
                SellerName = chatRoom.Seller?.FullName ?? string.Empty,
                CreatedAt = chatRoom.CreatedAt,
                LastMessage = lastMessage != null ? new ChatMessageDto
                {
                    Id = lastMessage.Id,
                    Content = lastMessage.Content,
                    SentAt = lastMessage.SentAt,
                    IsRead = lastMessage.IsRead,
                    SenderId = lastMessage.SenderId,
                    SenderName = lastMessage.Sender?.FullName ?? string.Empty
                } : null
            };
        }
    }
}
