using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteAPI.Data;
using NoteAPI.Models;
using NoteAPI.Models.Entities;

namespace NoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteDbContext noteDbContext;
        private object dbContext;

        public NoteController(NoteDbContext noteDbContext)
        {
            this.noteDbContext = noteDbContext;
        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            //get the notes from db
            return Ok(noteDbContext.Notes.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetNoteById(Guid id)
        {
            var note = noteDbContext.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public IActionResult AddNote(AddNoteDto addnotedto)
        {
            var noteEntity = new Note()
            {
                Title = addnotedto.Title,
                Description = addnotedto.Description,
                isVisible = addnotedto.isVisible,
            };

            noteDbContext.Notes.Add(noteEntity);
            noteDbContext.SaveChanges();

            return Ok(noteEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateNote(Guid id, UpdateNoteDto updateNoteDto)
        {
            var note = noteDbContext.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            note.Title = updateNoteDto.Title;
            note.Description = updateNoteDto.Description;
            note.isVisible = updateNoteDto.isVisible;

            noteDbContext.SaveChanges();
            return Ok(note);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteNote(Guid id)
        {
            var note = noteDbContext.Notes.Find(id);

            if (note == null) 
            {
                return NotFound();
            }

            noteDbContext.Notes.Remove(note);
            noteDbContext.SaveChanges();

            return Ok(note);
        }
    }
}
