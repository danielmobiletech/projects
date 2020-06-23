using System.Collections.Generic;
using System.Linq;
using LibraryData;
using LibraryData.Models;

namespace LibraryService
{
    public class StatusService : IStatusService
    {
        private readonly LibraryContext _context;

        public StatusService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Status newStatus)
        {
            _context.Add(newStatus);
            _context.SaveChanges();
        }

        public Status Get(int id)
        {
            return _context.Statuses.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses;
        }
    }
}