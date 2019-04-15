using APIData.Entities;
using System.Threading.Tasks;

namespace APIData.Repos
{
    public interface IRepos<T> where T : BaseEntity
    {
         Task<T> Get(long id);
    }
}