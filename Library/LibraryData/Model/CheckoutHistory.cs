using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Models
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        //Sets Foreign Key and creates column named Library Aasset Id field
        [Required] public LibraryAsset LibraryAsset { get; set; }
        //Sets Foreign Key and creates column named Library Card Id field
        [Required] public LibraryCard LibraryCard { get; set; }

        [Required] public DateTime CheckedOut { get; set; }
        //Allow Checkin to have a null value
        public DateTime? CheckedIn { get; set; }
    }
}