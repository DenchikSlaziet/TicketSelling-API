﻿using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ReadServices
{
    public interface ICinemaService
    {
        /// <summary>
        /// Получить список всех <see cref="CinemaModel"/>
        /// </summary>
        Task<IEnumerable<CinemaModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CinemaModel"/> по идентификатору
        /// </summary>
        Task<CinemaModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый кинотетар
        /// </summary>
        Task<CinemaModel> AddAsync(string address, string title, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий кинотеатр
        /// </summary>
        Task<CinemaModel> EditAsync(CinemaModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий кинотетар
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}