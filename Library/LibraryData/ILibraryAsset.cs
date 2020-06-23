using System;
using System.Collections.Generic;
using System.Text;
using LibraryData.Models;
namespace LibraryData
{
    public interface ILibraryAsset
    {
       IEnumerable<LibraryAsset> GetAll();
       LibraryAsset GetById(int id);
        void Add(LibraryAsset NewAsset);
        string GetAuthorOrDirector(int Id);
        string GetDeweyIndex(int Id);
        string GetType(int Id);
        string GetTitle(int Id);
        string GetIsbn(int Id);
        LibraryBranch GetLibraryBranchLocation(int Id);
        LibraryBranch GetCurrentLocation(int id);

    }
}
