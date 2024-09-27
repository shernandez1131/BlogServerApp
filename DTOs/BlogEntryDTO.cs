namespace BlogApp.DTOs
{
    public class BlogEntryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
        public UserDTO Author { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
