
using System.Threading.Tasks;

namespace HallOfFame.DAL.Data
{
    public interface IDataBaseInitializer
    {
        Task InitializeAsync();
    }
}
