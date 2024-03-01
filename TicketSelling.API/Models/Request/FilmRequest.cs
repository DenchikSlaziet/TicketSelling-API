namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса изменения фильма
    /// </summary>
    public class FilmRequest : CreateFilmRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
