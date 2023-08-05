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



// custom.js


function saveDireccion() {
        // Obtener la URL del atributo personalizado
        var saveUrl = $("#saveDireccionUrl").data("url");

    // Obtener los datos del formulario
    var formData = $("#formDireccion").serialize();

    $.ajax({
        type: "POST",
    url: saveUrl,
        data: formData,
        dataType: "json",
        success: function (data) {

            if (data.success) {
                // Actualizar el contenedor de direcciones con la lista de direcciones actualizada
                var direccionesHtml = "";

                $.each(data.direcciones, function (index, direccion) {
                    direccionesHtml += `
                    

                    <div class="d-flex flex-row justify-content-between align-items-center w-100">
                        <div class="">
                                 <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.8rem !important; font-weight:500">Dirección de entrega</span>
                        </div>

                        <div class="d-flex flex-column pe-4">
                                    <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Provincia: </span> ${direccion.Provincia}</span>
                                    <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Cantón: </span> ${direccion.Canton}</span>
                                    <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Distrito: </span> ${direccion.Distrito}</span>
                                    <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Código postal: </span> ${direccion.CodPostal}</span>
                                    <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Dirección exacta: </span> ${direccion.DireccionExacta}</span>
                        </div>

                        <span>
                            <input type="button" value="Editar" class="button-productos mt-2" id="btnSaveDireccionForm">
                        </span>
                    </div>`;
                });


                    $("#direccionActualizada").html(direccionesHtml);

                    // Limpiar el formulario después de agregar la dirección
                    $("#formDireccion")[0].reset();

                    // Ocultar el formulario después de agregar la dirección
                $("#direccionForm").hide();
                $("#direccion-label").hide();

                    // Restablecer las listas desplegables de provincias, cantones y distritos a sus valores iniciales
                    $("#IdProvincia").val(""); // Opcional: aquí deberías seleccionar la provincia que desees mostrar en la lista desplegable
                    $("#IdCanton").empty();
                    $("#IdDistrito").empty();


                  } else {
                    alert("Error al guardar la dirección.");
                  }
            },
    error: function (jqXHR, textStatus, errorThrown) {
        // Manejo de errores si es necesario
    }
        });
    }









