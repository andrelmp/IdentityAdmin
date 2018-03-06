using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityAdmin.Core.Interfaces
{
    public interface IValidator<in TEntity>
    {
        Task<OperationResult> ValidateAsync(TEntity entity);
    }
}
