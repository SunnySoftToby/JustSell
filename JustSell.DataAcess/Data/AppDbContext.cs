﻿using JustSell.Models;
using Microsoft.EntityFrameworkCore;

namespace JustSell.DataAccess
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Company> Companies { get; set; }
	}
}
