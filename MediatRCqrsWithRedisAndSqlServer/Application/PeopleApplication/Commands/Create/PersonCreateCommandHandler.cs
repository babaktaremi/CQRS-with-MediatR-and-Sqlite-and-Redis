using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRCqrs.Application.Common;
using MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Create;
using MediatRCqrs.Infrastructure;
using MediatRCqrs.Infrastructure.Models;
using Microsoft.Extensions.Caching.Distributed;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MediatRCqrs.Application.PeopleApplication.Commands.Create
{
    public class PersonCreateCommandHandler:IRequestHandler<PersonCreateCommand,OperationResult<PersonCreateCommandResult>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IDistributedCache _cache;

        public PersonCreateCommandHandler(ApplicationDbContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task<OperationResult<PersonCreateCommandResult>> Handle(PersonCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person=new People
                {
                    DateOfBirth = request.BirthDate,
                    Job = request.Job,
                    Name = request.Name
                };

                await _db.AddAsync(person, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);


                var content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(person));

                await _cache.SetAsync($"Person_{person.Id}", content, token: cancellationToken);
            
                return new OperationResult<PersonCreateCommandResult>
                {
                    Result = new PersonCreateCommandResult{PersonId = person.Id},
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
               return new OperationResult<PersonCreateCommandResult>
               {
                   IsSuccess = false,
                   Exception = e
               };
            }
        }
    }
}
