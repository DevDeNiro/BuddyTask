// Show Sidebar
// let raw = document.getElementById("raw");
// let flex = document.getElementById("check");
// flex.addEventListener("click", function () {
//   raw.style.display = raw.style.display == "none" ? "block" : "none";
// });

var hint = document.getElementById("raw");
var btn = document.getElementById("check");

btn.addEventListener("click", function () {
  hint.className = hint.className !== "show" ? "show" : "hide";
  if (hint.className === "hide") {
    hint.style.display = "block";
    window.setTimeout(function () {
      hint.style.opacity = 1;
      hint.style.transform = "translateX(0)";
    }, 0);
  }
  if (hint.className === "show") {
    hint.style.opacity = 0;
    hint.style.transform = "translateX(-20px)";
    window.setTimeout(function () {
      hint.style.display = "none";
    }, 500); // timed to match animation-duration
  }
});
