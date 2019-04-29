using System.Linq;
using System.Threading.Tasks;
using Lust.Domain.Assinaturas;
using Lust.Domain.Assinaturas.Interfaces;
using Lust.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lust.Infra.Data.Repository
{
    public class AssinaturaRepository : Repository<Assinatura>, IAssinaturaRepository
    {
        public AssinaturaRepository(LustContext context)
            : base(context)
        {

        }

       
    }
}
