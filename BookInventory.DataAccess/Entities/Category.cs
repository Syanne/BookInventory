using System.ComponentModel.DataAnnotations;

namespace BookInventory.DataAccess.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
