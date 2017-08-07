using System.Collections.Generic;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IGenreServiceProxy
    {
        bool Insert(GenreUIO entity);
        bool Update(GenreUIO entity);
        bool Delete(int pk);
        GenreUIO Single(int pk);
        IEnumerable<GenreUIO> FindAll();
        int MaxPK();
    }
}
