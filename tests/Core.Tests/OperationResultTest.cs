using Xunit;

namespace IdentityAdmin.Core.Tests
{
    public class OperationResultTest
    {
        [Fact]
        public void VerifyDefaultConstructor()
        {
            var result = new OperationResult();
            Assert.False(result.Succeeded);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void NullFailedUsesEmptyErrors()
        {
            var result = OperationResult.Failed();
            Assert.False(result.Succeeded);
            Assert.Empty(result.Errors);
        }
    }
}
