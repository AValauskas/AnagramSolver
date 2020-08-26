


const api = new anagramAPI();
$( document ).ready(function() {
    console.log( "ready!" );
});

const wordInput = document.querySelector('.word-input')
const wordButton = document.querySelector(".word-button")

wordButton.addEventListener('click', SubmitWord);

function SubmitWord(event)
{
  //  event.preventDefault();
    console.log("yra")

}

