using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Sets.SetDto;

namespace DofusRetroAPI.Services.SetService;

public interface ISetService
{
    public Task<ServiceResponse<GetSetDto>> AddSet(AddSetDto addSetDto);
    public Task<ServiceResponse<GetLocalizedStringDto>> AddSetName(AddLocalizedStringDto localizedStringDto);
}