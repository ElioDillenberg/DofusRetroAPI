using ClassLibrary.DTOs.Drop;
using ClassLibrary.DTOs.ServiceResponse;

namespace DofusRetroAPI.Services.DropService;

public interface IDropService
{
    // Create
    public Task<ServiceResponse<GetDropDto>> AddDrop(AddDropDto addDropDto);
    // Read
    public Task<ServiceResponse<GetDropDto>> GetDropId(int dropId, int languageId);
}