using System.Collections.Generic;
using System.Linq;
using LibraryData;
using LibraryData.Models;

namespace LibraryService
{
    public class LibraryCardService : ILibraryCardService
    {
        private readonly LibraryContext _context;

        public LibraryCardService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryCard newLibraryCard)
        {
            _context.Add(newLibraryCard);
            _context.SaveChanges();
        }

        public LibraryCard Get(int cardId)
        {
            return _context.LibraryCards.FirstOrDefault(p => p.Id == cardId);
        }

        public IEnumerable<LibraryCard> GetAll()
        {
            return _context.LibraryCards;
        }
    }
}