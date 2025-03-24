using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Region, RegionDto>().ReverseMap();
			CreateMap<AddRegionRequestDto, Region>()
				// NOTE: Ignore a single property, but it's not needed
				// in this case because it's actually the Region domain
				// that has an Id property
				//.ForMember(i => i.Id, opt => opt.Ignore())
				.ReverseMap();
			CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
			CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<Difficulty, DifficultyDto>().ReverseMap();
			CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
		}
	}
}
