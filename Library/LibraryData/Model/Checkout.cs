using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Models
{
    //Checkout Table for EntityFramework Migration
    public class Checkout
    {
        //Primary Key
        public int Id { get; set; }
        //set Name Property to a required field
        [Required]
        [Display(Name = "Library Asset")]
        public LibraryAsset LibraryAsset { get; set; }

        [Display(Name = "Library Card")] public LibraryCard LibraryCard { get; set; }

        [Display(Name = "Checked Out Since")] public DateTime Since { get; set; }

        [Display(Name = "Checked Out Until")] public DateTime Until { get; set; }
    }
}