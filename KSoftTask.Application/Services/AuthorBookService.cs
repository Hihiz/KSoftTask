using AutoMapper;
using KSoftTask.Application.Common.Exceptions;
using KSoftTask.Application.Dto.AuthorBooks;
using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Services
{
    public class AuthorBookService : IBaseService<AuthorBookDto, CreateAuthorBookDto, UpdateAuthorBookDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<AuthorBook> _repository;

        public AuthorBookService(IMapper mapper, IBaseRepository<AuthorBook> repository) => (_mapper, _repository) = (mapper, repository);

        public async Task<List<AuthorBookDto>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                List<AuthorBook> authorBooks = await _repository.GetAll(cancellationToken);
                List<AuthorBookDto> authorBooksDto = _mapper.Map<List<AuthorBookDto>>(authorBooks);

                return authorBooksDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AuthorBookDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                AuthorBook authorBook = await _repository.GetById(id, cancellationToken);

                if (authorBook == null)
                {
                    throw new NotFoundException(nameof(Book), id);
                }

                AuthorBookDto authorBookDto = _mapper.Map<AuthorBookDto>(authorBook);

                return authorBookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AuthorBookDto> Create(CreateAuthorBookDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null)
                {
                    throw new NotFoundException(nameof(AuthorBook), dto);
                }

                AuthorBook authorBook = _mapper.Map<AuthorBook>(dto);

                await _repository.Create(authorBook, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                AuthorBookDto authorBookDto = _mapper.Map<AuthorBookDto>(authorBook);

                return authorBookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AuthorBookDto> Update(int id, UpdateAuthorBookDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id != dto.Id || dto == null)
                {
                    throw new NotFoundException(nameof(Book), dto);
                }

                AuthorBook authorBook = _mapper.Map<AuthorBook>(dto);

                await _repository.Update(authorBook, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                AuthorBookDto authorBookDto = _mapper.Map<AuthorBookDto>(authorBook);

                return authorBookDto;
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
                AuthorBook authorBook = await _repository.GetById(id, cancellationToken);

                if (authorBook == null)
                {
                    throw new NotFoundException(nameof(Book), id);
                }

                await _repository.Delete(authorBook);
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
