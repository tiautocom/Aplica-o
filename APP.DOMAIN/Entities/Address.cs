namespace APP.DOMAIN
{
    public class Address
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