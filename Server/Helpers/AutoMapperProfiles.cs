using System;
using AutoMapper;
using Server.DTO;
using Server.Extensions;
using Server.Models;

namespace Server.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserModel, MemberDto>()
            .ForMember(d => d.age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
            .ForMember(d => d.PhotoUrl, o => 
                o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();

        CreateMap<MemberUpdateDto, UserModel>();
    }
}
