using System.ComponentModel.DataAnnotations;

namespace LibraryData.Models
{
    public abstract class LibraryAsset
    {
        public int Id { get; set; }

        [Required] public string Title { get; set; }
        [Required] public int Year { get; set; } // Just store as an int for BC
         //Sets Foreign Key and creates column named Status Id field
        [Required] public Status Status { get; set; }

        [Required]
        [Display(Name = "Cost of Replacement")]
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        //Sets Foreign Key and creates column named Location id field and allows it to be lazy loaded
        public virtual LibraryBranch Location { get; set; }
    }
}