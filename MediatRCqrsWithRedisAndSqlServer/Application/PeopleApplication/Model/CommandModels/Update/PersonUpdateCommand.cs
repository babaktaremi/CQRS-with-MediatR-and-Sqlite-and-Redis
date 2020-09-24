using System;
using MediatR;
using MediatRCqrs.Application.Common;

namespace MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Update
{
    public class PersonUpdateCommand:IRequest<OperationResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
