using Assets.Models;

namespace Assets.Contracts
{
    public interface IRepository<T>
    {
        Task<string> Post(T asset);
        Task<T> Read(string assetId);
        Task<IEnumerable<T>> Read();
        Task<bool> Update(T asset);
        Task<bool> Delete(string assetId);
        Task<List<string>> GetPurchaseType();

    }
}
