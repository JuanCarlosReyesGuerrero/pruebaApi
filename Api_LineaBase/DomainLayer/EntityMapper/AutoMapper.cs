using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;

namespace DomainLayer.EntityMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Modulo, ModuloDto>().ReverseMap();
            CreateMap<ModuloDto, Modulo>();

            CreateMap<Perfil, PerfilDto>().ReverseMap();
            CreateMap<PerfilDto, Perfil>();

            CreateMap<Permiso, PermisoDto>().ReverseMap();
            CreateMap<PermisoDto, Permiso>();

            CreateMap<TipoDocumento, TipoDocumentoDto>().ReverseMap();
            CreateMap<TipoDocumentoDto, TipoDocumento>();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<ProductoDto, Producto>();
        }
    }
}
