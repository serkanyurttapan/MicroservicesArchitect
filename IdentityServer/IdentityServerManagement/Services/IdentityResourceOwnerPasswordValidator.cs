using IdentityModel;
using IdentityServer4.Validation;
using IdentityServerManagement.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerManagement.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByIdAsync(context.UserName);
            if (existUser is null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "girdiginiz degerler için sonuc bulunamadi" });
                context.Result.CustomResponse = errors;
                return;
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (passwordCheck is false)
            {
                var errors = new Dictionary<string, object>
                {
                    { "errors", new List<string> { "girdiginiz degerler için sonuc bulunamadi" } }
                };
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

        }
    }
}
