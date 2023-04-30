
$(document).ready(function () {

});

$(function () {
    $("#solicitudInfoForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var nombre = $("#nombre").val()
        var correo = $("#correo").val()
        var telefono = $("#telefono").val()
        var comentario = $("#comentario").val()
        var idProp = $("#idProp").val()

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/
        frm.append("nombre", nombre);
        frm.append("correo", correo);
        frm.append("telefono", telefono);
        frm.append("comentario", comentario);
        frm.append("idProp", idProp);



        $.ajax({
            url: "/cliente/Home/EnviarSolicitud", // Url
            data: frm,
            contentType: false,
            processData: false,
            type: "post",  // Verbo HTTP
            beforeSend: function () {
                $('#btnSubmit').attr("disabled", "disabled");
            }
        })
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.data == "OK") {
                    LimpiarDatos();
                    document.getElementById("btnSubmit").disabled = false;
                    document.getElementById("respuesta").style.display = "block";
                }
                else if (result.data == "Error") {
                    //$('#muestraError').show();
                    //$("#mensajeError").text("No se ha podido enviar la solicitud");
                    //setTimeout(function () {
                    //    $("#muestraError").fadeOut(1500);
                    //}, 3000);
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
                $("#mensajeError").text("Ha ocurrido un error al intentar guardar el registro");
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {

            });

    })
});
function LimpiarDatos() {

    /*vaciar los campos de texto*/
    $("#nombre").val("")
    $("#correo").val("")
    $("#telefono").val("")
    $("#comentario").val("")

};