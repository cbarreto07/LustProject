
using System.Linq;
using System.Threading.Tasks;
using Lust.Domain.Sociais;
using Lust.Domain.Sociais.Interfaces;
using Lust.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lust.Infra.Data.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        public AvaliacaoRepository(LustContext context)
            : base(context)
        {

        }

       
    }
}
