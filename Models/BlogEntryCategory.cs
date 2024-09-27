namespace BlogApp.Models
{
    public class BlogEntryCategory
    {
        public int BlogEntryId { get; set; }
        public BlogEntry BlogEntry { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
