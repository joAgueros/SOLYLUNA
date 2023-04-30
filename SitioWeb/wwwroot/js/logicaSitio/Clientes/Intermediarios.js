var dataTable;

$(document).ready(function () {

    cargarDataTable();

    $('#muestraError').hide();
    $('#muestraAdvertencia').hide();

});

function LimpiarDatos() {

    /*vaciar los campos de texto*/
    $("#nombre").val("")
    $("#apellido1").val("")
    $("#apellido2").val("")
    $("#identificacion").val("")
    $("#telOficina").val("")
    $("#telMovil").val("")
    $("#telCasa").val("")
    $("#correo").val("")
    $("#direccion").val("")

    $(".text-danger").text("")
    $("#tipoIntermediario").val("0")

    /*vaciar los combos*/
    $("#idProvincia").empty();
    $("#idCanton").empty();
    $("#idDistrito").empty();
    $("#tipoPersona").empty();

    /*carrgar nuevamente el combo de provincias y tipos de identificacion*/
    cargarProvincias();
    //cargarTiposIdentificacion();
    /*cierra el modal*/
    $('#modal-lg-registrar-intermediario').modal('hide');
}

$("#btnCancelarRegistroIntermediario").click(function () {
    LimpiarDatos();
});

$("#cerrarModal").click(function () {
    LimpiarDatos();
});

$(function () {
    $("#RegistrarIntermediarioForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var tipoIntermediario = $("#tipoIntermediario").val()

        if (tipoIntermediario == "0") {
            $("#ErrorTipoIntermediario").text("Debe seleccionar el tipo de intermediario");
            return;
        }

        var tipoPersona = 1;
        /*Datos represente venta*/
        var nombre = $("#nombre").val()
        var apellido1 = $("#apellido1").val()
        var apellido2 = $("#apellido2").val()
        var identificacion = $("#identificacion").val()
        var telOficina = $("#telOficina").val()
        var telMovil = $("#telMovil").val()
        var telCasa = $("#telCasa").val()
        var correo = $("#correo").val()
        var idProvincia = $("#idProvincia").val()
        var idCanton = $("#idCanton").val()
        var idDistrito = $("#idDistrito").val()
        var direccion = $("#direccion").val()

        /* Recoge datos Juridicos */
        var nombreEntidad = "na";
        var cedulaJu = "na";
        var razonSocial = "na";
        var correoJu = "na";

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/
        frm.append("nombreEntidad", nombreEntidad);
        frm.append("cedulaJu", cedulaJu);
        frm.append("razonSocial", razonSocial);
        frm.append("correoJu", correoJu)
        frm.append("nombre", nombre);
        frm.append("apellido1", apellido1);
        frm.append("apellido2", apellido2);
        frm.append("identificacion", identificacion);
        frm.append("telefonoOficina", telOficina);
        frm.append("telefonoMovil", telMovil);
        frm.append("telefonoCasa", telCasa);
        frm.append("correoElectronico", correo);
        frm.append("idtipoIntermediario", tipoIntermediario);
        frm.append("tipoPersonaId", tipoPersona);
        frm.append("provinciaId", idProvincia);
        frm.append("cantonId", idCanton);
        frm.append("distritoId", idDistrito);
        frm.append("direccionExacta", direccion);

        $.ajax({
            url: "/admin/intermediarios/RegistrarIntermediario", // Url
            data: frm,
            contentType: false,
            processData: false,
            type: "post",  // Verbo HTTP
        })
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.data == "OK") {
                    LimpiarDatos();
                    dataTable.ajax.reload();
                    toastr.success("El registro ha sido almacenado de manera satisfactoria");

                } else if (result.data == "Ya existe") {
                    $('#muestraAdvertencia').show();
                    $("#mensajeAdvertencia").text("Ya existe una persona con el número de identificación " + identificacion);
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(1500);
                    }, 3000);
                }
                //else if (result.data == "Existe") {
                //    $('#muestraAdvertencia').show();
                //    $("#mensajeAdvertencia").text("Ya existe la entidad con la cédula " + cedulaJu);
                //    setTimeout(function () {
                //        $("#muestraAdvertencia").fadeOut(2000);
                //    }, 3000);
                //}
                else if (result.data == "Error") {
                    $('#muestraError').show();
                    $("#mensajeError").text("Ha ocurrido un error al intentar guardar el registro");
                    setTimeout(function () {
                        $("#muestraError").fadeOut(1500);
                    }, 3000);
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
                $("#mensajeError").text("Ha ocurrido un error al intentar guardar el registro");
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {

            });
    });
});

