$(document).ready(function () {

    $('#muestraError').hide();
    $('#divExitoso').hide();
});


$(function () {
    $("#RecuperarContraseniaForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var correo = $("#email").val()

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/

        frm.append("email", correo);

        $.ajax({
            url: "/admin/account/RecoverPassword", // Url
            data: frm,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $('#SubmitBtn').attr("disabled", "disabled");
            },
            type: "post",  // Verbo HTTP
        })

            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.success) {
                    $("#SubmitBtn").removeAttr("disabled");
                    $('#divExitoso').show();
                    $("#mensajeExito").text("Exitoso. El correo ha sido enviado con las instrucciones para la recuperación de contraseña.");
                } else {
                    if (result.message == "No existe") {
                        $("#SubmitBtn").removeAttr("disabled");
                        $('#muestraError').show();
                        $("#mensajeError").text("El correo ingresado no corresponde a un usuario del sistema");
                        setTimeout(function () {
                            $("#muestraError").fadeOut(1500);
                        }, 3000);
                    }
                    if (result.message == "Error") {
                        $("#SubmitBtn").removeAttr("disabled");
                        $('#muestraError').show();
                        $("#mensajeError").text("Ha ocurrido un error al intentar recuperar la contraseña");
                        setTimeout(function () {
                            $("#muestraError").fadeOut(1500);
                        }, 3000);
                    }
                }

            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {
                $("#SubmitBtn").removeAttr("disabled");
            });
    });
});