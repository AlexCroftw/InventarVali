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
            CreateMap<Autovehicule,AutovehiculeVM>().ReverseMap();
            CreateMap<AutovehiculeVM, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Autovehicule, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Employees, EmployeesVM>().ReverseMap();
            CreateMap<Computer, ComputerVM>().ReverseMap();
            CreateMap<ComputerVM, ComputerDetailsVM>().ReverseMap();
            CreateMap<Computer, ComputerDetailsVM>().ReverseMap();
            CreateMap<AutovehiculeVM, CombinedDataViewModel>().ReverseMap();
            CreateMap<ComputerVM, CombinedDataViewModel>().ReverseMap();
        }
    }
}
