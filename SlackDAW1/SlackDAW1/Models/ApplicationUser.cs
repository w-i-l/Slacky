using Microsoft.AspNetCore.Identity;

namespace SlackDAW1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserChannel> UserChannels { get; set; }



    }
}
