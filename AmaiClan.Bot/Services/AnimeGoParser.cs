using AmaiClan.Bot.Models;
using System.Text.RegularExpressions;

namespace AmaiClan.Bot.Services
{
    public class AnimeGoParser : IAnimeGoParser
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        private readonly string url;
        private readonly string httpParsePatternForName;
        private readonly string httpParsePatternForImageUrl;

        public AnimeGoParser(HttpClient httpClient, ILogger logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            url = "https://animego.org/anime/random";
            httpParsePatternForName = "<div class=\"anime-title\"><div><h1>(.+?)</h1>";
            httpParsePatternForImageUrl = "srcset=\"(.+?) 2x\" alt=";
        }

        public async Task<AnimeGoAnimeInfo> GetRandomAnimeGoAnimeInfoAsync()
        {
            while (true)
            {
                var respond = await httpClient.GetAsync(url);
                var siteUrl = respond.RequestMessage?.RequestUri?.AbsoluteUri;

                if (respond.IsSuccessStatusCode && siteUrl != null)
                {
                    var content = await respond.Content.ReadAsStringAsync();
                    var animeModel = await CreateAnimeGoAnimeInfo(content, siteUrl);

                    await logger.Log($"Найденное аниме - {respond.RequestMessage?.RequestUri?.AbsoluteUri}");

                    return animeModel;
                }
            }
        }

        private Task<AnimeGoAnimeInfo> CreateAnimeGoAnimeInfo(string httpContent, string siteUrl)
        {
            var nameMatch = Regex.Match(httpContent, httpParsePatternForName);
            var imageUrlMatch = Regex.Match(httpContent, httpParsePatternForImageUrl);

            if (!nameMatch.Success || !imageUrlMatch.Success)
                throw new Exception();

            var name = nameMatch.Groups[1].Value;
            var imageUrl = imageUrlMatch.Groups[1].Value;

            return Task.FromResult(new AnimeGoAnimeInfo()
            {
                Name = name,
                ImageUrl = imageUrl,
                SiteUrl = siteUrl,
            });
        }
    }
}