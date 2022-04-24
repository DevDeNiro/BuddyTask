// Show Sidebar
// let raw = document.getElementById("raw");
// let flex = document.getElementById("check");
// flex.addEventListener("click", function () {
//   raw.style.display = raw.style.display == "none" ? "block" : "none";
// });

let hint = document.getElementById("raw");
let btn = document.getElementById("check");
let content = document.getElementById("flex-content");

btn.addEventListener("click", function () {
  hint.className = hint.className !== "show" ? "show" : "hide";
  if (hint.className === "hide") {
    hint.style.display = "block";
    content.style.display = "block";
    window.setTimeout(function () {
      hint.style.opacity = 1;
      hint.style.transform = "translateX(0)";
      content.style.transform = "translateX(0)";
    }, 0);
  }
  if (hint.className === "show") {
    hint.style.opacity = 0;
    hint.style.transform = "translateX(-20px)";
    content.style.transform = "translateX(-20px)";
    window.setTimeout(function () {
      hint.style.display = "none";
    }, 200); // timed to match animation-duration
  }
});
