using Shallik.Professions.Data.Models;
using Shallik.Professions.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shallik.Professions.Business
{
    public class ShallikProfessionsBusiness
    {
        private readonly ShallikProfessionsRepositories _shallikProfessionsRepositories;
        private static readonly List<string> _nameProfessions = new() { "Récolte", "Pêche", "Chasse", "Cuisine", "Alchimie", "Transformation", "Dressage", "Commerce", "Agriculture", "Navigation", "Troc" };

        public ShallikProfessionsBusiness()
        {
            _shallikProfessionsRepositories = new();
        }

        /// <summary>
        /// Retourne les professions les plus élevé de Shallik
        /// </summary>
        /// <returns></returns>
        public async Task<List<Profession>> GetShallikHightProfessions()
        {
            List<Profession> professions = await _shallikProfessionsRepositories.GetShallikProfessions();

            return KeepHightProfession(professions);
        }

        /// <summary>
        /// Permet de retouner la profession la plus élévé dans chaque domaine
        /// </summary>
        /// <param name="professions"></param>
        /// <returns></returns>
        private List<Profession> KeepHightProfession(List<Profession> professions)
        {
            List<Profession> hightProfessions = new();
            List<Profession> professionByName = new();
            Profession hightProfession = null;
            int scoreRankCurrent = 0;
            int hightScoreRank = 0;

            foreach (string nameProfession in _nameProfessions)
            {
                professionByName = professions.FindAll(p => p.Name == nameProfession);
                hightProfession = null;

                foreach (Profession profession in professionByName)
                {
                    scoreRankCurrent = GetScoreRank(profession);

                    if (hightProfession == null)
                    {
                        hightProfession = profession;
                        hightScoreRank = scoreRankCurrent;
                    }
                    else if (hightScoreRank < scoreRankCurrent)
                    {
                        hightProfession = profession;
                        hightScoreRank = scoreRankCurrent;
                    }
                }

                hightProfessions.Add(hightProfession);
            }

            return hightProfessions;
        }

        /// <summary>
        /// Calcul le scoreRank d'une profession par rapport à son rank et son level
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        private int GetScoreRank(Profession profession) =>
            profession.Rank switch
            {
                "Débutants" => 100 + profession.Level,
                "Apprenti" => 200 + profession.Level,
                "Qualifié" => 300 + profession.Level,
                "Professionel" => 400 + profession.Level,
                "Artisan" => 500 + profession.Level,
                "Maître" => 600 + profession.Level,
                "Gourou" => 700 + profession.Level,
                _ => 0,
            };

    }
}
