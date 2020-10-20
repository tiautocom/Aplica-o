using System;
using System.Collections.Generic;


namespace APP.WEBAPI.DTOs
{
    public class UserDto
    {
      
         public string Email { get; set; }
         public string Password { get; set; }
         public string FullName { get; set; }
        
    //     public string Email { get; set; }
    //     public string Id { get; set; } = Guid.NewGuid().ToString();
    //     public string Name { get; set; }
    //     public string Email { get; set; }
    //     public bool IsActive { get; set; }
    //     //Lista de Perfis
    //    // public List<UserRole> UserRole { get; set; }
    }
}