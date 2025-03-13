namespace NoteAPI.Models
{
    public class AddNoteDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isVisible { get; set; }
    }
}
