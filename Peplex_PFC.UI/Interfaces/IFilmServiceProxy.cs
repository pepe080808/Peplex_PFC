using System.Collections.Generic;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IFilmServiceProxy
    {
        bool Insert(FilmUIO entity);
        bool Update(FilmUIO entity);
        bool Delete(int pk);
        FilmUIO Single(int pk);
        IEnumerable<FilmUIO> FindAll();
        int MaxPK();
    }
}