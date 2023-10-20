﻿using ClassLibrary.Enums.Languages;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Entities.Localization;

public abstract class BaseLocalizedText
{
    public int Id { get; set; }
    
    public Language Language { get; set; }

    public string Text { get; set; } = string.Empty;
}