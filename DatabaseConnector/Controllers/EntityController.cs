using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnector.DAO;
using DatabaseConnector.DAO.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly LabContext _context;
        public EntityController(LabContext context)
        {
            _context = context;
        }
        [HttpGet("chemicals")]
        public List<Chemical> GetChemicals(int labId)
        {
            return _context.Chemicals.Where(c => c.LabId == labId).ToList();
        }
    }
}