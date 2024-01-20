using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dishcover.Models.View
{
    [NotMapped]
    public class ManageUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "EmailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "LockoutEnd")]
        public DateTimeOffset? LockoutEnd { get; set; }

        [Display(Name = "LockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "AccessFailedCount")]
        public int AccessFailedCount { get; set; }

        [Display(Name = "Roles")]
        public IEnumerable<string> Roles { get; set; }

        public string rolesString
        {
            get
            {
                return string.Join(", ", Roles);
            }
        }
    }
}
