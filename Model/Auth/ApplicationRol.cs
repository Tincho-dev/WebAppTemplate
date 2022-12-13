using Microsoft.AspNet.Identity.EntityFramework;

namespace Model
{
    public class ApplicationRol : IdentityRole
    {
        public bool enabled { get; set; }
    }
}
