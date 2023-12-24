using Interfaces.Models;

namespace DTO.Models
{
    public class DTOModelBase : IModel
    {
        public Guid Id { get; set; }
    }
}
