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
    public class FilmService : IFilmService
    {
        //private readonly ServiceFaultHandling _faultHandling = new ServiceFaultHandling();
        private readonly IFilmManager _manager = CompositionRoot.Instance.Resolve<IFilmManager>();
        private readonly ObjectsMapper<FilmBO, FilmDTO> _mapBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<FilmBO, FilmDTO>();
        private readonly ObjectsMapper<FilmDTO, FilmBO> _mapDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<FilmDTO, FilmBO>();

        public void Insert(FilmDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Insert(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Update(FilmDTO entity)
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

        public FilmDTO Single(int pk)
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

        public IEnumerable<FilmDTO> FindAll()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var dtos = new List<FilmDTO>();
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