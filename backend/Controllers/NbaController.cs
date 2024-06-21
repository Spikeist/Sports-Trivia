using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NbaController : ControllerBase
    {
        private readonly NbaContext _context;

        public NbaController(NbaContext context)
        {
            _context = context;
        }

        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }

        [HttpGet("players/{teamId}")]
        public async Task<IActionResult> GetPlayersByTeam(int teamId)
        {
            var players = await _context.Players.Where(p => p.TeamId == teamId).ToListAsync();
            return Ok(players);
        }
    }
}