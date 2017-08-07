using System.Collections.Generic;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface ISerieServiceProxy
    {
        bool Insert(SerieUIO entity);
        bool Update(SerieUIO entity);
        bool Delete(int pk);
        SerieUIO Single(int pk);
        IEnumerable<SerieUIO> FindAll();
        int MaxPK();
    }
}