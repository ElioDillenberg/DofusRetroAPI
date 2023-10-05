using DofusRetroAPI.Database;

namespace DofusRetroAPI.Services;

public abstract class ServiceBase
{
    protected readonly DofusRetroDbContext _dbContext;

    protected ServiceBase(DofusRetroDbContext context)
    {
        _dbContext = context;
    }
}