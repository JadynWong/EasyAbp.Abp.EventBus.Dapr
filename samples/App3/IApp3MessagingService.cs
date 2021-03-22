using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace App3
{
    public interface IApp3MessagingService : ITransientDependency
    {
        Task RunAsync(string message);
    }
}