using QuickSocketDemo.Models;
using System.Threading.Tasks;

namespace QuickSocketDemo.Http
{
    public interface IQuickSocketApi
    {
        Task SendAsync(string connectionId, string payload);
        Task<string> AuthAsync(string referenceId);
    }
}
