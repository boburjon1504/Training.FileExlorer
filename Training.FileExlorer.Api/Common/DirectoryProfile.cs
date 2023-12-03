using AutoMapper;
using Training.FileExplorer.Api.Models.DTOs;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Common;

public class DirectoryProfile : Profile
{
    public DirectoryProfile()
    {
        CreateMap<StorageDirectoryDto,StorageDirectory>().ReverseMap();

    }
}
