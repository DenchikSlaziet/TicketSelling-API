﻿namespace TicketSelling.API.Models
{
    /// <summary>
    /// Модель ответа сущности фильма
    /// </summary>
    public class FilmResponse
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Ограничение по возросту
        /// </summary>
        public short Limitation { get; set; }
    }
}