/*Esta forma de cargar provincias se hace cuando se registra un vendedor, para recargar nuevamente
 * pero a traves de un llamado Ajax, y ya no a traves del ViewModel (Con el ViewModel lo hace unicamente
 * la primera vez que carga la pagina)*/
function cargarProvincias() {

    $.ajax({
        url: "/admin/intermediarios/ObtenerProvinciasCombo", // Url
        type: "get",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (provincias) {
            cargarCombos(provincias.data, "#idProvincia");
            $("#idCanton").append('<option value="0">(Seleccione un cantón)</option>');
            $("#idDistrito").append('<option value="0">(Seleccione un distrito)</option>');
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.error(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
}

/**
 Metodo generico para cargar opciones de un combo
 */
function cargarCombos(listado, id) {
    if (listado != null) {

        $.each(listado, function (i, elem) {
            $(id).append('<option value="'
                + elem.value + '">'
                + elem.text + '</option>');
        });
    }
}

$("#idProvincia").change(function () {

    $("#idCanton").empty();
    $("#idDistrito").empty();
    $("#idDistrito").append('<option value="0">(Seleccione un distrito)</option>');

    var provinciaObtenida = $('select[name="provincias"] option:selected').text();
    var valor = $(this).val();

    if (valor == 0) {
        $("#ErrorProvincias").text("Debe seleccionar una provincia");
        $("#idCanton").append('<option value="0">(Seleccione un cantón)</option>');
        return;
    }

    $.ajax({
        url: "/admin/intermediarios/ObtenerCantonesCombo", // Url
        data: { provincia: provinciaObtenida },
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (cantones) {
            cargarCombos(cantones.data, "#idCanton");
            $("#ErrorProvincias").text("");
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.error(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

});

$("#idCanton").change(function () {

    $("#idDistrito").empty();

    var cantonObtenido = $('select[name="cantones"] option:selected').text();
    var valor = $(this).val();

    if (valor == 0) {
        $("#ErrorCantones").text("Debe seleccionar un cantón");
        $("#idDistrito").append('<option value="0">(Seleccione un distrito)</option>');
        return;
    }

    $.ajax({
        url: "/admin/intermediarios/ObtenerDistritosCombo", // Url
        data: { canton: cantonObtenido },
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (distritos) {
            cargarCombos(distritos.data, "#idDistrito");
            $("#ErrorCantones").text("");
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.error(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

});

$("#idDistrito").change(function () {

    var valor = $(this).val();

    if (valor == 0) {
        $("#ErrorDistritos").text("Debe seleccionar un distrito");
    } else {
        $("#ErrorDistritos").text("");
    }
});


function cargarDataTable() {

    dataTable = $("#tblIntermediarios").DataTable({
        "ajax": {
            "url": "/admin/intermediarios/ObtenerListaIntermediarios",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "intermediario", "width": "45%" },
            { "data": "identificacion", "width": "5%" },
            { "data": "tipoInter", "width": "5%" },
            { "data": "correo", "width": "10%" },
            { "data": "estado", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Admin/intermediarios/VerRegistroIntermediario/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fa fa-user-astronaut nav-icon'></i>Perfil
                            </a>
                            </div>`;
                }, "width": "100%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}


/*Validacion formulario*/

function email_validate(email) {
    var regMail = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/;

    if (regMail.test(email) == false) {
        document.getElementById("status").innerHTML = "<span style='color:red;'class='warning'>Agregue una dirección de correo válida.</span>";
        $('#SubmitBtn').attr("disabled", true);
    } else {
        document.getElementById("status").innerHTML = "";
        $('#SubmitBtn').attr("disabled", false);
    }

}
