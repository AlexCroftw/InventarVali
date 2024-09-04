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
        }
    }
}
