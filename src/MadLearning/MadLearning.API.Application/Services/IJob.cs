using System.Threading.Tasks;

namespace MadLearning.API.Application.Services
{
    public interface IJob
    {
        Task Execute();
    }
}
