namespace BlogApp.Mappings
{
    using AutoMapper;
    using BlogApp.DTOs;
    using BlogApp.Models;

    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            // BlogEntry Mappings
            CreateMap<BlogEntryDTO, BlogEntry>()
                .ForMember(dest => dest.Author, opt => opt.Ignore())  
                .ForMember(dest => dest.BlogEntryCategories, opt => opt.Ignore());

            CreateMap<BlogEntry, BlogEntryDTO>(); 

            // Category Mappings
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.BlogEntryCategories, opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>();

            //  User Mappings
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.BlogEntries, opt => opt.Ignore());

            CreateMap<User, UserDTO>();
        }
    }

}
