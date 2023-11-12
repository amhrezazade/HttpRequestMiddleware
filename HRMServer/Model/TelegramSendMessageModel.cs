namespace HRMServer.Model
{
    public class TelegramSendMessageModel
    {
        public long chatId { set; get; }
        public string messageText { set; get; }
        public string botToken { set; get; }
        public string Key { set; get; }
    }
}
