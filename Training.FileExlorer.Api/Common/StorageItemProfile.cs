using AutoMapper;
using Training.FileExplorer.Api.Models.DTOs;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Common;

public class StorageItemProfile : Profile
{
    public StorageItemProfile()
    {
        CreateMap<IStorageItemDto, IStorageEntry>().ReverseMap();
    }
}
