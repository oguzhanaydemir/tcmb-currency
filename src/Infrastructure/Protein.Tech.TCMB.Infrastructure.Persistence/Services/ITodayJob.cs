using Hangfire;
using System.Threading.Tasks;

namespace Protein.Tech.TCMB.Infrastructure.Persistence.Services
{
    [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public interface ITodayJob
    {
        Task<bool> RunAsync();
    }
}
