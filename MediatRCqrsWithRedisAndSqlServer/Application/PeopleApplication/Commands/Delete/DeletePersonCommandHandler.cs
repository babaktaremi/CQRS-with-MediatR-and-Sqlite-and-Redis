using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRCqrs.Application.Common;
using MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Delete;
using MediatRCqrs.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;

namespace MediatRCqrs.Application.PeopleApplication.Commands.Delete
{
    public class DeletePersonCommandHandler:IRequestHandler<DeletePersonCommand,OperationResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IDistributedCache _cache;

        public DeletePersonCommandHandler(ApplicationDbContext db, IDistributedCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task<OperationResult> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _db.People.FindAsync(request.PersonId);

                if(person==null)
                    return new OperationResult{IsSuccess = false,Exception = new Exception("No User Was Found")};

                _db.Remove(person);

                await _db.SaveChangesAsync(cancellationToken);

                await _cache.RemoveAsync($"Person_{person.Id}", cancellationToken);

                return new OperationResult{IsSuccess = true};
            }
            catch (Exception e)
            {
                return new OperationResult{Exception = e,IsSuccess = false};
            }
        }
    }
}
