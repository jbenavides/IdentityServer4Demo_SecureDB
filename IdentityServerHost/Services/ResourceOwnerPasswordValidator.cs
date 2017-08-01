﻿using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityServerHost.Data;

namespace IdentityServerHost.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserRepository userRepository;

        public ResourceOwnerPasswordValidator(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await userRepository.GetByUserName(context.UserName);
            if (user == null)
                return;

            if (user.Password == context.Password.Sha256())
                context.Result = new GrantValidationResult(user.SubjectId.ToString(), "password");
        }
    }
}
