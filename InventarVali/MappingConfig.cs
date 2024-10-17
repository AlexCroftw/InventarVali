using AutoMapper;
using InventarVali.Models.ViewModel;

namespace InventarVali.Models
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {

            CreateMap<Autovehicule, AutovehiculeVM>().
                ForMember(x => x.DisplayInsurenceDate, opt => opt.MapFrom(src => src.InsurenceDate.HasValue ? src.InsurenceDate.Value.
                                                                                                        ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy"))).
                ForMember(x => x.DisplayITPExpirationDate, opt => opt.MapFrom(src => src.ITPExpirationDate.HasValue ? src.ITPExpirationDate.Value.
                                                                                                        ToString("dd/MM/yyyy") : DateTime.Now.AddYears(1).ToString("dd/MM/yyyy"))).
                ForMember(x => x.DisplayInsuranceExpirationDate, opt => opt.MapFrom(src => src.InsuranceExpirationDate.HasValue ? src.InsuranceExpirationDate.Value.
                                                                                                        ToString("dd/MM/yyyy") : DateTime.Now.AddYears(1).ToString("dd/MM/yyyy"))).
                ForMember(x => x.DisplayVinietaExpirationDate, opt => opt.MapFrom(src => src.VinietaExpirationDate.HasValue ? src.VinietaExpirationDate.Value.
                                                                                                        ToString("dd/MM/yyyy") : DateTime.Now.AddYears(1).ToString("dd/MM/yyyy"))).
                ForMember(x => x.DisplayHasITP, opt => opt.MapFrom(src => src.HasITP == true ? "Yes" : "No")). //GetValueorDefault
                ForMember(x => x.DisplayHasVinieta, opt => opt.MapFrom(src => src.HasVinieta == true ? "Yes" : "No"))
                .ReverseMap();


            CreateMap<AutovehiculeVM, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Autovehicule, AutovehiculeDetailsVM>().ReverseMap();
            CreateMap<Employees, EmployeesVM>().ReverseMap();
            CreateMap<Computer, ComputerVM>()
                .ForMember(x => x.Employee, opt => opt.MapFrom(src => src.Employees)).ReverseMap();
            CreateMap<Invoice, InvoiceVM>().ForMember(x => x.DisplayInvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate.HasValue ? src.InvoiceDate.Value.
                                                                                                         ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy"))).ReverseMap();







            //.ForMember(d => d.FullName, opt => opt.MapFrom(src => src.Employees.FullName)).
            //    ForMember(d => d.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}
