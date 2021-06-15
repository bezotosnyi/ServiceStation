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
    public class OwnersService : IEntityService<OwnerDTO>
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IMapper _mapper;

        public OwnersService(IRepository<Owner> ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OwnerDTO> GetItem(int id)
        {
            var item = await _ownerRepository.GetById(id);
            if(item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            return _mapper.Map<OwnerDTO>(item);
        }

        public async Task<IEnumerable<OwnerDTO>> GetItems()
        {
            return _mapper.Map<IEnumerable<Owner>, List<OwnerDTO>>(await _ownerRepository.GetAll());
        }

        public async Task Create(OwnerDTO item)
        {
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                await _ownerRepository.Add(_mapper.Map<Owner>(item));
            }
        }

        public async Task Update(OwnerDTO item)
        {
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                await _ownerRepository.Update(_mapper.Map<Owner>(item));
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
                await _ownerRepository.Delete(_mapper.Map<Owner>(item).Id);
            }
        }
    }
}
