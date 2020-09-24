using System;
using System.ComponentModel.DataAnnotations;

namespace MediatRCqrs.Models.Person
{
    public class UpdatePersonViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Job { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
