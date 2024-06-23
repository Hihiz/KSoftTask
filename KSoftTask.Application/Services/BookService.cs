using AutoMapper;
using KSoftTask.Application.Common.Exceptions;
using KSoftTask.Application.Dto.Books;
using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Services
{
    public class BookService : IBaseService<BookDto, CreateBookDto, UpdateBookDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Book> _repository;

        public BookService(IMapper mapper, IBaseRepository<Book> repository) => (_mapper, _repository) = (mapper, repository);

        public async Task<List<BookDto>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                List<Book> books = await _repository.GetAll(cancellationToken);
                List<BookDto> booksDto = _mapper.Map<List<BookDto>>(books);

                return booksDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<BookDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                Book book = await _repository.GetById(id, cancellationToken);

                if (book == null)
                {
                    throw new NotFoundException(nameof(Book), id);
                }

                BookDto bookDto = _mapper.Map<BookDto>(book);

                return bookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<BookDto> Create(CreateBookDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null)
                {
                    throw new NotFoundException(nameof(Book), dto);
                }

                Book book = _mapper.Map<Book>(dto);

                await _repository.Create(book, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                BookDto bookDto = _mapper.Map<BookDto>(book);

                return bookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<BookDto> Update(int id, UpdateBookDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id != dto.Id || dto == null)
                {
                    throw new NotFoundException(nameof(Book), dto);
                }

                Book book = _mapper.Map<Book>(dto);

                await _repository.Update(book, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                BookDto bookDto = _mapper.Map<BookDto>(book);

                return bookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                Book book = await _repository.GetById(id, cancellationToken);

                if (book == null)
                {
                    throw new NotFoundException(nameof(Book), id);
                }

                await _repository.Delete(book);
                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
