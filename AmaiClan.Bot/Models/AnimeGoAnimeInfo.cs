namespace AmaiClan.Bot.Models
{
    /// <summary>
    /// Модель данных со страницы аниме из https://animego.org/
    /// </summary>
    public class AnimeGoAnimeInfo
    {
        /// <summary>
        /// Название аниме
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Url на страницу сайта
        /// </summary>
        public string? SiteUrl { get; set; }
        /// <summary>
        /// Url на постер 
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}