using MemoirMap.Models;
using MemoirMap.Models.DTOs;

namespace MemoirMap.Services.Interfaces;

public interface IAuthenticationService
{
    public Task<ServiceResult<string>> SignIn(string username, string password);
    public Task<ServiceResult> Register(string username, string password);
}