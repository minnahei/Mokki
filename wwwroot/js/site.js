// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let anfitrion = document.getElementById('anfitrion');
let huesped = document.getElementById('huesped')
let pueblo = document.getElementById('pueblo')
let ciudad = document.getElementById('ciudad')


function Mostrar() {
    if (anfitrion.checked) {
        pueblo.style.display = "block";
        ciudad.style.display = "none";


    } else if (huesped.checked) {
        ciudad.style.display = "block";
        pueblo.style.display = "none";

    }
}

if (anfitrion != null) {
    anfitrion.addEventListener('change', Mostrar)
}
if (huesped != null) {
    huesped.addEventListener('change', Mostrar)
}