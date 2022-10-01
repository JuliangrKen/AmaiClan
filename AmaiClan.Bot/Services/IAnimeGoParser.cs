using AmaiClan.Bot.Models;

namespace AmaiClan.Bot.Services
{
    /// <summary>
    /// Интерфейс для парсинга сайта https://animego.org/
    /// </summary>
    public interface IAnimeGoParser
    {
        const string SiteUrl = "https://animego.org/";

        /// <summary>
        /// Метод для получения данных со случайной страницы аниме сайта
        /// </summary>
        /// <returns>Модель, инкапсулирующая данные об аниме</returns>
        Task<AnimeGoAnimeInfo> GetRandomAnimeGoAnimeInfoAsync();
    }
}