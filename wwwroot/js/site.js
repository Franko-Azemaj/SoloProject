// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const selectLanguage = (code, el) => {
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
    
}