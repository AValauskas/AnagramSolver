function  Func() {
        console.log("Hello world!");
    }
    async function connectedCallback() {
        let res = await fetch( 'SearchPage.html' )

        this.attachShadow( { mode: 'open' } )
            .innerHTML = await res.text()
    }
