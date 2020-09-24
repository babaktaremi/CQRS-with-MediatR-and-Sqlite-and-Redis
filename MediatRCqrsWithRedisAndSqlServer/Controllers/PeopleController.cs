using System.Threading.Tasks;
using MediatR;
using MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Create;
using MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Delete;
using MediatRCqrs.Application.PeopleApplication.Model.CommandModels.Update;
using MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetById;
using MediatRCqrs.Application.PeopleApplication.Model.QueryModels.GetPeople;
using MediatRCqrs.Filters;
using MediatRCqrs.Models.Person;
using Microsoft.AspNetCore.Mvc;

namespace MediatRCqrs.Controllers
{
    [ApiController]
    [Route("Api/People")]
    [ApiResponseFilter]
    public class PeopleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PeopleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpOptions]
        public IActionResult GetPeopleOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,DELETE,PUT");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new PersonCreateCommand{Job = model.Job,Name = model.Name,BirthDate = model.BirthDate});

            if (result.IsSuccess)
                return Ok();

            ModelState.AddModelError("",result.ExceptionMessage); //only for development. It's better to show a general message in production

            return BadRequest(ModelState);

        }

        [HttpGet("{personId}", Name = "GetPerson")]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var model = await _mediator.Send(new GetPersonByIdQuery {PersonId = personId});

            if (model.IsSuccess)
            {
                return Ok(model.Result);
            }

            ModelState.AddModelError("", model.ExceptionMessage); //only for development. It's better to show a general message in production

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var model = await _mediator.Send(new GetPeopleQuery());

            if (model.IsSuccess)
                return Ok(model.Result);

            ModelState.AddModelError("", model.ExceptionMessage); //only for development. It's better to show a general message in production

            return BadRequest(ModelState);
        }

        [HttpDelete("{personId}", Name = "DeletePerson")]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            var result = await _mediator.Send(new DeletePersonCommand {PersonId = personId});

            if (result.IsSuccess)
                return NoContent();

            ModelState.AddModelError("", result.ExceptionMessage); //only for development. It's better to show a general message in production

            return BadRequest(ModelState);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> UpdatePerson(int personId, [FromBody] UpdatePersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new PersonUpdateCommand
                {Name = model.Name, Job = model.Job, BirthDate = model.BirthDate, Id = personId});

            if (result.IsSuccess)
               return NoContent();

            ModelState.AddModelError("", result.ExceptionMessage); //only for development. It's better to show a general message in production

            return BadRequest(ModelState);
        }
    }
}
