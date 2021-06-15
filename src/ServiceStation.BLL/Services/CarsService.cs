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
    public class CarsService : IEntityService<CarDTO>
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IMapper _mapper;

        public CarsService(IRepository<Car> carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        
        public async Task<CarDTO> GetItem(int id)
        {
            var item = await _carRepository.GetById(id);
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            return _mapper.Map<CarDTO>(item);
        }

        public async Task<IEnumerable<CarDTO>> GetItems()
        {
            return _mapper.Map<IEnumerable<Car>, List<CarDTO>>(await _carRepository.GetAll());
        }

        public async Task Create(CarDTO car)
        {
            if (car == null)
            {
                throw new InvalidOperationException(nameof(car));
            }
            else
            {
                await _carRepository.Add(_mapper.Map<Car>(car));
            }
        }

        public async Task Update(CarDTO car)
        {
            if (car == null)
            {
                throw new InvalidOperationException(nameof(car));
            }
            else
            {
                await _carRepository.Update(_mapper.Map<Car>(car));
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
                await _carRepository.Delete(_mapper.Map<Car>(item).Id);
            }
        }
    }
}
