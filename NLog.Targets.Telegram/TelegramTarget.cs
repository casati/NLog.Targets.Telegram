using NLog.Common;
using NLog.Config;
using System;
using System.Net;
using System.Net.Http;

namespace NLog.Targets.Telegram
{
    [Target("Telegram")]
    public class TelegramTarget : TargetWithLayout
    {
        [RequiredParameter]
        public string BotToken { get; set; } = string.Empty;

        [RequiredParameter]
        public string ChatId { get; set; } = string.Empty;

        public string ParseMode { get; set; } = "DEFAULT";

        public string DisableWebPagePreview { get; set; } = "true";

        protected override void InitializeTarget()
        {
            if (string.IsNullOrWhiteSpace(BotToken))
                throw new ArgumentNullException(nameof(BotToken), "BotToken cannot be empty.");

            if (string.IsNullOrWhiteSpace(ChatId))
                throw new ArgumentNullException(nameof(BotToken), "ChatId cannot be empty.");

            base.InitializeTarget();
        }

        protected override void Write(AsyncLogEventInfo info)
        {
            try
            {                
                var message = Layout.Render(info.LogEvent);                
                var url = $"https://api.telegram.org/bot{BotToken}/sendMessage?chat_id={ChatId}&text={WebUtility.UrlEncode(message)}&parse_mode={ParseMode}&disable_web_page_preview={DisableWebPagePreview}";
                using var client = new HttpClient();
                var result = client.GetAsync(url).Result;

            }
            catch (Exception e)
            {
                info.Continuation(e);
            }
        }
    }
}