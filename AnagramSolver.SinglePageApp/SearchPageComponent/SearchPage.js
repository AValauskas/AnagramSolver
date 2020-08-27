wordInput = document.querySelector(".word-input")
wordButton = document.querySelector(".word-button")
wordList = document.querySelector(".word-list")


wordButton.addEventListener('click', SubmitWord);

function SubmitWord(event)
{
    
    CleanAnagrams();
    console.log( wordInput.value);
    var anagramsPromise = anagramAPI.GetAnagrams( wordInput.value)
    anagramsPromise.then( (val) => 
    {
        CleanAnagrams();      
        val.forEach(element => {
            console.log(element.word)
            let wordModel= new WordModel(element.word, element.languagePart);
            console.log(wordModel);
            FormWord(wordModel);
            var x = document.getElementById("anagramTable");   
             x.style.display="block";
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

    wordList.appendChild(wordDiv);
    console.log(wordList);

}

function CleanAnagrams()
{
    if(wordList.hasChildNodes){    
        while (wordList.firstChild) {
            wordList.removeChild(wordList.lastChild);           
          }
    };

}

