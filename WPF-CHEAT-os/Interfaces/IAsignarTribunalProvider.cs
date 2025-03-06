using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IAsignarTribunalProvider
    {
        Task<AsignarPropuestaDTO> GetByIdAsync(string id);
        Task AddAsync(AsignarPropuestaDTO propuesta);
        Task DeleteAsync(AsignarPropuestaDTO propuesta);
    }
}