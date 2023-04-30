

$(document).ready(function () {

    $("#ocultarFormularioEditar").hide();

});

$("#btnCancelar").click(function () {

    $("#ocultarFormularioEditar").hide();
    $("#btnEditarInformacionConstruccion").show();
})


$("#idTipoMedida").change(function () {

    var valor = $(this).val();

    if (valor == 0) {
        totalMedida
        $("#ErrorTipoMedida").text("Debe seleccionar un tipo de medida");
    }        
    else $("#ErrorTipoMedida").text("");
});

$("#idTipoVisualizacion").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoVisualizacion").text("Debe seleccionar un tipo de visualización");
    else $("#ErrorTipoVisualizacion").text("");

});

$("#idTipoCableado").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoCableado").text("Debe seleccionar un tipo de cableado");
    else $("#ErrorTipoCableado").text("");
});

$("#idTipoPeriodo").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoPeriodo").text("Debe seleccionar un periodo");
    else $("#ErrorTipoPeriodo").text("");
});

$("#idTipoEstadoFisico").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoEstadoFisico").text("Debe seleccionar el estado físico");
    else $("#ErrorTipoEstadoFisico").text("");
});


/*enviar la informacion a base de datos*/

$("#btnEditarInformacionConstruccion").click(function () {

    $("#ocultarFormularioEditar").show();
    $("#btnEditarInformacionConstruccion").hide();
})

$("#btnAgregarDatosInicialesPropiedad").click(function () {

    var idTipoMedida = $("#idTipoMedida").val()
    var idTipoVisualizacion = $("#idTipoVisualizacion").val()
    var idTipoPeriodo = $("#idTipoPeriodo").val()
    var idTipoEstadoFisico = $("#idTipoEstadoFisico").val()
    var totalMedida = $("#totalMedida").val()
    var totalPeriodo = $("#totalPeriodo").val()

    if (idTipoMedida === "0") {
        $("#ErrorTipoMedida").text("Debe seleccionar un tipo de medida");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idTipoVisualizacion === "0") {
        $("#ErrorTipoVisualizacion").text("Debe seleccionar un tipo de visualización");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idTipoPeriodo === "0") {
        $("#ErrorTipoPeriodo").text("Debe seleccionar un periodo");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idTipoEstadoFisico === "0") {
        $("#ErrorTipoEstadoFisico").text("Debe seleccionar el estado físico");
        mostrarMensajeAdvertencia()
        return;
    }
    if (totalMedida === "") {
        $("#ErrorTipoPeriodo").text("Debe indicar la cantidad para la medida");
        mostrarMensajeAdvertencia()
        return;
    }
    if (totalPeriodo === "0") {
        $("#ErrorTipoEstadoFisico").text("Debe indicar la cantidad para el periodo");
        mostrarMensajeAdvertencia()
        return;
    }  

    var estadoFisico = $('select[name="idTipoEstadoFisico"] option:selected').text();
    var periodo = $('select[name="idTipoPeriodo"] option:selected').text();

    var frm = new FormData();
    frm.append("idConstruccion", $("#idConstruccion").val());
    frm.append("idTipoMedida", idTipoMedida);
    frm.append("idTipoVisualizacion", idTipoVisualizacion);
    frm.append("idTipoPeriodo", idTipoPeriodo);
    frm.append("idTipoEstadoFisico", idTipoEstadoFisico);
    frm.append("totalMedida", totalMedida);
    frm.append("totalPeriodo", totalPeriodo);
    frm.append("periodo", periodo);
    frm.append("estadoFisico", estadoFisico);

    $.ajax({
        url: "/admin/construccion/AgregarDatosBasicosConstruccion", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                if (result.success) {
                    //LimpiarDatos();
                    $("#btnEditarInformacionConstruccion").show();
                    $("#ocultarFormularioEditar").hide();
                    toastr.success("El registro de edición de la construcción ha sido realizado de manera satisfactoria");
                } else {
                    //$('#muestraError').show();
                    //$("#mensajeError").text("Ha ocurrido un error al intentar guardar el registro");
                    //setTimeout(function () {
                    //    $("#muestraError").fadeOut(1500);
                    //}, 3000);
                }
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
});