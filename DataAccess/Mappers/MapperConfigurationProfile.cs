using AutoMapper;
using BusinessObject.Models;
using BusinessObject.ViewModels.Category;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.OrderDetail;
using BusinessObject.ViewModels.Product;

namespace DataAccess.Mappers
{
    public class MapperConfigurationProfile : Profile
    {
        public MapperConfigurationProfile() 
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();

            CreateMap<OrderViewModel, Order>()
                .ForMember(x => x.MemberId, y => y.MapFrom(a => a.UserId))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.UserId, y => y.MapFrom(a => a.MemberId))
                .ForMember(x => x.UserName, y => y.MapFrom(a => a.User.UserName));

            CreateMap<OrderDetailViewModel, OrderDetail>().ReverseMap();

            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ProductCreateModel, Product>().ReverseMap();
        }
    }
}
