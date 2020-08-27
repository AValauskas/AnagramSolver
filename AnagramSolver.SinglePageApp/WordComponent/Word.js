wordInput = document.querySelector(".word-input")
categoryInput = document.querySelector(".category-input")
formButton = document.querySelector(".form-button")
errorMessage = document.querySelector(".error")


formButton.addEventListener('click', SubmitForm);

function SubmitForm(e)
{
    if(categoryInput.value && wordInput.value)
    {
        let wordModel= new WordModel(wordInput.value, categoryInput.value);      
            var rez = anagramAPI.InsertWord(wordModel)
            .then(x=>
                {if(!rez.catch);
                { 
                window.location.href="http://127.0.0.1:8080/#WordManagement"
                }}
            )
            rez.catch(err => errorMessage.innerHTML=err            
            ) 
                          
    }
    else console.log("nera");
   
    
}