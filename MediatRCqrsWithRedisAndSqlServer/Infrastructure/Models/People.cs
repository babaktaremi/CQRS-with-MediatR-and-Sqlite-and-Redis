using System;

namespace MediatRCqrs.Infrastructure.Models
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Job { get; set; }
    }
}
