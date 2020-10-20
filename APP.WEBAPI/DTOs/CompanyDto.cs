using APP.DOMAIN;
using APP.DOMAIN.Identity;

namespace APP.WEBAPI.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
    }
}