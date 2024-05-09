using AutoMapper;
using CRUD.Application.Common.Models;
using CRUD.Domain.Entities;

namespace CRUD.Application.Common.Mappers;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}

