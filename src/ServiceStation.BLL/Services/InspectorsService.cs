using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ServiceStation.BLL.Models;
using ServiceStation.DAL.Models;
using ServiceStation.DAL.Repository;

namespace ServiceStation.BLL.Services
{
    public class InspectorsService : IEntityService<InspectorDTO>
    {
        private readonly IRepository<Inspector> _inspectorRepository;
        private readonly IMapper _mapper;

        public InspectorsService(IRepository<Inspector> inspectorRepository, IMapper mapper)
        {
            _inspectorRepository = inspectorRepository;
            _mapper = mapper;
        }

        public async Task<InspectorDTO> GetItem(int id)
        {
            var item = await _inspectorRepository.GetById(id);
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            return _mapper.Map<InspectorDTO>(item);
        }

        public async Task<IEnumerable<InspectorDTO>> GetItems()
        {
            return _mapper.Map<IEnumerable<Inspector>, List<InspectorDTO>>(await _inspectorRepository.GetAll());
        }

        public async Task Create(InspectorDTO item)
        {
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                await _inspectorRepository.Add(_mapper.Map<Inspector>(item));
            }
        }

        public async Task Update(InspectorDTO item)
        {
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                await _inspectorRepository.Update(_mapper.Map<Inspector>(item));
            }
        }

        public async Task Delete(int id)
        {
            var item = (await GetItems()).Single(el => el.Id == id);
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                await _inspectorRepository.Delete(_mapper.Map<Inspector>(item).Id);
            }
        }
    }
}
