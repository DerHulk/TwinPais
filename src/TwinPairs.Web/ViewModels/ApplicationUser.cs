using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPairs.Web.ViewModels
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser
    {
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
    }
}
