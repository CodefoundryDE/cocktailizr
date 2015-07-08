using System.ServiceModel;
using System.Threading.Tasks;
using CocktailizrTypes.Model.Message;

namespace Cocktailizr.Service
{
    [ServiceContract]
    public interface ITransferService
    {
        [OperationContract]
        Task<RemoteFileInfo> DownloadFile(DownloadRequest request);

        [OperationContract]
        Task UploadFile(RemoteFileInfo request);
    }
}