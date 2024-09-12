using AutoMapper;
using InventarVali.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models
{
    public class MappingConfig : Profile
    {   
        public MappingConfig()
        {

            CreateMap<Autovehicule, AutovehiculeVM>().ForMember(x => x.InsurenceDate, opt => opt.MapFrom(src => src.InsurenceDate.HasValue? src.InsurenceDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.ITPExpirationDate, opt => opt.MapFrom(src => src.ITPExpirationDate.HasValue ? src.ITPExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.InsuranceExpirationDate, opt => opt.MapFrom(src => src.InsuranceExpirationDate.HasValue ? src.InsuranceExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty)).
                ForMember(x => x.VinietaExpirationDate, opt => opt.MapFrom(src => src.VinietaExpirationDate.HasValue ? src.VinietaExpirationDate.Value.ToString("dd/MM/yyyy") : string.Empty)).ReverseMap();
            CreateMap<AutovehiculeVM, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Autovehicule, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Employees, EmployeesVM>().ReverseMap();
            CreateMap<Computer, ComputerVM>().ReverseMap();
            CreateMap<Autovehicule, CombinedDataViewModel>().ForMember(d => d.FullName, opt => opt.MapFrom(src => src.Employees.FullName));
            CreateMap<Computer, CombinedDataViewModel>().ForMember(d => d.FullName, opt => opt.MapFrom(src => src.Employees.FullName));
        }
    }
}
