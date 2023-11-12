using System.Net.Http;

namespace HRMServer.Service
{
    public class TelegramService
    {
        private readonly string _key;
        public TelegramService(IConfiguration configuration)
        {
            _key = configuration.GetSection("TelegramRequestKey").Value;
        }


        public bool CHeckKey(string key)
        {
            if (string.IsNullOrEmpty(key)) 
                return false;
            return key.Equals(_key);
        }

        public async Task<bool> SendMessageAsync(string botToken, long chatId, string text)
        {

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://api.telegram.org/bot{botToken}/sendMessage";

                var parameters = new
                {
                    chat_id = chatId,
                    text = text
                };

                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, parameters);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it accordingly
                    return false;
                }
            }
        }

    }
}
