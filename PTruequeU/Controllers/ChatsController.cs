using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTruequeU.DTOs.Chats;
using PTruequeU.Interfaces;
using System.Security.Claims;

namespace PTruequeU.Controllers
{
    [ApiController]
    [Route("chats")]
    [Authorize]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartChat([FromBody] StartChatRequestDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var thread = await _chatService.StartChat(userId, dto);
                if (thread == null) return NotFound("Listing no encontrado.");
                return Ok(thread);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _chatService.GetMyChats(userId);
            return Ok(result);
        }

        [HttpGet("{id:guid}/messages")]
        public async Task<IActionResult> GetMessages(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _chatService.GetMessages(id, userId);

            if (result == null) return NotFound("Chat no encontrado o no tienes acceso.");
            return Ok(result);
        }

        [HttpPost("{id:guid}/messages")]
        public async Task<IActionResult> SendMessage(Guid id, [FromBody] SendChatMessageRequestDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                var result = await _chatService.SendMessage(id, userId, dto);
                if (result == null) return NotFound("Chat no encontrado o no tienes acceso.");
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}