using Data.Contracts;
using Entity.Complaitns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ComplaintsRepository : Repository<Complaints>, IComplaintsRepository
    {
        public ComplaintsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
