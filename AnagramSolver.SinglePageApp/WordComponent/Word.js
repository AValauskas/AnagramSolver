wordInput = document.querySelector(".word-input")
categoryInput = document.querySelector(".category-input")
formButton = document.querySelector(".form-button")
errorMessage = document.querySelector(".error")



formButton.addEventListener('click', SubmitForm);
window.addEventListener('popstate', FillForm);

window.onload = function() {
    console.log(window.location.hash);
  };

function SubmitForm(e)
{
    console.log(wordInput.value);
    if(categoryInput.value && wordInput.value)
    {
        var index = window.location.hash.indexOf("/");
        if(index != -1)  
        {
            console.log("update"); 
            id = parseInt(window.location.hash.substring(index+1));
            let wordModel= new WordModel(wordInput.value, categoryInput.value, id);     
            console.log(wordModel); 
            var rez = anagramAPI.UpdateWord(wordModel)
            .then(x=>              
                window.location.href="http://127.0.0.1:8080/#WordManagement"                
            )
            .catch(err => errorMessage.innerHTML=err            
            )   
        }  
        else{
            console.log("insert"); 
        let wordModel= new WordModel(wordInput.value, categoryInput.value);      
            var rez = anagramAPI.InsertWord(wordModel)
            .then(x=>              
                window.location.href="http://127.0.0.1:8080/#WordManagement"                
            )
            .catch(err => errorMessage.innerHTML=err            
            )   
        }                 
    }
    else console.log("nera");    
}

function FillForm()
{   
    var index = window.location.hash.indexOf("/");
    if(index != -1)  
    {
        if(window.location.hash.substring(1,index)==="Word")
        {
            id = window.location.hash.substring(index+1);
            var rez = anagramAPI.GetWordById(id)
            .then( (val) => {         
            console.log(val);
            wordInput.value=val.word;
            categoryInput.value=val.languagePart;
            })           
        }  
    }

}
    