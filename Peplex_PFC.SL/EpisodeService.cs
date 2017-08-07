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
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeManager _manager = CompositionRoot.Instance.Resolve<IEpisodeManager>();
        private readonly ObjectsMapper<EpisodeBO, EpisodeDTO> _mapBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeBO, EpisodeDTO>();
        private readonly ObjectsMapper<EpisodeDTO, EpisodeBO> _mapDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<EpisodeDTO, EpisodeBO>();

        public void Insert(EpisodeDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Insert(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Update(EpisodeDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Update(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Delete(int serieId, int season, int number)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Delete(uow, serieId, season, number);
                uow.Commit();
            }
        }

        public EpisodeDTO Single(int serieId, int season, int number)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var bo = _manager.Single(uow, serieId, season, number);
                if (bo == null)
                    return null;

                var dto = _mapBO2DTO.Map(bo);
                return dto;
            }
        }

        public IEnumerable<EpisodeDTO> FindAll()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var dtos = new List<EpisodeDTO>();
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