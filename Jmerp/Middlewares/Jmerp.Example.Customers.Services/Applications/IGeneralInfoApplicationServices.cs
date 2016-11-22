using Jmerp.Commons;
using Jmerp.Example.Customers.Middlewares.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Middlewares.Applications
{
    public interface IGeneralInfoApplicationServices
    {
        Task<ResponseResult> CreateGeneralInfo(CustomerDto customer, CancellationToken cancellationToken);
        Task<ResponseResult> UpdateGeneralInfo(string customerId, GeneralInfoDto generalInfo, CancellationToken cancellationToken);
        Task<ResponseResult> GetGeneralInfo(string customerId, CancellationToken cancellationToken);
    }
}
