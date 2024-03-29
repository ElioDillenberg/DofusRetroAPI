using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.DTOs.Drop;

public record AddDropDto(
    int MonsterId,
    
    int ItemId,
    
    [Range(0,100)]
    float? Rate,
    
    int? DropCap,
    
    [Range(0,800)]
    int ProspectionThreshold
);