using System.ComponentModel.DataAnnotations;

namespace CardinalProject.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
    }
}
