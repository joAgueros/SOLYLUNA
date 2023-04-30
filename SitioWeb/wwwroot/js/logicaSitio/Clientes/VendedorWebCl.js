
$(document).ready(function () {

    //cargarDataTable();

    $('#muestraError').hide();
    $('#muestraAdvertencia').hide();

    $('#tipoPersona').on('change', function () {

        var selectValor = $("#tipoPersona").val();

        if (selectValor !== '2') {
            $("#juridicoDiv").hide();
            $("#tituloRep").hide();

        } else {
            $("#juridicoDiv").show();
            $('#tituloRep').show();
        }
    });
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
    $("#cedulaJu").val("")
    $("#razonSocial").val("")
    $("#nombreEntidad").val("")
    $("#correoJu").val("")

    /*vaciar los combos*/
    $("#idProvincia").empty();
    $("#idCanton").empty();
    $("#idDistrito").empty();
    $("#tipoPersona").empty();

    /*carrgar nuevamente el combo de provincias y tipos de identificacion*/
    cargarProvincias();
    cargarTiposIdentificacion();
    /*cierra el modal*/
    $('#modal-lg-registrar-vendedor').modal('hide');
}

$("#btnCancelarRegistroVendedor").click(function () {
    LimpiarDatos();
});

$(function () {
    $("#RegistrarVendedorForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var tipoPersona = $("#idProvincia").val()
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


        if (tipoPersona === "2") {
            /* Recoge datos Juridicos */
            var nombreEntidad = $("#nombreEntidad").val()
            var cedulaJu = $("#cedulaJu").val()
            var razonSocial = $("#razonSocial").val()
            var correoJu = $("#correoJu").val()
        } else {
            /* Recoge datos Juridicos */
            var nombreEntidad = "na";
            var cedulaJu = "na";
            var razonSocial = "na";
            var correoJu = "na";
        }

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
        frm.append("tipoPersonaId", tipoPersona);
        frm.append("provinciaId", idProvincia);
        frm.append("cantonId", idCanton);
        frm.append("distritoId", idDistrito);
        frm.append("direccionExacta", direccion);

        checkSubmit()

        $.ajax({
            url: "/cliente/vendedorWeb/RegistrarVendedor", // Url
            data: frm,
            contentType: false,
            processData: false,
            type: "post",  // Verbo HTTP
        })
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.data == "OK") {
                    LimpiarDatos();
                    //dataTable.ajax.reload();
                    //toastr.success("El registro ha sido almacenado de manera satisfactoria");
                    $("#juridicoDiv").hide();
                    $("#tituloRep").hide();
                    document.getElementById("submit").value = "Enviar Informacion";
                    document.getElementById("submit").disabled = false;
                    $('#muestraAdvertencia').show();
                    $("#mensajeAdvertencia").text(nombre + " pronto te estaremos contactando ");
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(1500);
                    }, 3000);

                } else if (result.data == "Ya existe") {
                    $('#muestraAdvertencia').show();
                    $("#mensajeAdvertencia").text("Ya existe un vendedor con el número de identificación " + identificacion);
                    document.getElementById("submit").value = "Enviar Informacion";
                    document.getElementById("submit").disabled = false;
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(1500);
                    }, 3000);
                }
                else if (result.data == "Existe") {
                    $('#muestraAdvertencia').show();
                    $("#mensajeAdvertencia").text("Ya existe la entidad con la cédula " + cedulaJu);
                    document.getElementById("submit").value = "Enviar Informacion";
                    document.getElementById("submit").disabled = false;
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(2000);
                    }, 3000);
                }
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

function checkSubmit() {
    document.getElementById("submit").value = "Enviando informacion...";
    document.getElementById("submit").disabled = true;
    return true;
}


/*Esta forma de cargar provincias se hace cuando se registra un vendedor, para recargar nuevamente
 * pero a traves de un llamado Ajax, y ya no a traves del ViewModel (Con el ViewModel lo hace unicamente
 * la primera vez que carga la pagina)*/
function cargarProvincias() {

    $.ajax({
        url: "/cliente/vendedorWeb/ObtenerProvinciasCombo", // Url
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

function cargarTiposIdentificacion() {

    $.ajax({
        url: "/cliente/vendedorWeb/ObtenerTiposIdentificacionCombo", // Url
        type: "get",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (tiposPersona) {
            cargarCombos(tiposPersona.data, "#tipoPersona");
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
        url: "/cliente/vendedorWeb/ObtenerCantonesCombo", // Url
        data: { provincia: provinciaObtenida },
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (cantones) {
            cargarCombos(cantones.data, "#idCanton");
            //$("#ErrorProvincias").text("MAE ESTRO PERO NO CARGO NADA");
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
        url: "/cliente/vendedorWeb/ObtenerDistritosCombo", // Url
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
