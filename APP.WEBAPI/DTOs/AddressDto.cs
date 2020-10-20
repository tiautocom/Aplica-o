using APP.DOMAIN;

namespace APP.WEBAPI.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        
        public string AddressName { get; set; } //Lagradouro
        
        public string District { get; set; } //Bairro
   
        public string City { get; set; } //Cidade
     
        public string ZipCode { get; set;} //CEP
        
        public string UF { get; set; } //UF
        public bool IsActive { get; set; }
    }
}