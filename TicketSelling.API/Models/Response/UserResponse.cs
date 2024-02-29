using TicketSelling.API.Enums;
using TicketSelling.Context.Contracts.Enums;

namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности клиента
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Роль
        /// </summary>
        public RoleResponse Role { get; set; }
    }
}
