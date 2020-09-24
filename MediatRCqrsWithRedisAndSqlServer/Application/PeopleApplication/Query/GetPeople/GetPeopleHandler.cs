using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRCqrs.Application.Common;
using MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetPeople;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace MediatRCqrs.Application.PeopleApplication.Query.GetPeople
{
    public class GetPeopleHandler : IRequestHandler<GetPeopleQuery, OperationResult<List<GetPeopleResult>>>
    {
        private readonly IDistributedCache _cache;

        public GetPeopleHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<OperationResult<List<GetPeopleResult>>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = new List<GetPeopleResult>();

                var redis = await ConnectionMultiplexer.ConnectAsync("localhost,password=mypass");

                //For Demo Only ! . It's NOT a good idea to fetch all keys and do stuff with their values accordingly. BAD effect on performance

                var keys = redis.GetServer("localhost", 6379).Keys(pattern: "Person_*")
                    .AsQueryable().Select(p => p.ToString()).ToList();

                foreach (var key in keys)
                {
                   result.Add(JsonSerializer.Deserialize<GetPeopleResult>(await _cache.GetStringAsync(key, token: cancellationToken)));
                }

                redis.Dispose();

                if (!result.Any())
                    return new OperationResult<List<GetPeopleResult>> { Exception = new Exception("People Not Found"), IsSuccess = false };


                return new OperationResult<List<GetPeopleResult>> { IsSuccess = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<GetPeopleResult>> { IsSuccess = true, Exception = e };
            }
        }
    }
}
