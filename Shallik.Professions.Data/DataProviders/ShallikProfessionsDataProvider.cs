using Shallik.Professions.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shallik.Professions.Data.DataProviders
{
    public class ShallikProfessionsDataProvider
    {
        /// <summary>
        /// URL pour récupérer la page affichant les professions de shallik
        /// </summary>
        const string URL_BDO_SHALLIK_PROFESSIONS = "https://www.naeu.playblackdesert.com/fr-FR/Adventure/Profile?profileTarget=tbXSK7e39Sb3U3yPi7UDjhrIuQG8ZEIVJgtTRjxEegpL%2bAb9Vc0C1GbApjsVeuQNsP%2bY4fQP2hkyWH5gii2V9a7Qvz8GG4w37hEyr3Veg1Syt1tLJysdbbqkkt8fin8ytLd572gl5ljRVKqW%2fNXj8%2f2gJaipsMDTOMTp1E51URmwc%2bfByVPQLyDnEQ1ErmZi";
        
        private readonly HttpClient _client;

        public ShallikProfessionsDataProvider()
        {
            _client = new();
        }

        /// <summary>
        /// Récupère le page contenant l'ensemble des professions
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetShallikProfessions()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "C# App");

            HttpResponseMessage response = await _client.GetAsync(URL_BDO_SHALLIK_PROFESSIONS);
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
