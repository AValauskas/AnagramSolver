import { hello } from '';

function getContent(fragmentId){

    // lets do some custom content for each page of your website
      var pages = {
        home: "This is the Home page. Welcome to my site.",
        about: "This page will describe what my site is about",
        contact: "Contact me on this page if you have any questions"
      };
    
    // look up what fragment you are searching for in the object
      return pages[fragmentId];
    }

function loadContent(){
  var contentDiv = document.getElementById("app");
  fragmentId = location.hash.substr(1);

  contentDiv.innerHTML = getContent(fragmentId);
}

if(!location.hash) {
    location.hash = "#home";
  }

window.addEventListener("hashchange", loadContent);

loadContent();