using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.Services
{
    public class UsuarioService : IUsuarioProvider
    {
        private readonly IHttpsJsonClientProvider<UsuarioDTO> _httpsJsonClientProvider;

        public UsuarioService(IHttpsJsonClientProvider<UsuarioDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAsync()
        {
            return await _httpsJsonClientProvider.GetAsync(Constants.USERS_PATH);
        }
    }
}