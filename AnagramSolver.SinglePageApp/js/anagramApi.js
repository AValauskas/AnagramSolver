class anagramAPI {
    constructor() {
      this.url = 'https://localhost:44321/api/anagram';
    }
    GetAnagrams(word) {
      return fetch(`${this.url}/${word}`)
        .then(res => res.json());
    }

  }