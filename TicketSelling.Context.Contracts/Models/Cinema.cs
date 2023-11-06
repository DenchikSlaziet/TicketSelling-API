﻿using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Кинотеатр
    /// </summary>
    public class Cinema : IEntity, IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; }
    }
}
