using AutoMapper;
using InventarVali.Models.ViewModel;

namespace InventarVali.Models
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Autovehicule, AutovehiculeVM>().ForMember(x => x.InsurenceDate, opt => opt.MapFrom(src => src.InsurenceDate.HasValue ? src.InsurenceDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.ITPExpirationDate, opt => opt.MapFrom(src => src.ITPExpirationDate.HasValue ? src.ITPExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.InsuranceExpirationDate, opt => opt.MapFrom(src => src.InsuranceExpirationDate.HasValue ? src.InsuranceExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.VinietaExpirationDate, opt => opt.MapFrom(src => src.VinietaExpirationDate.HasValue ? src.VinietaExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.HasITP, opt => opt.MapFrom(src => src.HasITP == true ? "Yes" : "No")). //GetValueorDefault
                ForMember(x => x.HasVinieta, opt => opt.MapFrom(src => src.HasVinieta.HasValue ?(src.HasVinieta.Value? "Yes" : "No"):"n/a")).ReverseMap();
            CreateMap<AutovehiculeVM, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Autovehicule, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Employees, EmployeesVM>().ReverseMap();
            CreateMap<Computer, ComputerVM>().ReverseMap();
            CreateMap<Autovehicule, CombinedDataViewModel>().ForMember(d => d.FullName, opt => opt.MapFrom(src => src.Employees.FullName)).
                ForMember(d => d.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<Computer, CombinedDataViewModel>().ForMember(d => d.FullName, opt => opt.MapFrom(src => src.Employees.FullName)).
                ForMember(d => d.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}
