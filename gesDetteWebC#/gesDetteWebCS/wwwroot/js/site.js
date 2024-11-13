// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
console.log("app.js est chargé");
const toggleSwitch = document.getElementById("toggleSwitch");
const formUser = document.getElementById("userSwitch");
formUser.style.display = "none";

toggleSwitch.addEventListener("change", function () {
  if (this.checked) {
    formUser.style.display = "block";
    console.log("oonn", this.checked);
  } else {
    formUser.style.display = "none";
  }
});
