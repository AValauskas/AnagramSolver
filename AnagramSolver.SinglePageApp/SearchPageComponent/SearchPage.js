const wordInput = document.querySelector(".word-input")
const wordButton = document.querySelector(".word-button")

wordButton.addEventListener('click', SubmitWord);

function SubmitWord(event)
{
    console.log( wordInput.value);
    var anagrams = anagramAPI.GetAnagrams( wordInput.value)
    console.log(anagrams)

}

