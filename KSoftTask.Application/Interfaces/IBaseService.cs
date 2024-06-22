namespace KSoftTask.Application.Interfaces
{
    public interface IBaseService<T, CreateDto, UpdateDto>
    {
        Task<List<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T> GetById(int id, CancellationToken cancellationToken = default);
        Task<T> Create(CreateDto dto, CancellationToken cancellationToken = default);
        Task<T> Update(int id, UpdateDto dto, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}