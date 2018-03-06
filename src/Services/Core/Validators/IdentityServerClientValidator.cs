using IdentityAdmin.Core.Entities;
using IdentityAdmin.Core.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAdmin.Core.Validators
{
    internal class IdentityServerClientValidator : IValidator<IdentityServerClient>
    {
        private readonly IStringLocalizer<IdentityServerClientValidator> _localizer;

        public IdentityServerClientValidator(IStringLocalizer<IdentityServerClientValidator> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public Task<OperationResult> ValidateAsync(IdentityServerClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            List<OperationError> errors = new List<OperationError>();

            Validator.IfNullOrWhiteSpace(client.ClientId, _localizer["Client Id cannot be null"], errors);
            Validator.IfNullOrWhiteSpace(client.ClientName, _localizer["Client Name cannot be null"], errors);
            Validator.IfEmpty(client.AllowedGrantTypes, _localizer["Client Allowed Grant Tyopes cannot be empty"], errors);

            var grantTypesWithRedirectUri = new string[] { "implicit", "hybrid", "authorization_code" };
            if (client.AllowedGrantTypes.Any(grant => grantTypesWithRedirectUri.Contains(grant)) && 
                (client.RedirectUris == null || !client.RedirectUris.Any()))
            {
                errors.Add(new OperationError(_localizer["Client Redirect Uris cannot be empty"]));
            }

            return Task.FromResult(errors.Count > 0 ? OperationResult.Failed(errors.ToArray()) : OperationResult.Success);
        }
    }
}
