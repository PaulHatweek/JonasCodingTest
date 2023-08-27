using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<CompanyDto, CompanyInfo>().ForMember(d => d.LastModified, opt => opt.Ignore()); 
            CreateMap<EmployeeInfo, EmployeeDto>().ForMember(d => d.LastModifiedDateTime, opt => opt.MapFrom(ss => ss.LastModifiedDateTime.ToString("yyyy-MM-dd HH-mm-ss")));
            CreateMap<EmployeeDto, EmployeeInfo>().ForMember(d => d.LastModifiedDateTime, opt => opt.Ignore());
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}