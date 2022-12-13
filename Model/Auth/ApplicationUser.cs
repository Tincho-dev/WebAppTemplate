using Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Model
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }

        public string Name { get; set; }
        public string LastName { get; set; }

        public async static Task<ClaimsIdentity> CreateUserClaims(
            ClaimsIdentity identity,
            UserManager<ApplicationUser> manager,
            string userId
            )
        {
            var currentuser = await manager.FindByIdAsync(userId);

            var jUser = JsonConvert.SerializeObject(new CurrentUser
            {
                UserId = currentuser.Id,
                Name = currentuser.Name,
                LastName = currentuser.LastName,
                Email = currentuser.Email,
                UserName = currentuser.UserName
            });

            identity.AddClaim(new Claim(ClaimTypes.UserData, jUser));

            return await Task.FromResult(identity);
        }

        public bool Any()
        {
            throw new System.NotImplementedException();
        }
    }
}
