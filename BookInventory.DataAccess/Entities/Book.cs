using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookInventory.DataAccess.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        public string PublicationYear { get; set; }

        [Required]
        public int Quantity { get; set;}

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
