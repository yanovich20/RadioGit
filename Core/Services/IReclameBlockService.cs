using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IReclameBlockService: IService<ReclameBlock>
    {
        List<ReclameBlock> GetAllWithReleases();
        ReclameBlock GetBlockWithReleleases(long id);
        int Create(Setting setting, ReclameBlock block);
        int Edit(Setting setting, ReclameBlock entity);
        int GetCountHourBlocks();
        decimal GetCostReleases(long id);
        //List<Release> GetAutoReleases(long rbId);
    }
}
