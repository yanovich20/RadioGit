using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IReclameBlockRepository:IRepository<ReclameBlock>
    {
        List<ReclameBlock> AllFull();
        ReclameBlock GetWithReleases(long id);
        int GetCountHourBlocks();
    }
}
