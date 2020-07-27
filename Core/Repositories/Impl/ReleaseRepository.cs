using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Impl
{
    public class ReleaseRepository:Repository<Release>,IReleaseRepository
    {
        public ReleaseRepository(RadioContext context) : base(context) { }
        public List<Release> GetByReclameBlockId(long id)
        {
            return Queryable().Include(rel=>rel.Leading).Where(r => r.ReclameBlockId == id).ToList();
        }
    }
}
