
using System.ComponentModel.DataAnnotations;
using CardinalProject.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardinalProject.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; } // For edit scenario

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Hospital selection is required")]
        public int HospitalId { get; set; }

        [Required(ErrorMessage = "Role selection is required")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindNever]
        public string? HospitalName { get; set; }
        [BindNever]
        public string? RoleName { get; set; }
        public bool IsActive { get; set; } = true;

        // For dropdowns in the view (populated in controller)
        [BindNever]
        public SelectList? HospitalSelectList { get; set; }
        [BindNever]
        public SelectList? RoleSelectList { get; set; }


    }
}