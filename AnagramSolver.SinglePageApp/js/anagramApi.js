class anagramAPI {
  
    constructor() {
      this.apiUrl = 'https://localhost:44321/api/anagram';
    }
    static GetAnagrams(word) {
    let anagramsobject;
    var url ='https://localhost:44321/api/anagram';
      var anagrams =  fetch(`${url}/${word}`)
        .then(res => res.json())   
       

        return anagrams;
    
        
             
        
    }

    
  }