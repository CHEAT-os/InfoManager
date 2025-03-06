using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Models;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IPropuestaProvider
    {
        Task<IEnumerable<PropuestaDTO>> GetAsync();
        Task<PropuestaDTO> GetByIdAsync(string id);
        Task AddAsync(PropuestaDTO propuesta);
        Task UpdateAsync(PropuestaDTO propuesta);
        Task DeleteAsync(string id);
        Task AddRelationTeacher(AsignarProfePropuestaModel propuestaUser);
    }
}
