namespace NoteAPI.Models
{
    public class UpdateNoteDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isVisible { get; set; }
    }
}
