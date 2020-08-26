const wordInput = document.querySelector(".word-input")
const wordButton = document.querySelector(".word-button")
const wordList = document.querySelector(".word-list")


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
            let wordModel= new Word(element.word, element.languagePart);
            console.log(wordModel);
            FormWord(wordModel);
        });         
    });
}

function FormWord(word)
{
    const wordDiv= document.createElement("div");
    wordDiv.classList.add("word");

    const newWord = document.createElement("td");
    newWord.innerHTML=word.word;
    newWord.classList.add('word-item');
    wordDiv.appendChild(newWord);

    const newLanguagePart = document.createElement("td");
    newLanguagePart.innerHTML=word.category;
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

