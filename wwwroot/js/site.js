// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function selectLanguage(code, el){
    el.classList.toggle("selected");

    const container = document.getElementById("selected-languages-container");

    var child = container.lastElementChild; 
    while (child) {
        container.removeChild(child);
        child = container.lastElementChild;
    }

    const selectedLanguages = Array.prototype.slice.call( document.querySelectorAll('.language-item.selected'));

    const clones = selectedLanguages.map(e => {
        const clone = e.cloneNode(true);
        clone.classList.remove("selected");
        return clone;
    } );

    clones.forEach(clone => {
        container.appendChild(clone);
    });  
    
    if(clones.length > 4){
        const notSelectedLanguages = Array.prototype.slice.call( document.querySelectorAll('.language-item:not(.selected)'));
        notSelectedLanguages.forEach(el => {
            el.style.opacity = 0.5;
            el.style.pointerEvents= "none";
        }); 
    } else {
        const languages = Array.prototype.slice.call( document.querySelectorAll('.language-item'));
        languages.forEach(el => {
            el.style.opacity = 1;
            el.style.pointerEvents= "auto";
        }); 
    }


}

const onJobNoticeFormSubmit = (e) => {

    const selectedLanguages = Array.prototype.slice.call( document.querySelectorAll('.language-item.selected'));

    selectedLanguages.forEach(language => {
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = "skills";
        input.value = language.dataset.skillCode;
        e.target.appendChild(input);
    }); 
}




//Job Part

function selectLanguageJob(code, el){

    el.classList.toggle("selected");

   
    const selectedAllLanguages = Array.prototype
    .slice.call( document.querySelectorAll('.language-item.selected'));

    const clones = selectedAllLanguages.map(e => {
        const clone = e.cloneNode(true);
        clone.classList.remove("selected");
        return clone;
    } );
    
    if(clones.length > 4){
        const notSelectedLanguages = Array.prototype.slice.call( document.querySelectorAll('.language-item:not(.selected)'));
        notSelectedLanguages.forEach(el => {
            el.style.opacity = 0.5;
            el.style.pointerEvents= "none";
        }); 
    } else {
        const languages = Array.prototype.slice.call( document.querySelectorAll('.language-item'));
        languages.forEach(el => {
            el.style.opacity = 1;
            el.style.pointerEvents= "auto";
        }); 
    }


}


const onJobCreationFormSubmit = (e) => {

    const selectedLanguages = Array.prototype.slice.call( document.querySelectorAll('.language-item.selected'));

    selectedLanguages.forEach(language => {
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = "skills";
        input.value = language.dataset.skillCode;
        e.target.appendChild(input);
    }); 
}