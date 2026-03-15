namespace PTruequeU.Models
{
    public class ChatMessage
    {
        public Guid ChatMessage_Id { get; set; }

        public Guid Thread_Id { get; set; }
        public ChatThread? Thread { get; set; }

        public string Sender_Id { get; set; } = string.Empty;
        public ApplicationUser? Sender { get; set; }

        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}