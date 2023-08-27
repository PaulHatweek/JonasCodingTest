using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

namespace BusinessLayer
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<DataEntity, BaseInfo>();
            CreateMap<Company, CompanyInfo>();
            CreateMap<Employee, EmployeeInfo>()
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.Phone))
                .ForMember(d => d.OccupationName, opt => opt.MapFrom(s => s.Occupation))
                .ForMember(d => d.LastModifiedDateTime, opt => opt.MapFrom(s => s.LastModified));
            CreateMap<EmployeeInfo, Employee>()
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Occupation, opt => opt.MapFrom(s => s.OccupationName))
                .ForMember(d => d.LastModified, opt => opt.MapFrom(s => s.LastModifiedDateTime));
            CreateMap<ArSubledger, ArSubledgerInfo>();
        }
    }

}