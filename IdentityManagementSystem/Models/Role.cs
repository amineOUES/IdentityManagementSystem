using System.ComponentModel.DataAnnotations;

namespace IdentityManagementSystem.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        public string Name { get; set; }
    }
}
