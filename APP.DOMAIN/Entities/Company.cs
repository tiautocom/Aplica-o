using System.ComponentModel;
using APP.DOMAIN.Identity;

namespace APP.DOMAIN
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // [StringLength(50)]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
    }
}