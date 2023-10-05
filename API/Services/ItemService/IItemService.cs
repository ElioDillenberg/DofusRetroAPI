using ClassLibrary.DTOs.Items.CardDto;
using ClassLibrary.DTOs.Items.ItemDto;
using DofusRetroClassLibrary.DTOs.Items.CardDto;
using DofusRetroClassLibrary.DTOs.Localization;

namespace DofusRetroAPI.Services.ItemService;

public interface IItemService
{
    // Create
    // Most items can be added using the same Dto, the unique properties are added through other services
    public Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto addItemDto);
    // Cards are a special case, they have their own Dto as they hold a CardNumber and a CardFamily that can be added through the Dto directly
    public Task<ServiceResponse<GetCardDto>> AddCard(AddCardDto addCardDto);
    
    public Task<ServiceResponse<GetLocalizedStringDto>> AddItemNameDto(AddLocalizedStringDto addItemNameDto);
    public Task<ServiceResponse<GetLocalizedStringDto>> AddItemDescriptionDto(AddLocalizedStringDto addItemDescriptionDto);
    
    // Read
    public Task<ServiceResponse<List<GetItemDto>>> GetAllItems(int language);
    public Task<ServiceResponse<GetItemDto>> GetItemById(int itemId, int language);
}