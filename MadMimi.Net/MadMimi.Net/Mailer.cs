using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MadMimi.Net
{
    public class Mailer
    {
        private readonly string _userName;

        private readonly string _apiKey;

        public string TransactionId { get; private set; }

        public string PromotionName { get; set; }

        public string TrackStatus
        {
            get
            {
                return String.Format("https://madmimi.com/mailers/status/{0}?username={1}&api_key={2}", TransactionId, _userName, _apiKey);
            }
        }

        public Mailer(string userName, string apiKey)
        {
            _userName = userName;
            _apiKey = apiKey;
        }

        public async Task Send(MailMessage message)
        {
            await SendAsync(PromotionName, message);
        }

        public async Task SendAsync(string promotionName, MailMessage message, string url = "https://api.madmimi.com/mailer")
        {
            if (_userName == null) throw new ArgumentNullException("userName");
            if (_apiKey == null) throw new ArgumentNullException("apiKey");
            if (promotionName == null && PromotionName == null) throw new ArgumentNullException("promotionName");
            if (message == null || message.To == null || message.From == null || message.Subject == null || message.Body == null) throw new ArgumentNullException("message");

            var param = CreateParam(promotionName, message);
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Add("User-Agent", "MadMimiMailer.Net");
                    request.Content = new FormUrlEncodedContent(param);
                    using (var response = await client.SendAsync(request).ConfigureAwait(false))
                    {
                        var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception(result);
                        TransactionId = result;
                    }
                }
            }
        }

        private Dictionary<string, string> CreateParam(string promotionName, MailMessage message)
        {
            var param = new Dictionary<string, string>
            {
                {"username", _userName},
                {"api_key", _apiKey},
                {"promotion_name", promotionName},
                {"recipients", message.To.ToString()},
                {"subject", message.Subject},
                {"from", message.From.ToString()},
                {"raw_html", message.Body}
            };
            if (message.Bcc.Any()) param.Add("bcc", string.Join(",", message.Bcc.Select(o => o.Address)));
            return param;
        }
    }
}
