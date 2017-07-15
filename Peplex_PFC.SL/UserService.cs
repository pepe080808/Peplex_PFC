using System.Collections.Generic;
using System.ServiceModel;
using EmitMapper;
using Peplex_PFC.BLL.Composition;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;
using Peplex_PFC.SL.InterfacesClasses.Classes.DTO;
using Peplex_PFC.SL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.SL
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : IUserService
    {
        //private readonly ServiceFaultHandling _faultHandling = new ServiceFaultHandling();
        private readonly IUserManager _manager = CompositionRoot.Instance.Resolve<IUserManager>();
        private readonly ObjectsMapper<UserBO, UserDTO> _mapBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<UserBO, UserDTO>();
        private readonly ObjectsMapper<UserDTO, UserBO> _mapDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<UserDTO, UserBO>();

        public void Insert(UserDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Insert(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Update(UserDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Update(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Delete(int pk)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Delete(uow, pk);
                uow.Commit();
            }
        }

        public UserDTO Single(int pk)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var bo = _manager.Single(uow, pk);
                if (bo == null)
                    return null;

                var dto = _mapBO2DTO.Map(bo);
                return dto;
            }
        }

        public IEnumerable<UserDTO> FindAll()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var dtos = new List<UserDTO>();
                var bos = _manager.FindAll(uow);

                bos.ForEach(bo => dtos.Add(_mapBO2DTO.Map(bo)));

                return dtos;
            }
        }

        public int MaxPK()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
                return _manager.MaxPK(uow);
        }
    }
}