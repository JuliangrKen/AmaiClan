using System.Text.RegularExpressions;

namespace AmaiClan.Bot.Services
{
    /// <summary>
    /// Класс, реализующий интерфейс для парсинга сайта https://rule34.xyz/
    /// </summary>
    public class Rule34Parser : IRule34Parser
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;
        private readonly Random random;

        private readonly int MinIdImage;
        private readonly int MaxIdImage;

        private readonly string httpParsePattern;

        public Rule34Parser(HttpClient httpClient, ILogger logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            random = new Random();

            MinIdImage = 3_500_000;
            MaxIdImage = 3_505_000;

            httpParsePattern = "class=\"img shadow-base\" src=\"(.+?)\"";
        }

        public async Task<string> GetRandomImageUrlAsync()
        {
            var postId = random.Next(MinIdImage, MaxIdImage);

            while (true)
            {
                var url = $"{IRule34Parser.SiteUrl}post/{postId}";

                var respond = await httpClient.GetAsync(url);
                await logger.Log($"Совершена попытка подключения к {url}");

                if (respond.IsSuccessStatusCode)// пост не найдет проверка
                {
                    await logger.Log($"Подключение к {url} произведено успешно!");
                    return await GenerateCorrectImageUrlAsync(await respond.Content.ReadAsStringAsync());
                }

                await logger.Log($"Подключиться к {url} не удалось!");
                postId = postId <= MaxIdImage ? postId++ : throw new Exception();
            }
        }
        
        private async Task<string> GenerateCorrectImageUrlAsync(string httpContent)
        {
            var match = Regex.Match(httpContent, httpParsePattern);

            if (!match.Success)
                throw new ArgumentException(nameof(match.Success));

            var imageUrl = match.Groups[1].Value;

            if (!imageUrl.Contains("https://rule34.xyz/"))
                imageUrl = "https://rule34.xyz" + imageUrl;

            await logger.Log($"{imageUrl} - {nameof(imageUrl)}");

            return imageUrl;
        }
    }
}