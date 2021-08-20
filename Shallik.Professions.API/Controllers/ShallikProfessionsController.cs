using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shallik.Professions.Business;
using Shallik.Professions.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shallik.Professions.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShallikProfessionsController : ControllerBase
    {
        private readonly ShallikProfessionsBusiness _shallikProfessionsBusiness; 

        public ShallikProfessionsController()
        {
            _shallikProfessionsBusiness = new();
        }

        [HttpGet]
        public async Task<ActionResult<List<Profession>>> GetShallikHightProfessions()
        {
            List<Profession> professions = await _shallikProfessionsBusiness.GetShallikHightProfessions();

            return professions;
        }
    }
}
