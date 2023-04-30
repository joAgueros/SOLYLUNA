
$(document).ready(function () {
    cargarDataCombo();
    cargarDataComboPaises();
});

/*Para poder activar un calendario en los campos para fechas*/
$("#datepicker").datepicker();

var btnFoto = document.getElementById("btnFoto");

btnFoto.onchange = function (e) {

    var reader = new FileReader();

    var file = e.target.files[0];
    reader.readAsDataURL(file);

    if (reader !=  null) {

        // Le decimos que cuando este listo ejecute el código interno
        reader.onload = function () {
            let preview = document.getElementById('preview'),
                image = document.createElement('img');

            image.id = 'imgFoto';
            image.src = reader.result;

            preview.innerHTML = '';
            preview.append(image);
        };

    }
    
}

/*Metodo que cargar un combobox a traves de ajax */
function cargarDataCombo(){

    $.ajax({
        type: "GET",
        url: "/cliente/home/ListarSexo",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombo(data.data);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function cargarDataComboPaises() {

    $.ajax({
        type: "GET",
        url: "/cliente/home/GetPaises",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarComboPaises(data.data);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}


/**
 Para que el combo box sea sensitivo (al tocar una opcion realiza una accion con el id seleccionado, por ejemplo listar algo)
 */

var comboSexo = document.getElementById("comboSexo").onchange = function() {

    //aqui puede hacer cualquier accion, como por ejm llamar un metodo y enviar el id seleccionado por parametro 
    //al metodo del controlador

    $.ajax({
        url: "/cliente/home/RecibirParametro", // Url
        data: {
            // Datos / Parámetros
            id: document.getElementById("comboSexo").value
        },
        type: "post"  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                alert(result.data);
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

}

var comboPaises = document.getElementById("comboPaises").onchange = function () {

    //aqui puede hacer cualquier accion, como por ejm llamar un metodo y enviar el id seleccionado por parametro 
    //al metodo del controlador

    $.ajax({
        url: "/cliente/home/GetProvincias", // Url
        data: {
            // Datos / Parámetros
            id: document.getElementById("comboPaises").value
        },
        type: "post"  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (data) {
            if (data != null) {
                cargarComboProvinciasEstados(data.data);
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

}

var comboProvincias = document.getElementById("comboProvinciaEstado").onchange = function () {

    //aqui puede hacer cualquier accion, como por ejm llamar un metodo y enviar el id seleccionado por parametro 
    //al metodo del controlador

    $.ajax({
        url: "/cliente/home/GetCiudades", // Url
        data: {
            // Datos / Parámetros
            id: document.getElementById("comboProvinciaEstado").value
        },
        type: "post"  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (data) {
            if (data != null) {
                cargarCiudades(data.data);
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

}


function cargarCombo(data) {

    var contenido = "<option value='0'> (Seleccione...) </option>";

    for (var i = 0; i < data.length; i++) {

        contenido += "<option value='"+data[i].idSexo+"'>";
        contenido += data[i].nombre;
        contenido += "</option>";

    }

    document.getElementById("comboSexo").innerHTML = contenido;

}

function cargarComboPaises(data) {

    var contenido = "<option value='0'> (Seleccione país...) </option>";

    for (var i = 0; i < data.length; i++) {

        contenido += "<option value='" + data[i].idPais + "'>";
        contenido += data[i].nombre;
        contenido += "</option>";

    }

    document.getElementById("comboPaises").innerHTML = contenido;

}

function cargarComboProvinciasEstados(data) {

    var contenido = "<option value='0'> (Seleccione provincia...) </option>";

    for (var i = 0; i < data.length; i++) {

        contenido += "<option value='" + data[i].idProvincia_Estado + "'>";
        contenido += data[i].nombre;
        contenido += "</option>";

    }

    document.getElementById("comboProvinciaEstado").innerHTML = contenido;

}
 
function cargarCiudades(data) {

    var contenido = "<option value='0'> (Seleccione ciudad...) </option>";

    for (var i = 0; i < data.length; i++) {

        contenido += "<option value='" + data[i].idCanton + "'>";
        contenido += data[i].nombre;
        contenido += "</option>";

    }

    document.getElementById("comboCiudad").innerHTML = contenido;

}


    $(function () {
        $("#AjaxForm").submit(function (e) {

           e.preventDefault();

            $.ajax({
                url: "/cliente/home/Post", // Url
                data: {
                    // Datos / Parámetros
                    Comentario: $("#Comentario").val(),
                    Nombre: $("#Nombre").val(),
                    Email: $("#Email").val(),
                    Titulo: $("#Titulo").val()
                },
                type: "post"  // Verbo HTTP
            })
                // Se ejecuta si todo fue bien.
                .done(function (result) {
                    if (result != null) {
                        alert(result.data);
                    }
                })
                // Se ejecuta si se produjo un error.
                .fail(function (xhr, status, error) {

                })
                // Hacer algo siempre, haya sido exitosa o no.
                .always(function () {

                });
        });
    });


/*Metodo sencillo para limpiar los controles despues de realizar una operacion, obtiene el
 * nombre de la clase de cada uno para realizar la operacion*/
function LimpiarCampos() {
    var controles = document.getElementsByClassName("borrar");

    for (var i = 0; i < controles.length; i++) {
        controles[i].value = "";
    }
}

var btnSubmitDatosPerson = document.getElementById("SubmitBtn");

btnSubmitDatosPerson.onclick = function () { AgregarDatos(); }


function AgregarDatos() {

    var nombre = document.getElementById("inputNombre").value;
    var apellidos = document.getElementById("inputApellido").value;
    var fecha = document.getElementById("datepicker").value;

    var foto = document.getElementById("imgFoto").src;

    if (foto.includes("data:image/png;base64,")) {
        foto = foto.replace("data:image/png;base64,", "");
    }
    if (foto.includes("data:image/jpeg;base64")) {
        foto = foto.replace("data:image/jpeg;base64,", "");
    }

    var frm = new FormData();

    frm.append("idPersona", 1);
    frm.append("nombre", nombre);
    frm.append("apellido", apellidos);
    frm.append("fechaNacimiento", fecha);
    frm.append("cadenaFoto", foto);

    $.ajax({
        url: "/cliente/home/GetDataPersona", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                alert(result.data);
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
};

