using AutoMapper;
using DukandaCore.Application.Tours.Dtos;
using DukandaCore.Domain.Entities;

namespace DukandaCore.Application.Common.Mappings;

public class PackageMappingProfile : Profile
{
    public PackageMappingProfile()
    {
        CreateMap<Package, PackageDto>();
    }
} 