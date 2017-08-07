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
    public class FormatService : IFormatService
    {
        private readonly IFormatManager _manager = CompositionRoot.Instance.Resolve<IFormatManager>();
        private readonly ObjectsMapper<FormatBO, FormatDTO> _mapBO2DTO = ObjectMapperManager.DefaultInstance.GetMapper<FormatBO, FormatDTO>();
        private readonly ObjectsMapper<FormatDTO, FormatBO> _mapDTO2BO = ObjectMapperManager.DefaultInstance.GetMapper<FormatDTO, FormatBO>();

        public void Insert(FormatDTO entity)
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                _manager.Insert(uow, _mapDTO2BO.Map(entity));
                uow.Commit();
            }
        }

        public void Update(FormatDTO entity)
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

        public FormatDTO Single(int pk)
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

        public IEnumerable<FormatDTO> FindAll()
        {
            using (var uow = CompositionRoot.Instance.Resolve<IUnitOfWork>())
            {
                var dtos = new List<FormatDTO>();
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