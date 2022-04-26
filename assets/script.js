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

      let okSVG = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "svg",
        "path"
      );
      let OkIconPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
      );
      okSVG.setAttribute("width", "30");
      okSVG.setAttribute("height", "30");
      okSVG.setAttribute("viewBox", "0 0 24 24");
      OkIconPath.setAttribute(
        "d",
        "M12 2c5.514 0 10 4.486 10 10s-4.486 10-10 10-10-4.486-10-10 4.486-10 10-10zm0-2c-6.627 0-12 5.373-12 12s5.373 12 12 12 12-5.373 12-12-5.373-12-12-12zm-1.959 17l-4.5-4.319 1.395-1.435 3.08 2.937 7.021-7.183 1.422 1.409-8.418 8.591z"
      );
      okSVG.classList.add("ok", "k");
      okSVG.appendChild(OkIconPath);
      listItem.appendChild(okSVG);

      let notOkSVG = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "svg"
      );
      let notOkIconPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
      );
      notOkSVG.setAttribute("width", "30");
      notOkSVG.setAttribute("height", "30");
      notOkSVG.setAttribute("viewBox", "0 0 24 24");
      notOkIconPath.setAttribute(
        "d",
        "M12 2c5.514 0 10 4.486 10 10s-4.486 10-10 10-10-4.486-10-10 4.486-10 10-10zm0-2c-6.627 0-12 5.373-12 12s5.373 12 12 12 12-5.373 12-12-5.373-12-12-12zm6 16.538l-4.592-4.548 4.546-4.587-1.416-1.403-4.545 4.589-4.588-4.543-1.405 1.405 4.593 4.552-4.547 4.592 1.405 1.405 4.555-4.596 4.591 4.55 1.403-1.416z"
      );
      notOkSVG.classList.add("notOk", "k");
      notOkSVG.appendChild(notOkIconPath);
      listItem.appendChild(notOkSVG);

      let updateSVG = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "svg"
      );
      let updateIconPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
      );
      updateSVG.setAttribute("width", "30");
      updateSVG.setAttribute("height", "30");
      updateSVG.setAttribute("viewBox", "0 0 24 24");
      updateIconPath.setAttribute(
        "d",
        "M18.363 8.464l1.433 1.431-12.67 12.669-7.125 1.436 1.439-7.127 12.665-12.668 1.431 1.431-12.255 12.224-.726 3.584 3.584-.723 12.224-12.257zm-.056-8.464l-2.815 2.817 5.691 5.692 2.817-2.821-5.693-5.688zm-12.318 18.718l11.313-11.316-.705-.707-11.313 11.314.705.709z"
      );
      updateSVG.classList.add("correct", "k");
      updateSVG.appendChild(updateIconPath);
      listItem.appendChild(updateSVG);

      let deleteSVG = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "svg"
      );
      let deleteIconPath = document.createElementNS(
        "http://www.w3.org/2000/svg",
        "path"
      );
      deleteSVG.setAttribute("width", "30");
      deleteSVG.setAttribute("height", "30");
      deleteSVG.setAttribute("viewBox", "0 0 24 24");
      deleteIconPath.setAttribute(
        "d",
        "M9 19c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm4 0c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm4 0c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm5-17v2h-20v-2h5.711c.9 0 1.631-1.099 1.631-2h5.315c0 .901.73 2 1.631 2h5.712zm-3 4v16h-14v-16h-2v18h18v-18h-2z"
      );
      deleteSVG.classList.add("delete", "k");
      deleteSVG.appendChild(deleteIconPath);
      listItem.appendChild(deleteSVG);

      let nameTodo = document.createElement("div");
      nameTodo.classList.add("modal_name");
      nameTodo.innerHTML = todo[i].name;
      listItem.appendChild(nameTodo);

      let dateTodo = document.createElement("div");
      dateTodo.classList.add("modal_date");
      dateTodo.innerHTML = todo[i].date;
      listItem.appendChild(dateTodo);

      deleteSVG.addEventListener("click", function () {
        deleteTodo();
      });

      todoList.appendChild(list);
    }

    let data = JSON.stringify(todo);
    window.localStorage.setItem("ListRegistered", data);
  }

  function deleteTodo(index) {
    if (todo.splice(index, 1)) {
      // const todos = todoList.childNodes;
      // todos.forEach(function (todo) {
      //   switch (index.value) {
      //     case "all":
      //       todo.style.display = "flex";
      //       break;
      //     case "completed":
      //       if (todo.classList.contains("completed")) {
      //         todo.style.display = "flex";
      //       } else {
      //         todo.style.display = "none";
      //       }
      //       break;
      //     case "uncompleted":
      //       if (!todo.classList.contains("completed")) {
      //         todo.style.display = "flex";
      //       } else {
      //         todo.style.display = "none";
      //       }
      //   }
      // });
    }

    RefreshTodo();
  }

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
      // content.style.transform = "translateX(-10px)";
      window.setTimeout(function () {
        hint.style.display = "none";
      }, 200); // timed to match animation-duration
    }
  });
});
