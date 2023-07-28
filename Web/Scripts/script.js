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
    // Aqu� puedes implementar la l�gica para obtener la respuesta seg�n la pregunta
    // Puedes usar condicionales, llamadas a APIs, etc.

    // Ejemplo b�sico:
    if (question.toLowerCase().includes('hola')) {
        return '�Hola! �En qu� puedo ayudarte?';
    } else {
        return 'Lo siento, no puedo responder esa pregunta.';
    }
}


    $(document).ready(function () {
        $('#tb_empleados').DataTable();
        });



/*Esconder y cambiar Login*/

const btnSignIn = document.querySelector("#sign-in"),
    btnSignUp = document.querySelector("#sign-up"),
    formRegister = document.querySelector(".register"),
    formLogin = document.querySelector(".login");

btnSignIn.addEventListener("click", (e) => {
    formRegister.classList.add("hide");
    formLogin.classList.remove("hide");
});

btnSignUp.addEventListener("click", (e) => {
    formLogin.classList.add("hide");
    formRegister.classList.remove("hide");
});


/*logica para cargar cargarCantones*/
async function cargarCantones() {
    var provinciaId = $("#IdProvincia").val();

    if (provinciaId === "") {
        // Limpiar el DropDownList de cantones si no hay provincia seleccionada
        $("#IdCanton").empty();
    } else {
        try {
            // Realizar la solicitud AJAX para obtener los cantones
            var response = await fetch(`https://ubicaciones.paginasweb.cr/provincia/${provinciaId}/cantones.json`);
            var data = await response.json();

            // Actualizar el DropDownList de cantones con los datos obtenidos
            var cantonesSelect = $("#IdCanton");
            cantonesSelect.empty();
            $.each(data, function (idCanton, nombreCanton) {
                cantonesSelect.append($('<option></option>').val(idCanton).text(nombreCanton));
            });
        } catch (error) {
            console.log(error);
        }
    }
}

/*logica para cargar cargarDistritos*/
async function cargarDistritos() {
    var provinciaId = $("#IdProvincia").val();
    var cantonId = $("#IdCanton").val();
    if (cantonId === "") {
        $("#IdDistrito").empty();
    } else {
        try {
            var response = await fetch(`https://ubicaciones.paginasweb.cr/provincia/${provinciaId}/canton/${cantonId}/distritos.json`)
            var data = await response.json();


            var distritoSelect = $("#IdDistrito");
            distritoSelect.empty();
            $.each(data, function (IdDistrito, nombreDistrito) {
                distritoSelect.append($('<option></option>').val(IdDistrito).text(nombreDistrito));
            });
        } catch (error) {
            console.log(error);
        }
    }
}








