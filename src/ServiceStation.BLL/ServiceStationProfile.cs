using AutoMapper;
using ServiceStation.BLL.Models;
using ServiceStation.DAL.Models;

namespace ServiceStation.BLL
{
    public class ServiceStationProfile : Profile
    {
        public ServiceStationProfile()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Inspector, InspectorDTO>().ReverseMap();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
