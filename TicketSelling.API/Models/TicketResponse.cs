using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.Models
{
    public class TicketResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер зала
        /// </summary>
        public short NumberHall { get; set; }

        /// <summary>
        /// Кол-во мест
        /// </summary>
        public short CountHall { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string FilmName { get; set; } = string.Empty;   

        /// <summary>
        /// Ограничение по возрасту
        /// </summary>
        public short LimitationFilm { get; set; }

        /// <summary>
        /// Название кинотетра
        /// </summary>
        public string CinemaName { get; set; } = string.Empty;

        /// <summary>
        /// Адрес кинотетра
        /// </summary>
        public string CinemaAdress { get; set; } = string.Empty;

        /// <summary>
        /// ФИО Клиента
        /// </summary>
        public string NameClient { get; set; } = string.Empty;

        // <summary>
        /// ФИО Персоны
        /// </summary>
        public string NameStuff { get; set; } = string.Empty;

        /// <summary>
        /// Должность персоны
        /// </summary>
        public string StuffPost { get; set; } = string.Empty;

        /// <summary>
        /// Ряд
        /// </summary>
        public short Row { get; set; }

        /// <summary>
        /// Место
        /// </summary>
        public short Place { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и врремя проведения фильма
        /// </summary>
        public string Date { get; set; } = string.Empty;
    }
}
