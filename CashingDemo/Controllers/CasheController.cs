using CashingDemo.Data;
using CashingDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CashingDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CasheController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _distributed;
        public CasheController(ApplicationDbContext context, IDistributedCache distributed)
        {
            _context = context;
            _distributed = distributed;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var mycashe = await _distributed.GetStringAsync("demoCash");
            if (mycashe == null)
            {
                var result = await _context.Cashes.ToListAsync();
                mycashe = JsonSerializer.Serialize(result);

                await _distributed.SetStringAsync("demoCash", mycashe, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
            }
            var result2 = JsonSerializer.Deserialize<Cashe[]>(mycashe);

            return Ok(result2);
        }
    }
}
