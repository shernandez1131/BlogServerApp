namespace BlogApp.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<BlogEntryCategory> BlogEntryCategories { get; set; }
    }
}
