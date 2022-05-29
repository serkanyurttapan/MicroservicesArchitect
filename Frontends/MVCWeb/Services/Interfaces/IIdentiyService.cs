using IdentityModel.Client;
using MVCWeb.Models;
using Shared.Dtos;
using System.Threading.Tasks;

namespace MVCWeb.Services.Interfaces
{
   public interface IIdentiyService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
