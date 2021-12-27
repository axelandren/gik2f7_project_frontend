using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF
{
    interface IGameRepository
    {
        Task<Game> Add(Game game);
        Task<IEnumerable<Game>> Get();
        Task<Game> Get(int Id);
        Task<Game> Update(Game game);
        Task<bool> Delete(int Id);
    }
}
