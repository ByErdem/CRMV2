using CRM.Entities.Concrete;

namespace CRM.Entities.Dtos
{
    public class HomeDto
    {
        public User? User { get; set; }
        public string MenuItems { get; set; }
    }
}
