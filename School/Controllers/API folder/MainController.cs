using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Data;

namespace School.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        public readonly string _connectionString;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MainController> _logger;
        private readonly ApplicationDbContext _schoolContext;

        protected MainController(IConfiguration configuration, ApplicationDbContext dbContext, ILogger<MainController> logger)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
            _dbContext = dbContext;
            _logger = logger;
        }
    }
}
