using System.Collections.Generic;
using System.Linq;
using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryService
{
    public class LibraryBranchService : ILibraryBranchService
    {
        private readonly LibraryContext _context;

        public LibraryBranchService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryBranch newBranch)
        {
            
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int branchId)
        {
            var Lib = _context.LibraryBranches
                 .Include(b => b.Patrons);
           var lib2 = Lib.Include(b => b.LibraryAssets);

            return lib2.FirstOrDefault(b=>b.Id==branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            var Lib = _context.LibraryBranches
                .Include(b => b.Patrons);
            var lib2 = Lib.Include(b => b.LibraryAssets).ToList();
            return lib2;
        }

        public int GetAssetCount(int branchId)
        {
            return Get(branchId).LibraryAssets.Count();
        }
        // Gets Assets based off the BranchId and returns the first found asset
        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.LibraryBranches.Include(a => a.LibraryAssets)
                .First(b => b.Id == branchId).LibraryAssets;
        }

        public decimal GetAssetsValue(int branchId)
        {
            var assetsValue = GetAssets(branchId).Select(a => a.Cost);
            return assetsValue.Sum();
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours.Where(a => a.Branch.Id == branchId);

            var displayHours =
                DataHelpers.HumanizeBusinessHours(hours);

            return displayHours;
        }

        public int GetPatronCount(int branchId)
        {
            return Get(branchId).Patrons.Count();
        }

        public IEnumerable<Patron> GetPatrons(int branchId)
        {
            return _context.LibraryBranches.Include(a => a.Patrons).First(b => b.Id == branchId).Patrons;
        }

        //TODO: Implement 
        public bool IsBranchOpen(int branchId)
        {
            return true;
        }
    }
}