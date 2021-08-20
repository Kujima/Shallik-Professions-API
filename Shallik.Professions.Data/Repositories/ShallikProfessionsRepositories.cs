using HtmlAgilityPack;
using Shallik.Professions.Data.DataProviders;
using Shallik.Professions.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shallik.Professions.Data.Repositories
{
    public class ShallikProfessionsRepositories
    {
        private readonly ShallikProfessionsDataProvider _shallikProfessionsDataProvider;

        private static readonly List<string> _nameProfessions = new() { "Récolte", "Pêche", "Chasse", "Cuisine", "Alchimie", "Transformation", "Dressage", "Commerce", "Agriculture", "Navigation", "Troc" };

        public ShallikProfessionsRepositories()
        {
            _shallikProfessionsDataProvider = new();
        }

        /// <summary>
        /// Retourne l'ensemble des metiers appartenant à Shallik
        /// </summary>
        /// <returns></returns>
        public async Task<List<Profession>> GetShallikProfessions()
        {
            var html = await _shallikProfessionsDataProvider.GetShallikProfessions();

            var professionsBrut = ScrappingProfessionsRepositories.Instance.ScrappHtmlProfession(html.ToString());

            List<Profession> professions = ParseNodeToClass(professionsBrut);
            
            return professions;
        }

        /// <summary>
        /// Parse le noeud en class Profession
        /// </summary>
        /// <param name="professionsBrut"></param>
        /// <returns></returns>
        private List<Profession> ParseNodeToClass(HtmlNodeCollection professionsBrut)
        {
            List<Profession> professions = new();
            int cptProfession = 0;
            int level = 0;

            foreach (var profession in professionsBrut)
            {
                var professionString = CheckReplaceSpecialCaracters(profession);

                // Level 
                string strLevel = Regex.Match(professionString, @"\d+").Value;
                level = 0;
                int.TryParse(strLevel, out level);

                // Rank 
                string rank = professionString.Remove(professionString.Length - strLevel.Length);

                professions.Add(new() { Name = _nameProfessions[cptProfession], Rank = rank, Level = level });

                if (cptProfession == 10)
                {
                    cptProfession = 0;
                }
                else
                {
                    cptProfession++;
                }
            }

            return professions;
        }

        /// <summary>
        /// Check si le noeud contient des carac spécial puis le renvoie sous forme de string
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        private string CheckReplaceSpecialCaracters(HtmlNode profession)
        {
            string professionString;

            if (profession.InnerText.Contains("&#233;"))
            {
                professionString = profession.InnerText.Replace("&#233;", "é");
            }
            else if (profession.InnerText.Contains("&#238;"))
            {
                professionString = profession.InnerText.Replace("&#238;", "î");
            }
            else
            {
                professionString = profession.InnerText;
            }

            return professionString;
        }


    }
}
