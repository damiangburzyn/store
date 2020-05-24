using System;
using System.Threading.Tasks;

namespace Store.Contracts.Managers
{
    public interface ICacheManager
    {
        Task<TItem> Get<TItem>(string key);

        bool TryGetValue<TItem>(string key, out TItem value);

        Task<TItem> Set<TItem>(string key, TItem value, TimeSpan absoluteExpirationRelativeToNow);

        Task Remove(string key);

        Task RemoveAllCache();
    }
}