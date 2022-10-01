namespace AmaiClan.Bot.Services
{
    /// <summary>
    /// Интерфейс для парсинга сайта https://rule34.xyz/
    /// </summary>
    public interface IRule34Parser
    {
        /// <summary>
        /// Ссылка на сайт, подвергающийся парсингу
        /// </summary>
        const string SiteUrl = "https://rule34.xyz/";
        /// <summary>
        /// Метод для получения ссылки на существующее случайное изображение
        /// </summary>
        /// <returns>Url изображение</returns>
        Task<string> GetRandomImageUrlAsync();
    }
}