using AutoMapper;
using KSoftTask.Application.Common.Exceptions;
using KSoftTask.Application.Dto.PublisherBooks;
using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Services
{
    public class PublisherBookService : IBaseService<PublisherBookDto, CreatePublisherBookDto, UpdatePublisherBookDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<PublisherBook> _repository;

        public PublisherBookService(IMapper mapper, IBaseRepository<PublisherBook> repository) => (_mapper, _repository) = (mapper, repository);

        public async Task<List<PublisherBookDto>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                List<PublisherBook> publisherBooks = await _repository.GetAll(cancellationToken);
                List<PublisherBookDto> publisherBooksDto = _mapper.Map<List<PublisherBookDto>>(publisherBooks);

                return publisherBooksDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PublisherBookDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                PublisherBook publisherBook = await _repository.GetById(id, cancellationToken);

                if (publisherBook == null)
                {
                    throw new NotFoundException(nameof(PublisherBook), id);
                }

                PublisherBookDto publisherBookDto = _mapper.Map<PublisherBookDto>(publisherBook);

                return publisherBookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PublisherBookDto> Create(CreatePublisherBookDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null)
                {
                    throw new NotFoundException(nameof(PublisherBook), dto);
                }

                PublisherBook publisherBook = _mapper.Map<PublisherBook>(dto);

                await _repository.Create(publisherBook, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                PublisherBookDto publisherDto = _mapper.Map<PublisherBookDto>(publisherBook);

                return publisherDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            };
        }

        public async Task<PublisherBookDto> Update(int id, UpdatePublisherBookDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null || dto.Id != id)
                {
                    throw new NotFoundException(nameof(PublisherBook), id);
                }

                PublisherBook publisherBook = _mapper.Map<PublisherBook>(dto);

                await _repository.Update(publisherBook, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                PublisherBookDto publisherBookDto = _mapper.Map<PublisherBookDto>(publisherBook);

                return publisherBookDto;
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
                PublisherBook publisherBook = await _repository.GetById(id, cancellationToken);

                if (publisherBook == null)
                {
                    throw new NotFoundException(nameof(PublisherBook), id);
                }

                await _repository.Delete(publisherBook);
                await _repository.SaveChangesAsync(cancellationToken);

                PublisherBookDto publisherBookDto = _mapper.Map<PublisherBookDto>(publisherBook);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
