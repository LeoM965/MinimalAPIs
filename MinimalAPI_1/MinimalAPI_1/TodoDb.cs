﻿using Microsoft.EntityFrameworkCore;

namespace MinimalAPI_1
{
    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions <TodoDb> options) : base(options)
        {
            
        }

        public DbSet<Todo> Todos => Set<Todo>();

    }
}
