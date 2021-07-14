using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class SignalRDemoContext : DbContext
    {
        public SignalRDemoContext (DbContextOptions<SignalRDemoContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Employee> Employee { get; set; }

        public DbSet<WebApplication1.Models.Notification> Notification { get; set; }
    }
}
