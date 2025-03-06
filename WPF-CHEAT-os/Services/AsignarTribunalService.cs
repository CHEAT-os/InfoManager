using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.Services
{
    public class AsignarTribunalService : IAsignarTribunalProvider
    {
        private readonly IHttpsJsonClientProvider<AsignarPropuestaDTO> _httpsJsonClientProvider;

        public AsignarTribunalService(IHttpsJsonClientProvider<AsignarPropuestaDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<AsignarPropuestaDTO> GetByIdAsync(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync($"{Constants.PROPUESTA_PATH}/{id}/usuarios", id);
        }

        public async Task AddAsync(AsignarPropuestaDTO propuesta)
        {
            try
            {
                if (propuesta == null) return;
                await _httpsJsonClientProvider.PostAsync($"{Constants.PROPUESTA_PATH}/asignarUsuario", propuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task DeleteAsync(AsignarPropuestaDTO propuesta)
        {
            await _httpsJsonClientProvider.DeleteAsync($"{Constants.PROPUESTA_PATH}/quitarUsuario",propuesta.Id);
        }
    }

   
}