dicWordList = document.querySelector(".word-list");
dictionaryTab = document.querySelector(".dictionary-route");

dictionaryTab.addEventListener('click', GetDictionary);


function GetDictionary()
{
    var anagramsPromise = anagramAPI.GetWords(1)
    anagramsPromise.then( (val) => 
    {
        CleanWords();      
        val.forEach(element => {        
            let wordModel= new Word(element.word, element.languagePart);          
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
    newLanguagePart.innerHTML=word.category;
    newLanguagePart.classList.add('word-item');
    wordDiv.appendChild(newLanguagePart);

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

