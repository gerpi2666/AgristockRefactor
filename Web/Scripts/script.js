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

    //themeToggler.addEventListener("click", () => {
    //    document.body.classList.toggle("dark-theme-variables");
    //});
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
                     <div class=" register w-100">
                        <div class=" w-100">
                            <div class="form-metPago-information-childs">
                                <form action="" class="form-direcion" id="formMetodoPago">
                                    <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.6rem !important; font-weight:500">Dirección de entrega</span>
                                    <hr />
                                    <div class="w-100 d-flex  flex-row justify-content-between align-items-center">
                                            <div class="d-flex flex-column pe-4">
                                                <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Provincia: </span> ${direccion.Provincia}</span>
                                                <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Cantón: </span> ${direccion.Canton}</span>
                                                <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Distrito: </span> ${direccion.Distrito}</span>
                                                <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Código postal: </span> ${direccion.CodPostal}</span>
                                                <span id="" class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Direccion exacta: </span> ${direccion.DireccionExacta}</span>

                                            </div>

                                            <span>
                                                <input type="button" value="Editar" class="button-productos mt-2" id="btnSaveDireccionForm">
                                            </span>
                                    </div>
                                    <div id="saveMetodoPagoUrl" data-url="@Url.Action("x", "x")"></div>

                                    <hr />
                                </form>
                            </div>
                        </div>
                    </div> `;
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


function cargarMeses() {
    var mesesSelect = document.getElementById("meses");

    // Guarda el valor seleccionado actualmente
    var valorSeleccionado = mesesSelect.value;

    // Limpia las opciones existentes
    while (mesesSelect.options.length > 0) {
        mesesSelect.remove(0);
    }

    // Crea y agrega las opciones de los meses del año
    for (var i = 1; i <= 12; i++) {
        var option = document.createElement("option");
        option.text = i;
        option.value = i;
        mesesSelect.add(option);
    }

    // Establece el valor seleccionado nuevamente
    mesesSelect.value = valorSeleccionado;
}


function cargarAnios() {
    var anioActual = new Date().getFullYear();
    var aniosSelect = document.getElementById("anios");

    // Guarda el valor seleccionado actualmente
    var valorSeleccionado = aniosSelect.value;

    // Limpia las opciones existentes
    while (aniosSelect.options.length > 0) {
        aniosSelect.remove(0);
    }

    // Crea y agrega las opciones de los años desde el año actual hasta 10 años en el futuro
    for (var i = anioActual; i <= anioActual + 10; i++) {
        var option = document.createElement("option");
        option.text = i;
        option.value = i;
        aniosSelect.add(option);
    }

    // Establece el valor seleccionado nuevamente
    aniosSelect.value = valorSeleccionado;
}



function saveMetodoPago() {
    // Obtener la URL del atributo personalizado
    var saveUrl = $("#saveMetodoPagoUrl").data("url");

    // Obtener los datos del formulario
    var formData = $("#formMetodoPago").serialize();

    $.ajax({
        type: "POST",
        url: saveUrl,
        data: formData,
        dataType: "json",
        success: function (data) {
            if (data.success) {
                // Actualizar el contenedor de metodo de pago con la lista de metodosPago actualizada
                var metodosPagoHtml = "";

                $.each(data.metodosPago, function (index, met) {
                    metodosPagoHtml += `
                    <div class="register w-100">
                        <div class="w-100">
                            <div class="form-metPago-information-childs">
                                <span class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.6rem !important; font-weight:500">Metodo de pago</span>
                                <hr />
                                <div class="w-100 d-flex flex-row justify-content-between align-items-center">
                                    <div class="d-flex flex-column pe-4">
                                        <span class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Tipo de pago: </span>${met.TipoPago}</span>
                                        <span class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Proveedor: </span>${met.Proveedor}</span>
                                        <span class="producto-titulo" style="color: var(--color-dark-variant) !important; font-size: 1.2rem !important;"><span style="font-weight:600">Numero de cuenta: </span>${met.NumCuenta}</span>
                                    </div>
                                    <span>
                                        <input type="button" value="Editar" class="button-productos mt-2" id="btnSaveDireccionForm">
                                    </span>
                                </div>
                                <hr />
                            </div>
                        </div>
                    </div> `;
                });

                $("#metodoPagoActualizado").html(metodosPagoHtml);

                // Limpiar el formulario después de agregar el metodoPago
                $("#formMetodoPago")[0].reset();

                // Ocultar el formulario después de agregar el metodoPago
                $("#formMetodoPago").hide();
                $("#metPago-label").hide();

                $("#mes").empty();
                $("#anios").empty();
            } else {
                alert("Error al guardar el método de pago.");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Manejo de errores si es necesario
        }
    });
}



function cargarTipoPago() {
    var tipoPagoSelect = document.getElementById("tipoPago");

    // Guarda el valor seleccionado actualmente
    var valorSeleccionado = tipoPagoSelect.value;

    // Limpia las opciones existentes
    while (tipoPagoSelect.options.length > 0) {
        tipoPagoSelect.remove(0);
    }

    // Crea y agrega las opciones de tipo de pago
    var tiposDePago = ["Tarjeta de credito", "Tarjeta de debito", "PayPal", "Criptomonedas", "Tarjeta de regalo"];

    for (var i = 0; i < tiposDePago.length; i++) {
        var option = document.createElement("option");
        option.text = tiposDePago[i];
        option.value = tiposDePago[i];
        tipoPagoSelect.add(option);
    }

    // Establece el valor seleccionado nuevamente
    tipoPagoSelect.value = valorSeleccionado;
}

