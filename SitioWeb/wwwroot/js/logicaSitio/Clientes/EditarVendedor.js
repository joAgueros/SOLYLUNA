$(document).ready(function () {

    $('#muestraError').hide();
    $('#muestraAdvertencia').hide();
});


$(function () {
    $("#RegistrarVendedorForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var tipoPersona = $("#tipoPersona").val()
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

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/

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

        var selectValor = $("#tipoPersona").val();

        if (selectValor == 2) {

            $("#nombreEntidad").val();
            $("#razonSocial").val();
            $("#cedulaJuridica").val();
            $("#correoJuridica").val();

            if ($("#nombreEntidad").val() === "") {
                $("#ErrorNombreEntidad").text("Debe seleccionar la entidad");
                return;
            }
            if ($("#razonSocial").val() === "") {
                $("#ErrorRazonSocial").text("Debe seleccionar la razón social");
                return;
            }
            if ($("#cedulaJuridica").val() === "") {
                $("#ErrorCedulaJuridica").text("Debe seleccionar la cédula de persona jurídica");
                return;
            }
            if ($("#correoJuridica").val() === "") {
                $("#ErrorCorreoJuridica").text("Debe seleccionar el correo de persona jurídica");
                return;
            }
        }
        else {
            $("#ErrorNombreEntidad").text("");
            $("#ErrorRazonSocial").text("");
            $("#ErrorCedulaJuridica").text("");
            $("#ErrorCorreoJuridica").text("");
        }

        $.ajax({
            url: "/admin/vendedor/RegistrarVendedor", // Url
            data: frm,
            contentType: false,
            processData: false,
            type: "post",  // Verbo HTTP
            beforeSend: function () {
                $('#SubmitBtn').attr("disabled", "disabled");
            }
        })
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.data == "OK") {
                    toastr.success("El registro ha sido actualizado de manera satisfactoria");
                } else if (result.data == "Ya existe") {
                    $('#muestraAdvertencia').show();
                    $("#mensajeAdvertencia").text("Ya existe un vendedor con el número de identificación o correo indicado");
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(1500);
                    }, 3000);
                }
                else if (result.data == "Error") {
                    $('#muestraError').show();
                    $("#mensajeError").text("Ha ocurrido un error al intentar actualizar el registro");
                    setTimeout(function () {
                        $("#muestraError").fadeOut(1500);
                    }, 3000);
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
                $("#mensajeError").text("Ha ocurrido un error al intentar actualizar el registro");
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {
                $("#SubmitBtn").removeAttr("disabled");
            });
    });
});

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
        url: "/admin/vendedor/ObtenerCantonesCombo", // Url
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
        url: "/admin/vendedor/ObtenerDistritosCombo", // Url
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

/*Esta forma de cargar provincias se hace cuando se registra un vendedor, para recargar nuevamente
 * pero a traves de un llamado Ajax, y ya no a traves del ViewModel (Con el ViewModel lo hace unicamente
 * la primera vez que carga la pagina)*/
function cargarProvincias() {

    $.ajax({
        url: "/admin/vendedor/ObtenerProvinciasCombo", // Url
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
