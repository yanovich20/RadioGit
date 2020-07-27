using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Impl
{
    public class ReclameBlockRepository:Repository<ReclameBlock>,IReclameBlockRepository
    {
        public ReclameBlockRepository(RadioContext context) : base(context) { }
        public override List<ReclameBlock> All() {
            return Queryable().Include(rb => rb.Responsible).ToList();
        }
        public List<ReclameBlock> AllFull() {
            return Queryable().Include(rb => rb.Responsible).Include(rb=>rb.Releases).ToList();
        }
        public ReclameBlock GetWithReleases(long id)
        {
            return Queryable().Include(rb => rb.Releases).ThenInclude(rel=>rel.Leading).Include(rb => rb.Responsible).SingleOrDefault(rb => rb.Id == id);
        }

        public override ReclameBlock Get(long id)
        {
            return Queryable().Include(rb => rb.Responsible).SingleOrDefault(rb => rb.Id == id);
        }

        public int GetCountHourBlocks() {
            var result = Queryable().Where(rb => rb.Status && rb.Period == Period.Hour).Count();
            return result;
        }
    }
}
