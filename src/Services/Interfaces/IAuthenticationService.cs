using MemoirMap.Models.DTOs;

namespace MemoirMap.Services.Interfaces;

public interface IAuthenticationService
{
    public string Login(LoginRequest loginData);
    public void Register(LoginRequest loginData);
}