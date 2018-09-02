using Latihan2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hashing = BCrypt.Net.BCrypt;


namespace Latihan2.Services
{
    public class LoginService
    {
        public const string CookieAuthenticationScheme = "MyCookie";

        private readonly StockDbContext Db;

        public LoginService(StockDbContext stockDbContext)
        {
            this.Db = stockDbContext;
        }

        internal async Task<ClaimsPrincipal> Login(string Username, string Password)
        {
            var user = await Db.User.Where(Q => Q.Username == Username).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var validatorPassword = Hashing.Verify(Password, user.Password);

            if (validatorPassword == false)
            {
                return null;
            }

            var claim = new ClaimsIdentity(CookieAuthenticationScheme);

            claim.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));

            var principal = new ClaimsPrincipal(claim);
            return principal;
        }

        internal async Task CreateUser(string username, string password)
        {
            //var response = await Db.User.Where(Q => Q.Username == username).FirstOrDefaultAsync();
            
            //if(response != null)
            //{
            //    return ;
            //}

            var hash = Hashing.HashPassword(password, 12);

            Db.User.Add(new User
            {
                UserId = Guid.NewGuid(),
                Username = username,
                Password = hash
            });
            await Db.SaveChangesAsync();
        }


    }
}
