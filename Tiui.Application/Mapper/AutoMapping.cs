using AutoMapper;
using Tiui.Application.DTOs.Cancelaciones;
using Tiui.Application.DTOs.Clientes;
using Tiui.Application.DTOs.Comun;
using Tiui.Application.DTOs.Guias;
using Tiui.Application.DTOs.Security;
using Tiui.Entities.Cancelaciones;
using Tiui.Entities.Comun;
using Tiui.Entities.Guias;
using Tiui.Entities.Seguridad;

namespace Tiui.Application.Mapper
{
    /// <summary>
    /// Clase auxiliar para la definición de los mapping
    /// </summary>
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Inicializa las configuraciones
        /// </summary>
        public AutoMapping()
        {
            #region Seguridad
            this.CreateMap<Usuario, UsuarioDTO>().ReverseMap();

            this.CreateMap<ConfiguracionCajaTiuiAmigo, ConfiguracionCajaTiuiAmigoDTO>()
                .ForMember(c => c.NombreConfiguracion, opt =>
                 opt.MapFrom(src => $"{src.Largo}x{src.Alto}x{src.Ancho}")).ReverseMap();
            #endregion
            #region Comun
            this.CreateMap<Pais, PaisDTO>().ReverseMap();
            this.CreateMap<Estado, EstadoDTO>().ReverseMap();
            this.CreateMap<Municipio, MunicipioDTO>().ReverseMap();
            this.CreateMap<LibretaDireccion, LibretaDireccionDTO>().ReverseMap();
            this.CreateMap<ConfiguracionApp, ConfiguracionAppDTO>().ReverseMap();
            this.CreateMap<LibretaDireccion, RemitenteDTO>()
            .ForMember(dto => dto.Ciudad, opt => opt.MapFrom(src => src.Municipio.Nombre))
            .ForMember(dto => dto.Estado, opt => opt.MapFrom(src => src.Municipio.Estado.Nombre))
            .ForMember(dto => dto.Pais, opt => opt.MapFrom(src => src.Municipio.Estado.Pais.Nombre));
            this.CreateMap<RemitenteDTO, LibretaDireccion>();
            this.CreateMap<LibretaDireccion, DestinatarioDTO>().ReverseMap();
            this.CreateMap<Estatus, EstatusDTO>().ReverseMap();
            #endregion
            #region Guias
            this.CreateMap<Paqueteria, PaqueteriaDTO>().ReverseMap();
            this.CreateMap<Remitente, RemitenteDTO>().ReverseMap();
            this.CreateMap<Destinatario, DestinatarioDTO>().ReverseMap();
            this.CreateMap<Paquete, PaqueteGuiaDTO>().ReverseMap();
            this.CreateMap<Guia, GuiaCreateDTO>().ReverseMap();
            this.CreateMap<ConfiguracionCajaTiuiAmigo, PaqueteGuiaDTO>().ReverseMap();
            this.CreateMap<NotificacionCliente, NotificacionClienteDTO>().ReverseMap();
            this.CreateMap<NotificacionCliente, NotificacionClienteCreateDTO>().ReverseMap();
            this.CreateMap<Guia, GuiaTrackingDTO>().ForMember(dto => dto.Destinatario, opt =>
             opt.MapFrom(src => src.Destinatario.Nombre)).ForMember(dto => dto.Estatus, opt =>
                opt.MapFrom(src => src.Estatus.Nombre)).ReverseMap();
            this.CreateMap<GuiaFilterDTO, Guia>();
            this.CreateMap<DireccionGuia, RemitenteDTO>();            
            #endregion
            #region Clientes
            this.CreateMap<TiuiAmigo, TiuiAmigoSelectDTO>();
            #endregion
            #region Cancelaciones
            this.CreateMap<MotivoCancelacion, MotivoCancelacionDTO>().ReverseMap();
            #endregion
        }
    }
}
