namespace Blog.Application.Mappings
{
    using AutoMapper;
    using Blog.Application.DTOs;
    using Blog.Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
                .ForMember(dest => dest.AuthorProfilePictureUrl, opt => opt.MapFrom(src => src.Author.ProfilePictureUrl));

            CreateMap<PostDto, Post>();

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>$"{src.User.FirstName} {src.User.LastName}"));
        }
    }
}
