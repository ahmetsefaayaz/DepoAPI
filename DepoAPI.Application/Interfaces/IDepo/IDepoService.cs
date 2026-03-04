using DepoAPI.Application.Dtos.DepoDTO;
namespace DepoAPI.Application.Interfaces.IDepo;

public interface IDepoService
{
    Task <GetDepoDTO> GetDepoByIdAsync(Guid id);
    Task <List<GetDepoDTO>> GetAllDeposAsync();
    Task AddDepoAsync(Guid customerId);
    Task UpdateDepoAsync(Guid id, UpdateDepoDTO dto);
    Task DeleteDepoAsync(Guid id);
}