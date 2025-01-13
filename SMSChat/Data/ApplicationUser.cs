using Microsoft.AspNetCore.Identity;

namespace SMSChat.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? SipUserName { get; set; }
        public string? SipPassword { get; set; }
        public string? SipServer { get; set; }
        public string? SipPhoneNumber { get; set; }
    }

}
