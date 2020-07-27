using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IReleaseService:IService<Release>
    {
        List<Release> GetReclameBlocksReleases(long rbId);
        List<Release> GetAutoReleases(long rbId);
        State GetState(Release rel);
    }
}
