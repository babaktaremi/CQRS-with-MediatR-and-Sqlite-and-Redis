using MediatR;
using MediatRCqrs.Application.Common;

namespace MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Delete
{
    public class DeletePersonCommand:IRequest<OperationResult>
    {
        public int PersonId { get; set; }
    }
}
