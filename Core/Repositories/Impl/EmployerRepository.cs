using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories.Impl
{
    public class EmployerRepository:Repository<Employer>,IEmployerRepository
    {
        public EmployerRepository(RadioContext context) : base(context) { }
    }
}
