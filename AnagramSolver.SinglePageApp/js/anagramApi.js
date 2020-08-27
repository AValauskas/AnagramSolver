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
          var words =  fetch(`${url}/${page}`)
            .then(res => res.json())          
    
            return words;      
        }

    static DeleteWords(word, page) {
            var url ='https://localhost:44321/api/dictionary';
              var words = fetch(`${url}/${word}`,
              {
                  method: 'DELETE'
              }).then(x=>this.GetWords(page))
          return words;
            }       
  }