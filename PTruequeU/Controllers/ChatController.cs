using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Chat;
using PTruequeU.Interfaces;
using PTruequeU.Models;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(IChatService chatService, UserManager<ApplicationUser> userManager)
        {
            _chatService = chatService;
            _userManager = userManager;
        }

        [HttpPost("start/{listingId}")]
        public async Task<ActionResult<ChatRoomResponseDto>> StartChat(int listingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.IsSuspended)
                return Forbid();

            var chatRoom = await _chatService.StartOrGetChatAsync(userId, listingId);
            if (chatRoom == null) return BadRequest("Cannot start chat.");
            return Ok(chatRoom);
        }

        [HttpPost("{chatRoomId}/messages")]
        public async Task<ActionResult<ChatMessageDto>> SendMessage(int chatRoomId, CreateChatMessageDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.IsSuspended)
                return Forbid();

            var message = await _chatService.SendMessageAsync(chatRoomId, userId, dto);
            if (message == null) return BadRequest("Cannot send message.");
            return Ok(message);
        }

        [HttpGet("{chatRoomId}/messages")]
        public async Task<ActionResult<List<ChatMessageDto>>> GetMessages(int chatRoomId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var messages = await _chatService.GetMessagesAsync(chatRoomId, userId);
            return Ok(messages);
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatRoomResponseDto>>> GetMyChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var chats = await _chatService.GetUserChatsAsync(userId);
            return Ok(chats);
        }
    }
}
