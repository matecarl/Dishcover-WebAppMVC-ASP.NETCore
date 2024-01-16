using System.ComponentModel.DataAnnotations.Schema;

namespace Dishcover.Models.View
{
    [NotMapped]
    public class ManageUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
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
