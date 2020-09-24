using System;

namespace MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetById
{
    public class GetPersonByIdResult
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
