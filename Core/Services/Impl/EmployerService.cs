using System;
using System.Collections.Generic;
using System.Text;

using Core.Repositories;

namespace Core.Services.Impl
{
    public class EmployerService:Service<Employer>,IEmployerService
    {
        public EmployerService(IEmployerRepository repository) : base(repository) { }

        public override bool Edit(Employer entity)
        {
            var oldEmployer = Get(entity.Id);
            oldEmployer.Name = entity.Name;
            return base.Edit(oldEmployer);
        }
    }
}
