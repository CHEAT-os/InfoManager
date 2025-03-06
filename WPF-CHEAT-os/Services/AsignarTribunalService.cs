﻿using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPF_CHEAT_os.Models;

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

        public async Task AddAsync(AsignarProfePropuestaModel propuesta)
        {
            var asignarPropuestaDTO = new AsignarPropuestaDTO
            {
                PropuestaId = propuesta.PropuestaId,
                UserId = propuesta.UserId
            };
            try
            {
                if (propuesta == null) return;
                await _httpsJsonClientProvider.PostAsync($"{Constants.PROPUESTA_PATH}/asignarUsuario", asignarPropuestaDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public async Task DeleteAsync(AsignarPropuestaDTO propuesta)
        {   
            await _httpsJsonClientProvider.DeleteAsync($"{Constants.PROPUESTA_PATH}/quitarUsuario",propuesta.Id.ToString());
        }
    }

   
}