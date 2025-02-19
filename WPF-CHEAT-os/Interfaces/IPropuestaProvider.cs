using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IPropuestaProvider<T> where T : class
    {
        public Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T propuesta);
        Task<bool> UpdateAsync(T propuesta);
        Task<bool> DeleteAsync(int id);
    }
}
