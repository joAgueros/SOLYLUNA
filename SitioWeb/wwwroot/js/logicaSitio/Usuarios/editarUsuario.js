$(document).ready(function () {

    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    $('#muestraError').hide();
    $('#muestraAdvertencia').hide();
});


/*permite revisar el si el check fue clickeado para activar o desactivar el registro de construcción*/
$('#customSwitch3').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = true;
    } else {
        valor = false;
    }

    var frm = new FormData();
    frm.append("activa", valor);
    frm.append("correo", $("#correo").val());

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/account/CambiarEstadoAdministrador",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.success) {
                    toastr.success("La acción se ha realizado corretamente");
                }
            } else {
                toastr.success("Error al realizar la acción");
            }
        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });
});

$(function () {
    $("#EditarAdministradorForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var nombre = $("#nombre").val()
        var apellidos = $("#apellidos").val()
        var identificacion = $("#identificacion").val()
        var correo = $("#correo").val()

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/

        frm.append("nombre", nombre);
        frm.append("apellidos", apellidos);
        frm.append("identificacion", identificacion);
        frm.append("correo", correo);

        $.ajax({
            url: "/admin/account/EditarAdministrador", // Url
            data: frm,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $('#SubmitBtn').attr("disabled", "disabled");
                $('#btnCancelarRegistroAdministrador').attr("disabled", "disabled");
            },
            type: "post",  // Verbo HTTP
        })

            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.success) {
                    $("#SubmitBtn").removeAttr("disabled");
                    $("#btnCancelarRegistroAdministrador").removeAttr("disabled");
                    toastr.success("El registro ha sido editado de manera satisfactoria");
                } else {
                    $("#SubmitBtn").removeAttr("disabled");
                    $("#btnCancelarRegistroAdministrador").removeAttr("disabled");
                    $('#muestraError').show();
                    $("#mensajeError").text("Ha ocurrido un error al intentar editar el registro");
                    setTimeout(function () {
                        $("#muestraError").fadeOut(1500);
                    }, 3000);
                }

            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {
                $("#SubmitBtn").removeAttr("disabled");
                $("#btnCancelarRegistroAdministrador").removeAttr("disabled");

            });
    });
});