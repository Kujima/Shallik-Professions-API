using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shallik.Professions.Data.Repositories
{
    public class ScrappingProfessionsRepositories
    {

        private static ScrappingProfessionsRepositories _instance;
        private readonly HtmlDocument _htmlDoc ;

        public static ScrappingProfessionsRepositories Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }

        private ScrappingProfessionsRepositories() => _htmlDoc = new();

        /// <summary>
        /// Récupère l'ensemble des données que compose la page des professions
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public HtmlNodeCollection ScrappHtmlProfession(string html)
        {
            _htmlDoc.LoadHtml(html);
            var professions = _htmlDoc.DocumentNode.SelectNodes("//span[@class='spec_level']");

            return professions;
        }
    }
}
