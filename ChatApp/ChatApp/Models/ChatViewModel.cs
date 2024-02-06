namespace ChatApp.Models
{
    public class ChatViewModel
    {

        //public ChatViewModel()
        //{
        //    Messages = new List<MessageViewModel>();
        //}

        

        /// <summary>
        /// Current Message
        /// </summary>
        public MessageViewModel CurrentMessage { get; set; } = null!;

        /// <summary>
        /// Messages
        /// </summary>
        
        public List<MessageViewModel> Messages { get; set; } = null!;

        
    }
}
