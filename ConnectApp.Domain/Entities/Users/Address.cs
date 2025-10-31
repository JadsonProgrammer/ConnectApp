using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    public class Address
    {
        public Guid Id { get; set; } = new Guid();
        public string? Tipo { get; set; }
        public string Endereco { get; set; }
        public string Street { get; set; }         
        public string Number { get; set; }         
        public string? Complement { get; set; }     
        public string City { get; set; }           
        public string State { get; set; }          
        public string ZipCode { get; set; }        
        public string Country { get; set; }        
        public bool IsPrimary { get; set; }        
    }
}
