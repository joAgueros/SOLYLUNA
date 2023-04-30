
$(document).ready(function () {
   
    $('#muestraAdvertencia').hide();
});

$(function () {

    $("#RegistraContactoForm").submit(function (e) {

        e.preventDefault();

        var date = new Date();
      
;        /*Recoge la informacion de cada campo*/
        var nombre = $("#nomPer").val()
        var apellidos = $("#apellidos").val()
        var tel = $("#tel").val()
        var correo = $("#correo").val()
        var descripcion = $("#descripcion").val()
        var tipoContacto = "Informacion";
        var fecha = date.toLocaleDateString()
        var estado = "1"

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/

        frm.append("nombre", nombre);
        frm.append("apellidos", apellidos);
        frm.append("tel", tel);
        frm.append("correo", correo);
        frm.append("tipoContacto", tipoContacto);
        frm.append("descripcion", descripcion);
        frm.append("fecha", fecha);
        frm.append("estado", estado);
        
        checkSubmit();

        $.ajax({
            url: "/cliente/Contacto/RegistrarContacto", // Url
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
                    $('#muestraAdvertencia').show();
                    //toastr.success("Su información ha sido enviada satisfactoriamente");
                    $("#mensajeAdvertencia").text(nombre + " Pronto estaremos llamandote");
                    document.getElementById("btnSubmit").value = "Enviar nueva consulta";
                    document.getElementById("btnSubmit").disabled = false;
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(1500);
                    }, 3000);
                } else if (result.data == "Ya existe") {
                    $('#muestraAdvertencia').show();
                    $("#mensajeAdvertencia").text("Ya existe un vendedor con el número de identificación " + identificacion);
                    setTimeout(function () {
                        $("#muestraAdvertencia").fadeOut(1500);
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

function LimpiarDatos() {

    /*vaciar los campos de texto*/
    $("#nomPer").val("")
    $("#apellidos").val("")
    $("#tel").val("")
    $("#correo").val("")
    $("#descripcion").val("")
    $("#fecha").val("")
    /*cierra el modal*/
}



var statSend = false;
function checkSubmit() {
    if (!statSend) {
        statSend = true;
        return true;
    } else {
        alert("El formulario ya se esta enviando...");
        return false;
    }
}
function checkSubmit() {
    document.getElementById("btnSubmit").value = "Enviando..";
    document.getElementById("btnSubmit").disabled = true;
    return true;
}