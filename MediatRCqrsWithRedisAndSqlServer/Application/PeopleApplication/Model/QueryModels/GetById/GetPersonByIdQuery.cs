using MediatR;
using MediatRCqrs.Application.Common;

namespace MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetById
{
    public class GetPersonByIdQuery:IRequest<OperationResult<GetPersonByIdResult>>
    {
        public int PersonId { get; set; }
    }
}
