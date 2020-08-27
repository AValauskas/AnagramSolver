function Router(routes) {
    try {
        if (!routes) {
            throw 'error: routes param is mandatory';
        }
        this.constructor(routes);
        this.init();
    } catch (e) {
        console.error(e);   
    }
}

Router.prototype = {
    routes: undefined,
    rootElem: undefined,
    constructor: function (routes) {
        this.routes = routes;
        this.rootElem = document.getElementById('app');    
       
    },
    init: function () {
        var r = this.routes;
        (function(scope, r) { 
            window.addEventListener('hashchange', function (e) {
                scope.hasChanged(scope, r);
               
            });
        })(this, r);
        this.hasChanged(this, r);
    },
    hasChanged: function(scope, r){
        if (window.location.hash.length > 0) {
            for (var i = 0, length = r.length; i < length; i++) {
                var route = r[i];
                let path    
                var index = window.location.hash.indexOf("/");
                if(index!=-1)  
                path = window.location.hash.substring(1, index);         
                else
                path = window.location.hash.substring(1);
                
                if(route.isActiveRoute(path)) {
                    scope.goToRoute(route.htmlName);
                }
            }
        } else {
            for (var i = 0, length = r.length; i < length; i++) {
                var route = r[i];
                if(route.default) {
                    scope.goToRoute(route.htmlName);
                }
            }
        }
    },
    goToRoute: function (htmlName) {        
        (function(scope) { 
            var url = htmlName+'Component/' + htmlName+'.html',
                xhttp = new XMLHttpRequest();                
            xhttp.onreadystatechange = function () {
                if (this.readyState === 4 && this.status === 200) {  
                        
                    if( scope.rootElem.hasChildNodes){    
                        while ( scope.rootElem.firstChild) {
                            scope.rootElem.removeChild( scope.rootElem.lastChild);           
                          }
                        }
                    
                   var scriptNode = document.createElement('script');      
                   scriptNode.src = htmlName+'Component/' + htmlName+'.js'
                       
                   var linkNode = document.createElement('link');      
                   linkNode.rel ="stylesheet";
                   linkNode.href=htmlName+'Component/' + htmlName+'.css'

                   scope.rootElem.innerHTML = this.responseText;
                   scope.rootElem.appendChild(scriptNode)
                   scope.rootElem.appendChild(linkNode)
                }
            };
            
            xhttp.open('GET', url, true);
            xhttp.send();
        })(this);
    }
    
};
