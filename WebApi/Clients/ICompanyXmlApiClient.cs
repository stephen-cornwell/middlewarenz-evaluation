using System.Threading;
using System.Threading.Tasks;

namespace MiddlewareNz.Evaluation.WebApi.Clients
{
    public interface ICompanyXmlApiClient
    {
        Task<ApiClientResponse<XmlCompany>> Get(int id, CancellationToken cancellationToken);
    }
}