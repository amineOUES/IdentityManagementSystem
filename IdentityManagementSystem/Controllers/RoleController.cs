using IdentityManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "Role already exists!" });
            IdentityRole identityRole = new IdentityRole()
            {
                Name = role.Name,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var result = await _roleManager.CreateAsync(identityRole);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "Role creation failed! Please check role details and try again." });
            return Ok(new Response { Status = "Success", Message = "Role Created succesfully" }); 
        }
    }
}
