using Project.DependencyInjection;
using System.Threading.Tasks;

namespace Project
{
    public interface ISaveSystem : IAsyncDIModule
    {
        Task Save<T>(string saveId, T saveData);
        Task<T> Load<T>(string saveId);
        bool HasSave(string saveId);
        bool ClearSave(string saveId);
    }
}
