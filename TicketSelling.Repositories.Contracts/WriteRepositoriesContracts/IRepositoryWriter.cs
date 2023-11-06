﻿using System.Diagnostics.CodeAnalysis;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.WriteRepositoriesContracts
{
    public interface IRepositoryWriter<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}