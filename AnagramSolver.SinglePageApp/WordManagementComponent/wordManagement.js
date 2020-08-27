dicWordList = document.querySelector(".word-list");
dictionaryTab = document.querySelector(".dictionary-route");

window.addEventListener('popstate', GetDictionary);

dictionaryTab.addEventListener('click', GetDictionary);

window.onload = function() {   
    GetDictionary();
  };

function GetDictionary()
{console.log(window.location.hash);
    if(window.location.hash==="#WordManagement")    
    {
    var wordsPromise = anagramAPI.GetWords(1)
    wordsPromise.then( (val) => 
    {
        CleanWords();      
        val.forEach(element => {        
            let wordModel= new WordModel(element.word, element.languagePart, element.id);          
            FormWord(wordModel);
            var x = document.getElementById("anagramTable");            
        });    
        
    });
    }
}

function DeleteWord(word)
{
    var wordsPromise = anagramAPI.DeleteWords(word,1)
    wordsPromise.then( (val) => 
    {
        CleanWords();      
        val.forEach(element => {        
            let wordModel= new WordModel(element.word, element.languagePart, element.id);          
            FormWord(wordModel);
            var x = document.getElementById("anagramTable");            
        });    
        
    });

}

function FormWord(word)
{   
    const wordDiv= document.createElement("tr");
    wordDiv.classList.add("word");

    const nr = document.createElement("th");
    nr.scope="col";
    nr.innerHTML=word.word;
    nr.classList.add('word-item');
    wordDiv.appendChild(nr);

    const newWord = document.createElement("th");
    newWord.scope="col";
    newWord.innerHTML=word.word;
    newWord.classList.add('word-item');
    wordDiv.appendChild(newWord);

    const newLanguagePart = document.createElement("th");
    newLanguagePart.scope="col";
    newLanguagePart.innerHTML=word.languagePart;
    newLanguagePart.classList.add('word-item');
    wordDiv.appendChild(newLanguagePart);

    const update = document.createElement("th");
    update.scope="col";
    update.innerHTML='<a href="#Word/' + word.id + '">Update</a>'
    update.classList.add('word-item');
    wordDiv.appendChild(update);

    const deleteRef = document.createElement("th");
    deleteRef.scope="col";
    deleteRef.innerHTML='<button onclick="DeleteWord(\'' + word.word + '\')">Delete</button>'
    deleteRef.classList.add('word-item');
    wordDiv.appendChild(deleteRef);   

    dicWordList.appendChild(wordDiv);

    
}

function CleanWords()
{
    if(dicWordList.hasChildNodes){    
        while (dicWordList.firstChild) {
            dicWordList.removeChild(dicWordList.lastChild);           
          }
    };

}

