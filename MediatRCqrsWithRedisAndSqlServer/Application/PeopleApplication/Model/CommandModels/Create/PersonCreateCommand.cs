using System;
using MediatR;
using MediatRCqrs.Application.Common;

namespace MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Create
{
    public class PersonCreateCommand:IRequest<OperationResult<PersonCreateCommandResult>>
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
