var navbar = document.getElementById("navbar");
var navbar = document.getElementById("menu");
var navlinks = document.getElementById("navlinks");

var sticky = navbar.offsetTop;

function myFunction() {
  if (window.pageYOffset >= sticky) {
    navbar.classList.add("sticky")
  } else {
    navbar.classList.remove("sticky");
  }
}

 function showMenu() {
        snelkoppelingen.style.right = "0";
        }
function hideMenu() {
        snelkoppelingen.style.left = "-250px";
        }
