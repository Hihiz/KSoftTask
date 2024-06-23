using AutoMapper;
using KSoftTask.Application.Common.Exceptions;
using KSoftTask.Application.Dto.Publishers;
using KSoftTask.Application.Interfaces;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Services
{
    public class PublisherService : IBaseService<PublisherDto, CreatePublisherDto, UpdatePublisherDto>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Publisher> _repository;

        public PublisherService(IMapper mapper, IBaseRepository<Publisher> repository) => (_mapper, _repository) = (mapper, repository);

        public async Task<List<PublisherDto>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                List<Publisher> publishers = await _repository.GetAll(cancellationToken);
                List<PublisherDto> publishersDto = _mapper.Map<List<PublisherDto>>(publishers);

                return publishersDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PublisherDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                Publisher publisher = await _repository.GetById(id, cancellationToken);

                if (publisher == null)
                {
                    throw new NotFoundException(nameof(Publisher), id);
                }

                PublisherDto publisherDto = _mapper.Map<PublisherDto>(publisher);

                return publisherDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PublisherDto> Create(CreatePublisherDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null)
                {
                    throw new NotFoundException(nameof(Publisher), dto);
                }

                Publisher publisher = _mapper.Map<Publisher>(dto);

                await _repository.Create(publisher, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                PublisherDto publisherDto = _mapper.Map<PublisherDto>(publisher);

                return publisherDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PublisherDto> Update(int id, UpdatePublisherDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto == null || dto.Id != id)
                {
                    throw new NotFoundException(nameof(Publisher), id);
                }

                Publisher publisher = _mapper.Map<Publisher>(dto);

                await _repository.Update(publisher, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);

                PublisherDto publisherDto = _mapper.Map<PublisherDto>(publisher);

                return publisherDto;
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
                Publisher publisher = await _repository.GetById(id, cancellationToken);

                if (publisher == null)
                {
                    throw new NotFoundException(nameof(Publisher), id);
                }

                await _repository.Delete(publisher);
                await _repository.SaveChangesAsync(cancellationToken);

                PublisherDto publisherDto = _mapper.Map<PublisherDto>(publisher);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
