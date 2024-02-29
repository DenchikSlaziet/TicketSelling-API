namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности зала
    /// </summary>
    public class HallResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер зала
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Кол-во мест в ряду
        /// </summary>
        public int CountPlaceInRow { get; set; }

        /// <summary>
        /// Кол-во рядов
        /// </summary>
        public int CountRow { get; set; }
    }
}
