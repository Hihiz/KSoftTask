﻿namespace KSoftTask.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; } = new List<AuthorBook>();
    }
}
