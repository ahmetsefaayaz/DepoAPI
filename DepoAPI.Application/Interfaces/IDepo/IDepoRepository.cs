using DepoAPI.Domain.Entities;

namespace DepoAPI.Application.Interfaces.IDepo;

public interface IDepoRepository
{
    Task <Depo>  GetAsync(Guid id);
    Task<List<Depo>> GetAllAsync();
    Task AddAsync(Depo depo);
    Task UpdateAsync(Depo depo);
    Task DeleteAsync(Depo depo);
}