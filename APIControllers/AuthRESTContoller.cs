using Dishcover.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dishcover.APIController
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthRESTContoller : ControllerBase
    {
        SignInManager<ApplicationUser> _signInManager;

        public AuthRESTContoller(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Login")]
        [Route("/api/Login")]
        public async Task<ActionResult<Boolean>> Authenticate([FromBody] LoginModel @login)
        {
            var result = await _signInManager.PasswordSignInAsync(@login.Name, @login.Password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        [HttpGet]
        [Route("Logout")]
        [Route("/api/Logout")]
        public async Task<ActionResult<Boolean>> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }

    public class LoginModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
