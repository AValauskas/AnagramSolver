class anagramAPI {
  
    constructor() {
      this.apiUrl = 'https://localhost:44321/api/anagram';
    }

    static GetAnagrams(word) {
    var url ='https://localhost:44321/api/anagram';
      var anagrams =  fetch(`${url}/${word}`)
        .then(res => res.json())          

        return anagrams;      
    }

    static GetWords(page) {
        var url ='https://localhost:44321/api/dictionary';
          var anagrams =  fetch(`${url}/${page}`)
            .then(res => res.json())          
    
            return anagrams;      
        }


    
  }