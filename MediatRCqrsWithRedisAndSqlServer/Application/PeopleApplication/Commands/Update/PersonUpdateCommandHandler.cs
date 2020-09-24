using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRCqrs.Application.Common;
using MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Update;
using MediatRCqrs.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MediatRCqrs.Application.PeopleApplication.Commands.Update
{
    public class PersonUpdateCommandHandler:IRequestHandler<PersonUpdateCommand,OperationResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IDistributedCache _cache;

        public PersonUpdateCommandHandler(ApplicationDbContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }


        public async Task<OperationResult> Handle(PersonUpdateCommand request, CancellationToken cancellationToken)
        {
            var person = await _db.People.FindAsync(request.Id);

            if(person==null)
                return new OperationResult{IsSuccess = false,Exception=new Exception("person Not Found")};

            try
            {
                person.Job = request.Job;
                person.DateOfBirth = request.BirthDate;
                person.Name = request.Name;

                await _db.SaveChangesAsync(cancellationToken);


                var content = JsonSerializer.Serialize(person);
                var cacheContent = Encoding.UTF8.GetBytes(content);
                await _cache.SetAsync($"Person_{person.Id}", cacheContent, token: cancellationToken);
                return new OperationResult{IsSuccess = true};
            }
            catch (Exception e)
            {
               return new OperationResult{IsSuccess = false,Exception = e};
            }
        }
    }
}
