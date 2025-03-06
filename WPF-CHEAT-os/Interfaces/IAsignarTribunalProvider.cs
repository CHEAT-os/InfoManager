using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Models;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IAsignarTribunalProvider
    {
        Task<AsignarPropuestaDTO> GetByIdAsync(string id);
        Task AddAsync(AsignarProfePropuestaModel propuesta);
        Task DeleteAsync(AsignarPropuestaDTO propuesta);
    }
}