using System.Threading.Tasks;

namespace Store.Contracts.Managers
{
    public interface ISeedManager
    {
        Task Run();
    }
}