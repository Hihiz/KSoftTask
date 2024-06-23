using FluentValidation.Results;
using KSoftTask.Application.Dto.Publishers;
using KSoftTask.Application.Interfaces;
using KSoftTask.Application.Validations.FluentValidations.Publishers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KSoftTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IBaseService<PublisherDto, CreatePublisherDto, UpdatePublisherDto> _publisherService;

        public PublisherController(IBaseService<PublisherDto, CreatePublisherDto, UpdatePublisherDto> publisherService) => (_publisherService) = (publisherService);

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _publisherService.GetAll(cancellationToken));

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken) => Ok(await _publisherService.GetById(id, cancellationToken));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreatePublisher(CreatePublisherDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new CreatePublisherValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _publisherService.Create(dto, cancellationToken));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublisher(int id, UpdatePublisherDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new UpdatePublisherValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _publisherService.Update(id, dto, cancellationToken));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisher(int id, CancellationToken cancellationToken)
        {
            await _publisherService.Delete(id, cancellationToken);

            return Ok(id);
        }
    }
}
