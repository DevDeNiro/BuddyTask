let todo = [];
//Loading resources
window.addEventListener("load", () => {
  // select DOM for main todolist section
  const todoInput = document.querySelector(".titleTask");
  const todoDate = document.querySelector(".dateTask");
  const todoButton = document.querySelector(".submitTask");
  const todoList = document.querySelector(".todo");

  // select DOM for Sidebar show
  let hint = document.getElementById("raw");
  let btn = document.getElementById("check");
  let content = document.getElementById("flex-content");

  let save = window.localStorage.getItem("ListRegistered");
  if (save) {
    let save_obj = JSON.parse(save);
    todo = save_obj;
    RefreshTodo();
  }

  todoButton.addEventListener("click", function () {
    createTodo();
  });

  function createTodo() {
    let info = {
      name: todoInput.value,
      date: todoDate.value,
    };

    todo.push(info);

    todoInput.value = "";
    todoDate.value = "";

    RefreshTodo();
  }

  function RefreshTodo() {
    todoList.innerHTML = "";

    for (let i = 0; i < todo.length; i++) {
      let list = document.createElement("div");
      list.classList.add("modal_list");

      let listItem = document.createElement("div");
      listItem.classList.add("modal_item");
      list.appendChild(listItem);

      let okSVG = document.createElement("svg");
      okSVG.classList.add("ok");
      listItem.appendChild(okSVG);

      let notOkSVG = document.createElement("svg");
      notOkSVG.classList.add("notOk");
      listItem.appendChild(notOkSVG);

      let updateSVG = document.createElement("svg");
      updateSVG.classList.add("correct");
      listItem.appendChild(updateSVG);

      let deleteSVG = document.createElement("svg");
      deleteSVG.classList.add("delete");
      listItem.appendChild(deleteSVG);

      let nameTodo = document.createElement("div");
      nameTodo.classList.add("modal_name");
      nameTodo.innerHTML = todo[i].name;
      listItem.appendChild(nameTodo);

      let dateTodo = document.createElement("div");
      dateTodo.classList.add("modal_date");
      dateTodo.innerHTML = todo[i].date;
      listItem.appendChild(dateTodo);

      todoList.appendChild(list);
    }

    let data = JSON.stringify(todo);
    window.localStorage.setItem("ListRegistered", data);
  }

  function DeleteTodo() {}

  // Show Sidebar
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
});
