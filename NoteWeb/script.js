const saveButton = document.querySelector('#savebtn');
const titleInput = document.querySelector('#title');
const descriptionInput = document.querySelector('#description');
const notesContainer = document.querySelector('#note-container');
const deleteButton = document.querySelector('#deletebtn');

function clearForm() {
    titleInput.value = '';
    descriptionInput.value = '';
    deleteButton.classList.add('hidden')
}

function displayNoteinForm(note) {
    titleInput.value = note.title;
    descriptionInput.value = note.description;
    deleteButton.classList.remove('hidden');
    deleteButton.setAttribute('data-id', note.id);
    saveButton.setAttribute('data-id', note.id);
}

function getNoteById(id) {
    fetch(`https://localhost:7284/api/Note/${id}`)
    .then(data => data.json())
    .then(response => displayNoteinForm(response));
}

function populateForm(id) {
    getNoteById(id);
}

function addNote(title, description) {
    
    const body = {
        title: title,
        description: description,
        isVisible: true
    };

    fetch('https://localhost:7284/api/Note',{
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
            "content-type": "application/json"
        }
    })
    .then(data => data.json())
    .then(response => {
        clearForm();
        getAllNotes();
    });
}

function displayNotes(notes) {
    let allNotes = '';

    notes.forEach(note => {
        const noteElement = `
                             <div class="note" data-id="${note.id}">
                             <h3>${note.title}</h3>
                             <p>${note.description}</p>
                             </div>
                            `;
        allNotes += noteElement;
    });

    notesContainer.innerHTML = allNotes;

    document.querySelectorAll('.note').forEach(note =>{
        note.addEventListener('click', function(){
          populateForm(note.dataset.id);
        });
    });
}

function getAllNotes() {
    fetch('https://localhost:7284/api/Note')
    .then(data => data.json())
    .then(response => displayNotes(response));
}

getAllNotes();

function updateNote(id, title, description) {
    const body = {
        title: title,
        description: description,
        isVisible: true
    };

    fetch(`https://localhost:7284/api/Note/${id}`,{
        method: 'PUT',
        body: JSON.stringify(body),
        headers: {
            "content-type": "application/json"
        }
    })
    .then(data => data.json())
    .then(response => {
        clearForm();
        getAllNotes();
    });
}

saveButton.addEventListener('click', function(){
    const id = saveButton.dataset.id;

    if(id){
        updateNote(id, title.value, description.value)
    }
    else{
        addNote(titleInput.value, descriptionInput.value);
    }
});

function deleteNote(id) {
    fetch(`https://localhost:7284/api/Note/${id}`,{
        method: 'DELETE',
        headers: {
            "content-type": "application/json"
        }
    })
    .then(response => {
        clearForm();
        getAllNotes();
    });
}

deleteButton.addEventListener('click', function() {
   const id = deleteButton.dataset.id;
   deleteNote(id); 
});