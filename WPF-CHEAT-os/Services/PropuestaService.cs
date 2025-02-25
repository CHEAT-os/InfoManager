using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;


namespace WPF_CHEAT_os.Services
{
    public class PropuestaService : IPropuestaProvider<PropuestaDTO>
    {
        public async Task<List<PropuestaDTO>?> GetAsync()
        {
            return await HttpJsonClient<List<PropuestaDTO>>.Get(Constants.PROPUESTA_PATH);
        }

        public async Task<PropuestaDTO?> GetByIdAsync(int id)
        {
            return await HttpJsonClient<PropuestaDTO>.Get($"{Constants.PROPUESTA_PATH}/{id}");
        }

        public async Task<PropuestaDTO?> AddAsync(PropuestaDTO propuesta)
        {
            return await HttpJsonClient<PropuestaDTO>.Post(Constants.PROPUESTA_PATH, propuesta);
        }

        public async Task<bool> UpdateAsync(PropuestaDTO propuesta)
        {
            return await HttpJsonClient<bool>.Put($"{Constants.PROPUESTA_PATH}/{propuesta.Id}", propuesta);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await HttpJsonClient<bool>.Delete($"{Constants.PROPUESTA_PATH}/{id}");
        }
    }
}
