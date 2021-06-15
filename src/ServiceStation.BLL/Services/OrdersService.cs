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
    public class OrdersService : IEntityService<OrderDTO>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Inspector> _inspectorRepository;
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IMapper _mapper;

        public OrdersService(IRepository<Order> conditionRepository, IRepository<Car> carRepository,
            IRepository<Inspector> inspectorRepository, IRepository<Owner> ownerRepository ,IMapper mapper)
        {
            _orderRepository = conditionRepository;
            _carRepository = carRepository;
            _inspectorRepository = inspectorRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> GetItem(int id)
        {
            var item = await _orderRepository.GetById(id);
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            return _mapper.Map<OrderDTO>(item);
        }

        public async Task<IEnumerable<OrderDTO>> GetItems()
        {
            return _mapper.Map<IEnumerable<Order>, List<OrderDTO>>(await _orderRepository.GetAll());
        }

        public async Task Create(OrderDTO item)
        {
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                try
                {
                    await _inspectorRepository.GetById(item.InspectorId);
                    await _carRepository.GetById(item.CarId);
                    await _ownerRepository.GetById(item.OwnerId);
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                await _orderRepository.Add(_mapper.Map<Order>(item));
            }
        }

        public async Task Update(OrderDTO item)
        {
            if (item == null)
            {
                throw new InvalidOperationException(nameof(item));
            }
            else
            {
                try
                {
                    await _inspectorRepository.GetById(item.InspectorId);
                    await _carRepository.GetById(item.CarId);
                    await _ownerRepository.GetById(item.OwnerId);
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                await _orderRepository.Update(_mapper.Map<Order>(item));
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
                await _orderRepository.Delete(_mapper.Map<Order>(item).Id);
            }
        }
    }
}
