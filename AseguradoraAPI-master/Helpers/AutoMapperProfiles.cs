using AseguradoraViamatica.DTOs;
using AseguradoraViamatica.DTOs.Asegurado;
using AseguradoraViamatica.DTOs.Seguro;
using AseguradoraViamatica.Entidades;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AseguradoraViamatica.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Seguro, SeguroDTO>()
                .ForMember(dest => dest.Asegurados, opt => opt.MapFrom(src => src.SegurosAsegurados.Select(sa => sa.Asegurado)))
                .ReverseMap();

            CreateMap<SeguroCreacionDTO, Seguro>()
                .ReverseMap();

            CreateMap<SeguroAseguradosDTO, Seguro>()
                .ReverseMap();

            CreateMap<Asegurado, AseguradoDTO>()
                .ForMember(dest => dest.Seguros, opt => opt.MapFrom(src => src.SegurosAsegurados.Select(sa => sa.Seguro)))
                .ReverseMap();

            CreateMap<AseguradoCreacionDTO, Asegurado>()
                .ForMember(dest => dest.SegurosAsegurados, opt => opt.MapFrom((src, dest) => MapSegurosAsegurados(src, dest)))
                .ReverseMap();

            CreateMap<SegurosAsegurados, SeguroDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Seguro.Id))
                .ForMember(dest => dest.NombreSeguro, opt => opt.MapFrom(src => src.Seguro.NombreSeguro))
                .ForMember(dest => dest.CodigoSeguro, opt => opt.MapFrom(src => src.Seguro.CodigoSeguro))
                .ForMember(dest => dest.SumadaAsegurada, opt => opt.MapFrom(src => src.Seguro.SumadaAsegurada))
                .ForMember(dest => dest.Prima, opt => opt.MapFrom(src => src.Seguro.Prima));
        }

        private List<SegurosAsegurados> MapSegurosAsegurados(AseguradoCreacionDTO aseguradoCreacionDTO, Asegurado asegurado)
        {
            var resultado = new List<SegurosAsegurados>();
            if (aseguradoCreacionDTO.Seguros == null) { return resultado; }
            foreach (var seguroId in aseguradoCreacionDTO.Seguros)
            {
                resultado.Add(new SegurosAsegurados() { SeguroId = seguroId.SeguroId, AseguradoId = asegurado.Id });
            }

            return resultado;
        }
    }
}
