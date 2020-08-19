using System.Threading.Tasks;
using Store.Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Store.Contracts.Managers;
using Store.Data.EF.Entities;
using Store.Data.Entities.Identity;
using Microsoft.Extensions.Logging;

namespace Store.Data.Database
{
    public class SeedManager : ISeedManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        // private readonly ILogger _logger;
        //  private readonly ConfigurationKeys _configurationKeys;

        public SeedManager(
            IMemoryCache memoryCache,
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
            // ILogger logger
            // IOptions<ConfigurationKeys> configurationKeys
            )
        {
            _memoryCache = memoryCache;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
          // _logger = logger;
          // _configurationKeys = configurationKeys.Value;
        }

        public async Task Run()
        {
            _dbContext.Database.Migrate();

            InitialApplicationIdentitySeed.Run(_dbContext, _userManager, _roleManager
            //, _logger
                );
        }
    }
}
