using FluentValidation.Results;
using KSoftTask.Application.Dto.AuthorBooks;
using KSoftTask.Application.Interfaces;
using KSoftTask.Application.Validations.FluentValidations.AuthorBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KSoftTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        private readonly IBaseService<AuthorBookDto, CreateAuthorBookDto, UpdateAuthorBookDto> _authorBookService;

        public AuthorBookController(IBaseService<AuthorBookDto, CreateAuthorBookDto, UpdateAuthorBookDto> authorBookService) => (_authorBookService) = (authorBookService);

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _authorBookService.GetAll(cancellationToken));

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken) => Ok(await _authorBookService.GetById(id, cancellationToken));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateAuthorBook(CreateAuthorBookDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new CreateAuthorBookValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _authorBookService.Create(dto, cancellationToken));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorBook(int id, UpdateAuthorBookDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new UpdateAuthorBookValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _authorBookService.Update(id, dto, cancellationToken));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorBook(int id, CancellationToken cancellationToken)
        {
            await _authorBookService.Delete(id, cancellationToken);

            return Ok(id);
        }
    }
}
