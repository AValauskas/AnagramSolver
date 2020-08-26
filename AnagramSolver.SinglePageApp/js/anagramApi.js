class anagramAPI {
  
    constructor() {
      this.apiUrl = 'https://localhost:44321/api/anagram';
    }
    static GetAnagrams(word) {
    var url ='https://localhost:44321/api/anagram';
      var rez = fetch(`${url}/${word}`)
        .then(res => res.json()).Wait();
       
        console.log(rez);
      return rez;
    }

  }