using System.Collections.Generic;
using Peplex_PFC.UI.UIO;

namespace Peplex_PFC.UI.Interfaces
{
    public interface IUserServiceProxy
    {
        bool Insert(UserUIO entity);
        bool Update(UserUIO entity);
        bool Delete(int pk);
        UserUIO Single(int pk);
        IEnumerable<UserUIO> FindAll();
        int MaxPK();
    }
}