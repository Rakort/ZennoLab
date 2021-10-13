using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ZennoLab.Api.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected SqlContext()
        {
        }

        public virtual DbSet<DataSetEntity> Contacts { get; set; }
    }
}
