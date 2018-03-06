using IdentityAdmin.Core.Validators;
using IdentityServer4.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IdentityAdmin.Core.Tests.Validators
{
    public class IdentityServerClientValidatorTest : IDisposable
    {
        private readonly MockRepository mocks;
        private readonly Mock<IStringLocalizer<IdentityServerClientValidator>> localizer;

        public IdentityServerClientValidatorTest()
        {
            mocks = new MockRepository(MockBehavior.Default);
            localizer = mocks.Create<IStringLocalizer<IdentityServerClientValidator>>();
        }

        public void Dispose()
        {
            mocks.VerifyAll();
        }

        [Fact]
        public async Task ThrowsArgumentNullException()
        {
            var target = new IdentityServerClientValidator(localizer.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(() => target.ValidateAsync(null));
        }

        [Fact]
        public async Task ReturnErrorsWhenNotValid()
        {
            localizer.Setup(l => l["Client Id cannot be null"]).Returns(new LocalizedString(string.Empty, "Client Id cannot be null"));
            localizer.Setup(l => l["Client Name cannot be null"]).Returns(new LocalizedString(string.Empty, "Client Name cannot be null"));
            localizer.Setup(l => l["Client Allowed Grant Tyopes cannot be empty"]).Returns(new LocalizedString(string.Empty, "Client Allowed Grant Tyopes cannot be empty"));

            var target = new IdentityServerClientValidator(localizer.Object);
            OperationResult actual = await target.ValidateAsync(new Entities.IdentityServerClient());
            Assert.False(actual.Succeeded);
            Assert.Equal(3, actual.Errors.Count());
        }

        [Fact]
        public async Task ReturnErrorsWhenNoRedirectUri()
        {
            localizer.Setup(l => l["Client Redirect Uris cannot be empty"]).Returns(new LocalizedString(string.Empty, "Client Redirect Uris cannot be empty"));

            var entity = new Entities.IdentityServerClient
            {
                ClientId = "clientId",
                ClientName = "clientName",
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials
            };

            var target = new IdentityServerClientValidator(localizer.Object);
            OperationResult actual = await target.ValidateAsync(entity);
            Assert.False(actual.Succeeded);
            Assert.Single(actual.Errors);
        }

        [Fact]
        public async Task ReturnSucceded()
        {
            var entity = new Entities.IdentityServerClient
            {
                ClientId = "clientId",
                ClientName = "clientName",
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                RedirectUris = { "http://localhost "}
            };

            var target = new IdentityServerClientValidator(localizer.Object);
            OperationResult actual = await target.ValidateAsync(entity);
            Assert.True(actual.Succeeded);
        }
    }
}
