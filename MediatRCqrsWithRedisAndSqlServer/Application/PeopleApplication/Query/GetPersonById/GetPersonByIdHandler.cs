using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRCqrs.Application.Common;
using MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetById;
using MediatRCqrs.Infrastructure.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace MediatRCqrs.Application.PeopleApplication.Query.GetPersonById
{
    public class GetPersonByIdHandler:IRequestHandler<GetPersonByIdQuery,OperationResult<GetPersonByIdResult>>
    {
        private readonly IDistributedCache _cache;

        public GetPersonByIdHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<OperationResult<GetPersonByIdResult>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var content = await _cache.GetStringAsync($"Person_{request.PersonId}", cancellationToken);

                if(content==null)
                    return new OperationResult<GetPersonByIdResult>{Exception = new Exception("Person Not Found"),IsSuccess = false};

                var person = JsonSerializer.Deserialize<People>(content);

                return new OperationResult<GetPersonByIdResult>
                {
                    IsSuccess = true,
                    Result = new GetPersonByIdResult{Job = person.Job,BirthDate = person.DateOfBirth,Name = person.Name}
                };
            }
            catch (Exception e)
            {
                return new OperationResult<GetPersonByIdResult>{Exception = e,IsSuccess = false};
            }
        }
    }
}
