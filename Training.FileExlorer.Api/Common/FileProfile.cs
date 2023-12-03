using AutoMapper;
using Training.FileExplorer.Api.Models.DTOs;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Common;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<StorageDirectoryDto,StorageFile>().ReverseMap();
    }
}
