using AutoMapper;
using Training.FileExplorer.Api.Models.DTOs;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Common;

public class DriveProfile : Profile
{
    public DriveProfile()
    {
        CreateMap<StorageDriveDto, StorageDrive>().ReverseMap();
    }
}
