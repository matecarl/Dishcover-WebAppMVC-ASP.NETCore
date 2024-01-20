using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Dishcover.Models;
using Microsoft.AspNetCore.Identity;

namespace Dishcover.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Display(Name = "FirstName")]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Display(Name = "LastName")]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    public ICollection<Recipe> Recipes { get; }

    public ICollection<RecipeCollection> Collections { get; }
}

