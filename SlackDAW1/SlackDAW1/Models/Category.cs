using SlackDAW1.Models;
using System.ComponentModel.DataAnnotations;

namespace SlackDAW1.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters")]
        [MinLength(10, ErrorMessage = "Category name must be at least 10 characters")]
        public String CategoryName { get; set; }

        /*public virtual ICollection<Channel> Channels { get; set; }*/

    }
}
