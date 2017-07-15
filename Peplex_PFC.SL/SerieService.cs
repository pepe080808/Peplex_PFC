using System.Collections.Generic;
using System.Linq;
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
    public class SerieService : ISerieService
    {
        //private readonly ServiceFaultHandling _faultHandling = new ServiceFaultHandling();
        private readonly ISerieManager _manager = CompositionRoot.Instance.Resolve<ISerieManager>();
        private readonly ObjectsMapper<SerieBO, SerieDTO> _mapBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<SerieBO, SerieDTO>();
        private readonly ObjectsMapper<SerieDTO, SerieBO> _mapDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<SerieDTO, SerieBO>();
        private readonly ObjectsMapper<EpisodeBO, EpisodeDTO> _mapEpBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeBO, EpisodeDTO>();
        private readonly ObjectsMapper<EpisodeDTO, EpisodeBO> _mapEpDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeDTO, EpisodeBO>();

        public void Insert(SerieDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Insert(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Update(SerieDTO entity)
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

        public SerieDTO Single(int pk)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var bo = _manager.Single(uow, pk);
                if (bo == null)
                    return null;

                var dto = _mapBO2DTO.Map(bo);

                bo.Episodes.ForEach(b => dto.Episodes.Add(_mapEpBO2DTO.Map(b)));

                return dto;
            }
        }

        public IEnumerable<SerieDTO> FindAll()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var dtos = new List<SerieDTO>();
                var bos = _manager.FindAll(uow);

                bos.ForEach(bo =>
                {
                    dtos.Add(_mapBO2DTO.Map(bo));
                    bo.Episodes.ForEach(b => dtos.Last().Episodes.Add(_mapEpBO2DTO.Map(b)));
                });

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