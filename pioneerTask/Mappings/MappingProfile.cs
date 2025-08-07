using AutoMapper;
using pioneerTask.Enums;
using pioneerTask.Models;
using pioneerTask.ViewModels;

namespace pioneerTask.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
              .ForMember(dest => dest.Properties,
                  opt => opt.MapFrom(src => src.PropertyValues));

           
            CreateMap<EmployeePropertyValue, EmployeePropertyValueViewModel>()
                .ForMember(dest => dest.PropertyName,
                    opt => opt.MapFrom(src => src.PropertyDefinition.Name))
                .ForMember(dest => dest.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyDefinition.Type))
                .ForMember(dest => dest.IsRequired,
                    opt => opt.MapFrom(src => src.PropertyDefinition.IsRequired))
                .ForMember(dest => dest.Options,
                    opt => opt.MapFrom(src =>
                        src.PropertyDefinition.Type == PropertyType.Dropdown
                            ? src.PropertyDefinition.DropdownOptions.Select(o => o.Value).ToList()
                            : null));

            
            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest => dest.PropertyValues,
                    opt => opt.MapFrom(src => src.Properties));

            CreateMap<EmployeePropertyValueViewModel, EmployeePropertyValue>()
                .ForMember(dest => dest.PropertyDefinition,
                    opt => opt.Ignore());

            CreateMap<EmployeePropertyDefinition, PropertyDefinitionViewModel>()
               .ForMember(dest => dest.Options,
                   opt => opt.MapFrom(src =>
                       src.Type == PropertyType.Dropdown && src.DropdownOptions != null
                           ? src.DropdownOptions.Select(o => o.Value).ToList()
                           : null))
               .ReverseMap()
               .ForMember(dest => dest.DropdownOptions,
                   opt => opt.MapFrom(src =>
                       src.Type == PropertyType.Dropdown && src.Options != null
                           ? src.Options.Select(o => new DropdownOption { Value = o }).ToList()
                           : new List<DropdownOption>()));

           
            CreateMap<EmployeePropertyValue, EmployeePropertyValueViewModel>()
                .ForMember(dest => dest.PropertyName,
                    opt => opt.MapFrom(src => src.PropertyDefinition.Name))
                .ForMember(dest => dest.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyDefinition.Type))
                .ForMember(dest => dest.IsRequired,
                    opt => opt.MapFrom(src => src.PropertyDefinition.IsRequired))
                .ForMember(dest => dest.Options,
                    opt => opt.MapFrom(src =>
                        src.PropertyDefinition.Type == PropertyType.Dropdown &&
                        src.PropertyDefinition.DropdownOptions != null
                            ? src.PropertyDefinition.DropdownOptions.Select(o => o.Value).ToList()
                            : null))
                .ReverseMap()
                .ForMember(dest => dest.PropertyDefinition,
                    opt => opt.Ignore());


        }
    }
}
