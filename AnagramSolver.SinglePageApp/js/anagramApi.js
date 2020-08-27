const Dic_API = 'https://localhost:44321/api/dictionary'
const Anagram_API = 'https://localhost:44321/api/dictionary'

class anagramAPI {
    static GetAnagrams(word) {
        var anagrams = fetch(`${Anagram_API}/${word}`)
            .then(res => res.json())

        return anagrams;
    }

    static GetWords(page) {
        var words = fetch(`${Dic_API}/${page}`)
            .then(res => res.json())

        return words;
    }

    static DeleteWords(word, page) {
        var words = fetch(`${Dic_API}/${word}`,
            {
                method: 'DELETE'
            }).then(x => this.GetWords(page))
        return words;
    }

    static UpdateWord(word) {
        var words = fetch(`${Dic_API}/${word}`,
            {
                method: 'PATCH'
            }).then(x => this.GetWords(page))
        return words;
    }
    static InsertWord(word) {
        console.log(word)
        var promise =fetch(`${Dic_API}`,
            {
                method: 'POST',
                body: JSON.stringify(word),
                headers: {'Content-Type': 'application/json'}
            })
            .then((res) => {
                if (res.status === 400) {                     
                    throw 'Word already exist';              
                   
                }})
            .catch(err =>{
                throw new Error(err);    }          
                ) 
        return promise;
              

    }

}