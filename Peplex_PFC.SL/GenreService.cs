using System;
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
    public class GenreService : IGenreService
    {
        //private readonly ServiceFaultHandling _faultHandling = new ServiceFaultHandling();
        private readonly IGenreManager _manager = CompositionRoot.Instance.Resolve<IGenreManager>();
        private readonly ObjectsMapper<GenreBO, GenreDTO> _mapBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<GenreBO, GenreDTO>();
        private readonly ObjectsMapper<GenreDTO, GenreBO> _mapDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<GenreDTO, GenreBO>();

        public void Insert(GenreDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Insert(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Update(GenreDTO entity)
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

        public GenreDTO Single(int pk)
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

        public IEnumerable<GenreDTO> FindAll()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var dtos = new List<GenreDTO>();
                var bos = _manager.FindAll(uow);

                bos.ForEach(bo =>dtos.Add(_mapBO2DTO.Map(bo)));

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
