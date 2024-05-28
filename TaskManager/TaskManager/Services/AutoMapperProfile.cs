using AutoMapper;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Models.DTOs;

namespace TaskManager.Services {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<TaskWork, TaskWorkDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserGroup, UserGroupDTO>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, UserDetailsViewModel>().ReverseMap();
            CreateMap<User, EditInfoViewModel>().ReverseMap();
            CreateMap<Group, CreateGroupViewModel>().ReverseMap();
            CreateMap<Group, EditGroupViewModel>().ReverseMap();
        }
    }
}