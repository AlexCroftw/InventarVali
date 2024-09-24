using InventarVali.DataAccess.Data;
using InventarVali.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InventarVali.DataAccess.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        public DBInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }


        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Employee).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = _config.GetSection("DefaultEmailAddress").Value,
                    Email = _config.GetSection("DefaultEmailAddress").Value,

                }, _config.GetSection("DefaultEmailPassword").Value).GetAwaiter().GetResult();

                IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == _config.GetSection("DefaultEmailAddress").Value);
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;
            //migrations if they are not applied

            //create roles if they are not created

            //if roles are not created, create admin role as well

        }
    }
}
