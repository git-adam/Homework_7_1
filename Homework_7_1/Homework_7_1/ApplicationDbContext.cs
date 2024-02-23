using System;
using System.Data.Entity;
using System.Linq;

namespace Homework_7_1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }
    }

}