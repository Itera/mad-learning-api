using System.Threading;
using System.Threading.Tasks;

namespace MadLearning.API.Application.Services
{
    public interface IChatMessageService
    {
        Task SendMessage(string message, CancellationToken cancellationToken);
    }
}
