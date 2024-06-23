using AutoMapper;
using KSoftTask.Application.Common.Exceptions;
using KSoftTask.Application.Dto.Authors;
using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Services
{
    public class AuthorService : IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Author> _repository;

        public AuthorService(IMapper mapper, IBaseRepository<Author> repository) => (_mapper, _repository) = (mapper, repository);

        public async Task<List<AuthorDto>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                List<Author> authors = await _repository.GetAll(cancellationToken);
                List<AuthorDto> authorsDto = _mapper.Map<List<AuthorDto>>(authors);

                return authorsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                throw;
            }
        }

        public async Task<AuthorDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                Author author = await _repository.GetById(id, cancellationToken);

                if (author == null)
                {
                    throw new NotFoundException(nameof(Author), id);
                }

                AuthorDto authorDto = _mapper.Map<AuthorDto>(author);

                return authorDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AuthorDto> Create(CreateAuthorDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null)
                {
                    throw new NotFoundException(nameof(Author), dto);
                }

                Author author = _mapper.Map<Author>(dto);

                await _repository.Create(author, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                AuthorDto authorDto = _mapper.Map<AuthorDto>(author);

                return authorDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AuthorDto> Update(int id, UpdateAuthorDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null || dto.Id != id)
                {
                    throw new NotFoundException(nameof(Author), id);
                }

                Author author = _mapper.Map<Author>(dto);

                await _repository.Update(author, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                AuthorDto authorDto = _mapper.Map<AuthorDto>(author);

                return authorDto;
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
                Author author = await _repository.GetById(id, cancellationToken);

                if (author == null)
                {
                    throw new NotFoundException(nameof(Author), id);
                }

                await _repository.Delete(author);
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
