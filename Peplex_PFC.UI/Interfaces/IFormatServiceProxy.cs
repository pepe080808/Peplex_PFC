using System.Collections.Generic;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IFormatServiceProxy
    {
        bool Insert(FormatUIO entity);
        bool Update(FormatUIO entity);
        bool Delete(int pk);
        FormatUIO Single(int pk);
        IEnumerable<FormatUIO> FindAll();
        int MaxPK();
    }
}