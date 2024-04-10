using System;
using System.ComponentModel.DataAnnotations;

namespace MediaCenter.Pocos
{
    public class users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    
    }
}