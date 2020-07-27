using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Core.Repositories;

namespace Core.Services.Impl
{
    public class ReclameBlockService : Service<ReclameBlock>, IReclameBlockService
    {
        public ReclameBlockService(IReclameBlockRepository repository) : base(repository) { }

        private readonly int numOfReleases = 10;

        public int Create(Setting setting, ReclameBlock block)
        {
            if (setting.MaxBlocks <= GetCountHourBlocks())
                return -2;
            if (Create(block) <= 0)
                return -1;
            return 1;
        }
        public int Edit(Setting setting, ReclameBlock entity)
        {
            if (entity.Period == Period.Hour)
            {
                if (setting.MaxBlocks <= GetCountHourBlocks())
                    return -2;
            }
            if (!Edit(entity))
                return -1;
            return 1;
        }
        public override bool Edit(ReclameBlock entity)
        {
            var oldBlock = Get(entity.Id);
            if (oldBlock == null)
                return false;
            oldBlock.Code = entity.Code;
            oldBlock.Comment = entity.Comment;
            //ldBlock.Created = entity.Created;
            //oldBlock.Deleted = entity.Deleted;
            oldBlock.Name = entity.Name;
            oldBlock.Period = entity.Period;
            oldBlock.ResponsibleId = entity.ResponsibleId;
            // oldBlock.Responsible = null;
            oldBlock.Status = entity.Status;
            return base.Edit(oldBlock);
        }

        public List<ReclameBlock> GetAllWithReleases()
        {
            var rep = Repository as IReclameBlockRepository;
            return rep.AllFull();
        }

        public ReclameBlock GetBlockWithReleleases(long id)
        {
            var rep = Repository as IReclameBlockRepository;
            return rep.GetWithReleases(id);
        }

        public int GetCountHourBlocks()
        {
            var rep = Repository as IReclameBlockRepository;
            return rep.GetCountHourBlocks();
        }

        public decimal GetCostReleases(long id)
        {
            var rep = Repository as IReclameBlockRepository;
            var block = rep.GetWithReleases(id);
            if (block == null)
                return -1;
            var summa = block.Releases.Select(rel => { decimal result = 0;
                if ((DateTime.Now - rel.Date).Milliseconds < 0)
                    return 0;
                switch (block.Period)
                {
                    case Period.Hour:
                        result = rel.Cost *((int) (DateTime.Now - rel.Date).TotalHours);
                        if ((DateTime.Now - rel.Date).TotalSeconds % 3600 > rel.Duration.TotalSeconds) //остаток часа
                            result += rel.Cost;
                        break;
                    case Period.Day:
                        result = rel.Cost * ((int)(DateTime.Now - rel.Date).TotalDays);
                        if ((DateTime.Now - rel.Date).TotalSeconds % 86400 > rel.Duration.TotalSeconds) //остаток дня
                            result += rel.Cost;
                        break;
                    case Period.Week:
                        result = rel.Cost *(int)(((int) (DateTime.Now - rel.Date).TotalDays) / 7);
                        if ((DateTime.Now - rel.Date).TotalSeconds % 604800 > rel.Duration.TotalSeconds) //остаток недели
                            result += rel.Cost;
                        break;
                }
                return result;
            }).Sum();
            return summa;
        }
        //public List<Release> GetAutoReleases(long rbId)
        //{
        //    ReclameBlock rb = this.GetBlockWithReleleases(rbId);
        //    if (rb == null)
        //        return null;
        //    List<Release> result = new List<Release>();
        //    if (rb.Releases.Count == 0)
        //        return new List<Release>();
        //    for (int i=0;i<numOfReleases;)
        //    {
        //        if (i == 0)
        //        {
        //            foreach (Release rel in rb.Releases)
        //            {
        //                if (i >= 9)
        //                    break;
        //                Release autoRelease = new Release
        //                {
        //                    Date = DateTime.Now,
        //                    Duration = rel.Duration,
        //                    LeadingId = rel.LeadingId,
        //                    Leading = rel.Leading,
        //                    ReclameBlockId = rel.ReclameBlockId,
        //                    ReclameBlock = rel.ReclameBlock,
        //                    State = State.Planned
        //                };
        //                if (i == 0)
        //                    autoRelease.State = State.OnAir;
        //                result.Add(autoRelease);
        //                i++;
        //            }
        //        }
        //        else
        //        {
        //            var countPeriods = 1;
        //            foreach (Release rel in rb.Releases)
        //            {
        //                if (i >= 9)
        //                    break;
        //                Release autoRelease = new Release
        //                {
        //                    Duration = rel.Duration,
        //                    LeadingId = rel.LeadingId,
        //                    Leading = rel.Leading,
        //                    ReclameBlockId = rel.ReclameBlockId,
        //                    ReclameBlock = rel.ReclameBlock,
        //                    State = State.Planned
        //                };
        //                switch (rb.Period)
        //                {
        //                    case Period.Hour:
        //                        autoRelease.Date = DateTime.Now.AddHours(countPeriods);
        //                        break;
        //                    case Period.Day:
        //                        autoRelease.Date = DateTime.Now.AddDays(countPeriods);
        //                        break;
        //                    case Period.Week:
        //                        autoRelease.Date = DateTime.Now.AddDays(countPeriods*7);
        //                        break;
        //                }
        //                result.Add(autoRelease);
        //                i++;
        //            }
        //            countPeriods++;
        //        }
        //    }
        //    return result;
        //}
    }
}
