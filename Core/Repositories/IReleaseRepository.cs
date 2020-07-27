using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IReleaseRepository:IRepository<Release>
    {
        List<Release> GetByReclameBlockId(long id);
    }
}
