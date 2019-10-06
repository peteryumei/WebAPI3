﻿using Microsoft.EntityFrameworkCore;

namespace WebAPI3.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> todoItems { get; set; }
    }
}
