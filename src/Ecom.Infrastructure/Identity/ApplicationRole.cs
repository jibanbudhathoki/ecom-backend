using Microsoft.AspNetCore.Identity;

namespace Ecom.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
        public bool IsSystemRole { get; set; } = false;
        
        // JSON representation of permissions (e.g. { "Orders": ["Read", "Update"] })
        public string PermissionsJson { get; set; } = "{}";
    }
}
