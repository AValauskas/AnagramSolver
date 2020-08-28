dicWordList = document.querySelector(".word-list");
dictionaryTab = document.querySelector(".dictionary-route");
pageBar = document.querySelector(".page-bar");
SearchInput = document.querySelector(".search-input")
SearchButton = document.querySelector(".search-button")

window.addEventListener('popstate', CallMethod);
dictionaryTab.addEventListener('click', GetDictionary);
SearchButton.addEventListener('click', SearchWord);
page =1;




window.onload = function() {   
        GetDictionary();
        FormPageBar(null);    
  };

function CallMethod()
{  if(window.location.hash==="#WordManagement")    
    {
        console.log("page", page);
        GetDictionary();
        FormPageBar(null);
    }
}

function GetDictionary(){
console.log(window.location.hash);
    if(window.location.hash==="#WordManagement")    
    {
    var wordsPromise = anagramAPI.GetWords(page)
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
    var value = confirm("Are you sure, you want delete this item?")

    if(value){
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
}

function NextPage(e)
{
    console.log("clicked");
    if(isNaN(e.target.innerHTML))  
    console.log("žodis")
    else {
        let num =parseInt(e.target.innerHTML)
        page =num;
        console.log("psl kits ", page)
        GetDictionary(num);
        FormPageBar(null);       
    }
}

function PaginationFormat(num)
{
    if(num = 1)  
    {
    var x = document.getElementById("page-previous");   
    x.style.pointerEvents="auto";
    x.style.color="blue";
    }
    else 
    GetDictionary(parseInt(e.target.innerHTML))
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

function FormPageBar(word)
{
    var pageCount = anagramAPI.GetPageCount(word)
    pageCount.then( (val) => 
    {
        let pageBefore, pageAfter;
        if(val>6)
        {
            pageBefore = page - 3;
            if(pageBefore <= 0)
            {
            pageAfter = page +3 - pageBefore;
            pageBefore=1;
            }
            else pageAfter = page + 3;
        }
        else
        {
            pageBefore=1;
            pageAfter=val;
        }
        PageBarView(pageBefore,pageAfter)
    })
}

function PageBarView(pageBefore, pageAfter)
{
    CleanPages();
    const PaginationUl= document.createElement("ul");
    PaginationUl.classList.add("page-bar");
    PaginationUl.classList.add("pagination");
    PaginationUl.classList.add("justify-content-center");

    for (i = pageBefore; i <= pageAfter; i++) {
        const pageUnit = document.createElement("li");       
        pageUnit.innerHTML='<a class="page-link" >'+i+'</a>';     
        pageUnit.classList.add('bar-item');
        PaginationUl.appendChild(pageUnit);
      }
      pageBar.appendChild(PaginationUl);

      pages = document.querySelectorAll(".page-link");
      for (var i = 0; i < pages.length; i++) 
        pages[i].addEventListener('click', NextPage)   
}

function CleanPages()
{
    if(pageBar.hasChildNodes){    
        while (pageBar.firstChild) {
            pageBar.removeChild(pageBar.lastChild);           
          }
    };
}

function SearchWord()
{      
    var anagramsPromise = anagramAPI.SearchWord( SearchInput.value)
    anagramsPromise.then( (val) => 
    {         
        CleanWords();  
        val.forEach(element => {
            console.log(element.word)
            let wordModel= new WordModel(element.word, element.languagePart, element.id);
            console.log(wordModel);
            FormWord(wordModel);           
        });         
    });
}

