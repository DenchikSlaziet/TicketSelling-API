namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса изменения сотрудника
    /// </summary>
    public class StaffRequest : CreateStaffRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
