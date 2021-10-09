namespace FleetChatBotUI.ViewModels
{
    public class Message
    {
        /// <summary>
        /// Text to be displayed
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Came from user => true
        /// Came from ChatBot => false
        /// </summary>
        public bool FromUser { get; set; }

        /// <summary>
        /// Time this message has been sent
        /// </summary>
        public System.DateTime Time { get; set; }
    }
}
