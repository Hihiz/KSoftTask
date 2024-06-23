using FluentValidation.Results;
using KSoftTask.Application.Dto.PublisherBooks;
using KSoftTask.Application.Interfaces;
using KSoftTask.Application.Validations.FluentValidations.PublisherBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KSoftTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherBookController : ControllerBase
    {
        private readonly IBaseService<PublisherBookDto, CreatePublisherBookDto, UpdatePublisherBookDto> _publisherBookService;

        public PublisherBookController(IBaseService<PublisherBookDto, CreatePublisherBookDto, UpdatePublisherBookDto> publisherBookService) => (_publisherBookService) = (publisherBookService);

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _publisherBookService.GetAll(cancellationToken));

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken) => Ok(await _publisherBookService.GetById(id, cancellationToken));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreatePublisherBook(CreatePublisherBookDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new CreatePublisherBookValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _publisherBookService.Create(dto, cancellationToken));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublisherBook(int id, UpdatePublisherBookDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new UpdatePublisherBookValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _publisherBookService.Update(id, dto, cancellationToken));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisherBook(int id, CancellationToken cancellationToken)
        {
            await _publisherBookService.Delete(id, cancellationToken);

            return Ok(id);
        }
    }
}
