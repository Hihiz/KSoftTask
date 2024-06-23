using FluentValidation.Results;
using KSoftTask.Application.Dto.Authors;
using KSoftTask.Application.Interfaces;
using KSoftTask.Application.Validations.FluentValidations.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KSoftTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto> _authorService;

        public AuthorController(IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto> authorService) => (_authorService) = (authorService);

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _authorService.GetAll(cancellationToken));

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken) => Ok(await _authorService.GetById(id, cancellationToken));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateAuthor(CreateAuthorDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new CreateAuthorValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _authorService.Create(dto, cancellationToken));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, UpdateAuthorDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new UpdateAuthorValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _authorService.Update(id, dto, cancellationToken));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            await _authorService.Delete(id, cancellationToken);

            return Ok(id);
        }
    }
}
