using System;

namespace FloofBot.Core.Services.Database.Models
{
    public class DbEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}