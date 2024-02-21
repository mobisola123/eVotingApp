using eVotingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace eVotingApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Voters> Voters { get; set; }
    }
}
