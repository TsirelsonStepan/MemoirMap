using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public static class IdentityResultMapper
{
    public static ServiceResult Map(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded) return ServiceResult.Success();
        
        IEnumerable<string> errorCodes = identityResult.Errors.Select(error => error.Code);

        return ServiceResult.Failure(errorCodes);
    }
}