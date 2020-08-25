(function () {
    function init() {
        var router = new Router([
            new Route('SearchPage', 'SearchPage', true),    
            new Route('about', 'about'),           
            new Route('WordManagement', 'WordManagement'),            
        ]);
    }
    init();
}());