using System.Threading.Tasks;

namespace Ifood.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}