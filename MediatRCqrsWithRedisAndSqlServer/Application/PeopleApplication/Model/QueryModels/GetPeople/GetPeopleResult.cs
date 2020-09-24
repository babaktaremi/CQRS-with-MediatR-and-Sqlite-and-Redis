using System;

namespace MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetPeople
{
    public class GetPeopleResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
