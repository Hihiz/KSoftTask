using FluentValidation.Results;
using KSoftTask.Application.Dto.Books;
using KSoftTask.Application.Interfaces;
using KSoftTask.Application.Validations.FluentValidations.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KSoftTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBaseService<BookDto, CreateBookDto, UpdateBookDto> _bookService;

        public BookController(IBaseService<BookDto, CreateBookDto, UpdateBookDto> bookService) => (_bookService) = (bookService);

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _bookService.GetAll(cancellationToken));

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken) => Ok(await _bookService.GetById(id, cancellationToken));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateBook(CreateBookDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new CreateBookValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _bookService.Create(dto, cancellationToken));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, UpdateBookDto dto, CancellationToken cancellationToken)
        {
            ValidationResult result = new UpdateBookValidator().Validate(dto);

            if (!result.IsValid)
            {
                return BadRequest(string.Join('\n', result.Errors));
            }

            return Ok(await _bookService.Update(id, dto, cancellationToken));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            await _bookService.Delete(id, cancellationToken);

            return Ok(id);
        }
    }
}
