using System;
using System.Collections.Generic;
using System.Text;

using Core.Repositories;

namespace Core.Services.Impl
{
    public class ReleaseService : Service<Release>, IReleaseService
    {
        private readonly int numOfReleases = 10;
        private IReclameBlockRepository reclameBlockRepository { get; }
        public ReleaseService(IReleaseRepository repository,IReclameBlockRepository reclameBlockRepository) : base(repository)
        {
            this.reclameBlockRepository = reclameBlockRepository;
        }

        public List<Release> GetReclameBlocksReleases(long rbId)
        {
            var rep = Repository as IReleaseRepository;
            return rep.GetByReclameBlockId(rbId);
        }
        public List<Release> GetAutoReleases(long rbId)
        {
            ReclameBlock rb = reclameBlockRepository.GetWithReleases(rbId);
            if (rb == null)
                return null;
            List<Release> result = new List<Release>();
            if (rb.Releases.Count == 0)
                return new List<Release>();
            var countPeriods = 1;
            for (int i = 0; i < numOfReleases;)
            {
                DateTime currentDate = DateTime.Now;
                if (i == 0)
                {
                    foreach (Release rel in rb.Releases)
                    {
                        if (i >= numOfReleases)
                            break;
                        Release autoRelease = new Release
                        {
                            Cost = rel.Cost,
                            Date = currentDate,
                            Duration = rel.Duration,
                            LeadingId = rel.LeadingId,
                            Leading = rel.Leading,
                            ReclameBlockId = rel.ReclameBlockId,
                            ReclameBlock = rel.ReclameBlock,
                            State = State.Planned
                        };
                        if (i == 0)
                            autoRelease.State = State.OnAir;
                        result.Add(autoRelease);
                        currentDate = currentDate + rel.Duration;
                        i++;
                    }
                }
                else
                {
                    foreach (Release rel in rb.Releases)
                    {
                        if (i >= numOfReleases)
                            break;
                        Release autoRelease = new Release
                        {
                            Cost = rel.Cost,
                            Duration = rel.Duration,
                            LeadingId = rel.LeadingId,
                            Leading = rel.Leading,
                            ReclameBlockId = rel.ReclameBlockId,
                            ReclameBlock = rel.ReclameBlock,
                            State = State.Planned
                        };

                        var date = currentDate;

                        switch (rb.Period)
                        {
                            case Period.Hour:
                                date = date.AddHours(countPeriods);
                                //autoRelease.Date = DateTime.Now.AddHours(countPeriods);
                                break;
                            case Period.Day:
                                date = currentDate.AddDays(countPeriods);
                                //autoRelease.Date = DateTime.Now.AddDays(countPeriods);
                                break;
                            case Period.Week:
                                date = currentDate.AddDays(countPeriods * 7);
                                //autoRelease.Date = DateTime.Now.AddDays(countPeriods * 7);
                                break;
                        }
                        autoRelease.Date = date;
                        result.Add(autoRelease);
                        currentDate = currentDate + rel.Duration;
                        i++;
                    }
                    countPeriods++;
                }
            }
            return result;
        }
        public State GetState(Release rel)
        {
            TimeSpan diff = DateTime.Now -rel.Date;
            if (diff.TotalSeconds > 0 && diff.TotalSeconds < rel.Duration.TotalSeconds) 
                return State.OnAir;
            if (diff.TotalSeconds > 0 && diff.TotalSeconds > rel.Duration.TotalSeconds)
                return State.Completed;
            if (diff.TotalSeconds < 0)
                return State.Planned;
            return State.None;
        }
    }
}

