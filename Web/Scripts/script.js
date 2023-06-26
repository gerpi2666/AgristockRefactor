document.addEventListener("DOMContentLoaded", function () {

document.addEventListener("DOMContentLoaded", function () {
  const inputFecha = document.getElementById("fecha");
  const fechaActual = new Date();
  const fechaFormateada = fechaActual.toISOString().split("T")[0];
  inputFecha.value = fechaFormateada;
});

const sideMenu = document.querySelector("aside");
const menuBtn = document.querySelector("#menu-btn");
const closeBtn = document.querySelector("#close-btn");
const themeToggler = document.querySelector(".theme-toggler");

if (menuBtn) {
  menuBtn.addEventListener("click", () => {
    sideMenu.style.display = "block";
  });
}

if (closeBtn) {
  closeBtn.addEventListener("click", () => {
    sideMenu.style.display = "none";
  });
}

    themeToggler.addEventListener("click", () => {
        document.body.classList.toggle("dark-theme-variables");
    });
});


function sendMessage() {
    var userInput = document.getElementById('user-input');
    var chatBody = document.getElementById('chat-body');

    var question = userInput.value;
    var answer = getAnswer(question);

    var questionElement = document.createElement('p');
    questionElement.textContent = question;
    questionElement.classList.add('chat-message');

    var answerElement = document.createElement('p');
    answerElement.textContent = answer;
    answerElement.classList.add('chat-message');

    chatBody.appendChild(questionElement);
    chatBody.appendChild(answerElement);

    userInput.value = '';
}

function getAnswer(question) {
    // Aquí puedes implementar la lógica para obtener la respuesta según la pregunta
    // Puedes usar condicionales, llamadas a APIs, etc.

    // Ejemplo básico:
    if (question.toLowerCase().includes('hola')) {
        return '¡Hola! ¿En qué puedo ayudarte?';
    } else {
        return 'Lo siento, no puedo responder esa pregunta.';
    }
}



