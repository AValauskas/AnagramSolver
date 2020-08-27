wordInput = document.querySelector(".word-input")
categoryInput = document.querySelector(".category-input")
formButton = document.querySelector(".form-button")


formButton.addEventListener('click', SubmitForm);

function SubmitForm(e)
{
    if(categoryInput.value && wordInput.value)
    {
        let wordModel= new WordModel(wordInput.value, categoryInput.value); 
        anagramAPI.InsertWord(wordModel)
    }
    else console.log("nera");
   
    
}