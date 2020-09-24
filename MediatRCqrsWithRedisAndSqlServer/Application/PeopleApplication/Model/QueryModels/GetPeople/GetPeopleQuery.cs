using System.Collections.Generic;
using MediatR;
using MediatRCqrs.Application.Common;

namespace MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetPeople
{
    public class GetPeopleQuery : IRequest<OperationResult<List<GetPeopleResult>>> //mark up class
    {
    }
}
