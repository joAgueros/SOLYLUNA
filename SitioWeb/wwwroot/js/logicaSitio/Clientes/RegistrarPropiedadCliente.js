
$(document).ready(function () {

    /*oculta el loader del inicio*/
    //$('.loader').fadeOut();

    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    /*mensaje advertencia y error*/
    $('#muestraAdvertencia').hide("fast", 0);
    $('#muestraError').hide("fast");

    activarToggle(".card-body1", "#iconoColapso1");
    $('#desplegable2').hide("fast", 0);
    $('#desplegable3').hide("fast", 0);
    $('#desplegable4').hide("fast", 0);
    $('#desplegable5').hide("fast", 0);
    $('#desplegable6').hide("fast", 0);
    $('#ocultarOpcionesPozo').hide("fast", 0);
    $('#btnAgregarDatosInicialesPropiedad').hide("fast", 0);
    $('#btnCancelarRegistroPropiedad').hide("fast", 0);

});


$("#btnCancelarRegistroPropiedad").click(function () {
    LimpiarDatos();
});

/*Funciones para cambiar icono del desplegable y abrir o cerrarlo*/
$("#cerrarColapso1").click(function () {

    activarToggle(".card-body1", "#iconoColapso1");
});

$("#cerrarColapso2").click(function () {
    activarToggle(".card-body2", "#iconoColapso2");
});

$("#cerrarColapso3").click(function () {
    activarToggle(".card-body3", "#iconoColapso3");
});

$("#cerrarColapso4").click(function () {
    activarToggle(".card-body4", "#iconoColapso4");
});

$("#cerrarColapso5").click(function () {
    activarToggle(".card-body5", "#iconoColapso5");
});

$("#cerrarColapso6").click(function () {
    activarToggle(".card-body6", "#iconoColapso6");
});

/**
Metodo generico para los eventos de arriba de cada desplegable
 */
function activarToggle(contenido, elemento) {
    $(contenido).toggle();
    var respuesta = $(elemento).hasClass("fas fa-plus")
    if (respuesta) $(elemento).removeClass("fas fa-plus").addClass("fas fa-minus")
    else $(elemento).removeClass("fas fa-minus").addClass("fas fa-plus")
}

/*permite revisar el si el check fue clickeado*/
$('#checkPoseePozo').click(function () {
    if ($(this).is(':checked')) {
        $('#ocultarOpcionesPozo').show();
    } else {
        $("#idTipoPozoPropiedad").empty();
        $("#idEstatusPozoPropiedad").empty();
        RecargarCombosEstadoInicial("CargarComboTipoPozoPropiedad", "#idTipoPozoPropiedad");
        RecargarCombosEstadoInicial("CargarComboEstatusLegalPozoPropiedad", "#idEstatusPozoPropiedad");
        $('#ocultarOpcionesPozo').hide();
    }
});


/*Logica de combos*/

$("#idTipoPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoPropiedades").text("Debe seleccionar un tipo de propiedad");
    else $("#ErrorTipoPropiedades").text("");
});

$("#idTipoUsoPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoUsoPropiedades").text("Debe seleccionar el uso de la propiedad");
    else $("#ErrorTipoUsoPropiedades").text("");

});

$("#idIngresoPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoIngresoPropiedades").text("Debe seleccionar el tipo de ingreso de la propiedad");
    else $("#ErrorTipoIngresoPropiedades").text("");

});

$("#idEstadoAccesoPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorEstadoAccesoPropiedades").text("Debe seleccionar el estado de acceso de la propiedad");
    else $("#ErrorEstadoAccesoPropiedades").text("");

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
        url: "/admin/propiedad/ObtenerCantonesCombo", // Url
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
        url: "/admin/propiedad/ObtenerDistritosCombo", // Url
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

$("#idMedidasPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) {
        $("#ErrorMedidaPropiedad").text("Debe seleccionar una medida");
    } else {
        $("#ErrorMedidaPropiedad").text("");
    }
});

$("#idTipoPozoPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) {
        $("#ErrorTipoPozosPropiedad").text("Debe seleccionar el tipo de pozo");
    } else {
        $("#ErrorTipoPozosPropiedad").text("");
    }
});

$("#idEstatusPozoPropiedad").change(function () {

    var valor = $(this).val();

    if (valor == 0) {
        $("#ErrorEstatusPozosPropiedad").text("Debe seleccionar el estado legal del pozo");
    } else {
        $("#ErrorEstatusPozosPropiedad").text("");
    }
});
/*-------------------------------------------------------------------------------------------------*/

/*LLAMADOS AJAX ENVIOS AL SERVIDOR*/

$("#btnContinuar1").click(function () {

    var idTipoPropiedad = $("#idTipoPropiedad").val()
    var idUsoPropiedad = $("#idTipoUsoPropiedad").val()

    if (idTipoPropiedad === "0") {
        $("#ErrorTipoPropiedades").text("Debe seleccionar un tipo de propiedad");
        return;
    }
    if (idUsoPropiedad === "0") {
        $("#ErrorTipoUsoPropiedades").text("Debe seleccionar el uso de la propiedad");
        return;
    }

    $("#ErrorTipoUsoPropiedades").text("");
    $("#ErrorTipoPropiedades").text("");
    $(".card-body2").toggle();
    $('#desplegable2').show();
    $("#btnContinuar1").hide()

});

$("#btnContinuar2").click(function () {

    /*validar las opciones que estan seleccionadas */
    var idTipoIngresoPropiedad = $("#idIngresoPropiedad").val()
    var idUEstadoAccesoPropiedad = $("#idEstadoAccesoPropiedad").val()

    if (idTipoIngresoPropiedad === "0") {
        $("#ErrorTipoIngresoPropiedades").text("Debe seleccionar el tipo de ingreso de la propiedad");
        return;
    }
    if (idUEstadoAccesoPropiedad === "0") {
        $("#ErrorEstadoAccesoPropiedades").text("Debe seleccionar el estado de acceso de la propiedad");
        return;
    }

    $("#ErrorEstadoAccesoPropiedades").text("");
    $("#ErrorTipoIngresoPropiedades").text("");
    $(".card-body3").toggle();
    $('#desplegable3').show();
    $("#btnContinuar2").hide()

});

$("#btnContinuar3").click(function () {

    var idProvincia = $("#idProvincia").val()
    var idCanton = $("#idCanton").val()
    var idDistrito = $("#idDistrito").val()
    var direccion = $("#direccion").val()

    if (idProvincia === "0") {
        $("#ErrorProvincias").text("Debe seleccionar una provincia");
        return;
    }
    if (idCanton === "0") {
        $("#ErrorCantones").text("Debe seleccionar un cantón");
        return;
    }
    if (idDistrito === "0") {
        $("#ErrorDistritos").text("Debe seleccionar un distrito");
        return;
    }
    if (direccion === "") {
        $("#ErrorDireccion").text("Debe indicar la dirección exacta");
        return;
    }

    $("#ErrorDireccion").text("");
    $("#ErrorDistritos").text("");
    $("#ErrorCantones").text("");
    $("#ErrorProvincias").text("");
    $(".card-body4").toggle();
    $('#desplegable4').show();
    $("#btnContinuar3").hide()

});

$("#btnContinuar4").click(function () {

    var idMedida = $("#idMedidasPropiedad").val()
    var valor = $("#valorMedida").val()

    if (idMedida === "0") {
        $("#ErrorMedidaPropiedad").text("Debe seleccionar una medida");
        return;
    }
    if (valor === "") {
        $("#ErrorValorMedidaPropiedad").text("Debe indicar el valor de la medida");
        return;
    }

    $("#ErrorValorMedidaPropiedad").text("");
    $("#ErrorMedidaPropiedad").text("");
    $(".card-body5").toggle();
    $('#desplegable5').show();
    $("#btnContinuar4").hide()
});

$("#btnContinuar5").click(function () {

    var activa = $("#checkPoseePozo").prop('checked');

    if (activa) {

        var idTipoPozoPropiedad = $("#idTipoPozoPropiedad").val()
        var idEstatusPozoPropiedad = $("#idEstatusPozoPropiedad").val()

        if (idTipoPozoPropiedad === "0") {
            $("#ErrorTipoPozosPropiedad").text("Debe seleccionar el tipo de pozo");
            return;
        }
        if (idEstatusPozoPropiedad === "0") {
            $("#ErrorEstatusPozosPropiedad").text("Debe seleccionar el estado legal del pozo");
            return;
        }

    }
    else {
        $("#ErrorTipoPozosPropiedad").text("");
        $("#ErrorEstatusPozosPropiedad").text("");
    }

    $('#btnContinuar5').hide();
    $(".card-body6").toggle();
    $('#desplegable6').show();

});

$("#btnContinuar6").click(function () {

    $('#btnContinuar6').hide();
    $('#btnAgregarDatosInicialesPropiedad').show();
    $('#btnCancelarRegistroPropiedad').show("fast", 0);

});

function mostrarMensajeAdvertencia() {
    $('#muestraAdvertencia').show();
    $("#mensajeAdvertencia").text("Faltan datos por completar en el formulario");
    setTimeout(function () {
        $("#muestraAdvertencia").fadeOut(1500);
    }, 2000);
}

$("#btnAgregarDatosInicialesPropiedad").click(function () {

    /*Se recogen todos los valores del formulario */
    var idClienteVendedor = $("#idClienteVendedor").val()
    var idTipoPropiedad = $("#idTipoPropiedad").val()
    var idUsoSuelo = $("#idUsoSueloPropiedad").val()
    var idUsoPropiedad = $("#idTipoUsoPropiedad").val()
    var idTipoIngresoPropiedad = $("#idIngresoPropiedad").val()
    var idUEstadoAccesoPropiedad = $("#idEstadoAccesoPropiedad").val()
    var idProvincia = $("#idProvincia").val()
    var idCanton = $("#idCanton").val()
    var idDistrito = $("#idDistrito").val()
    var direccion = $("#direccion").val()
    var barrioPoblado = $("#barrioPoblado").val()
    var idMedida = $("#idMedidasPropiedad").val()
    var valorMedida = $("#valorMedida").val()
    var precioMaximo = $("#valorPrecioMaximo").val()
    var precioMinimo = $("#valorPrecioMinimo").val()
    var valorNumeroFinca = $("#valorNumeroFinca").val()
    var valorNumeroPlano = $("#valorNumeroPlano").val()
    var valorCuotaMantenimiento = $("#valorCuotaMantenimiento").val()
    var activaPoseePozo = $("#checkPoseePozo").prop('checked');
    var moneda = $("#idTipoMoneda").val()
    var linkVideo = $("#linkVideo").val()
    var descripcionPropiedad = $("#descripcionPropiedad").val()

    var precioMaximo = $("#valorPrecioMaximo").val()
    var precioMinimo = $("#valorPrecioMinimo").val()

/*Comienzan las validaciones de los campos obligatorios*/


    if (idTipoPropiedad === "0") {
        $("#ErrorTipoPropiedades").text("Debe seleccionar un tipo de propiedad");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idUsoPropiedad === "0") {
        $("#ErrorTipoUsoPropiedades").text("Debe seleccionar el uso de la propiedad");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idTipoIngresoPropiedad === "0") {
        $("#ErrorTipoIngresoPropiedades").text("Debe seleccionar el tipo de ingreso de la propiedad");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idUEstadoAccesoPropiedad === "0") {
        $("#ErrorEstadoAccesoPropiedades").text("Debe seleccionar el estado de acceso de la propiedad");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idProvincia === "0") {
        $("#ErrorProvincias").text("Debe seleccionar una provincia");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idCanton === "0") {
        $("#ErrorCantones").text("Debe seleccionar un cantón");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idDistrito === "0") {
        $("#ErrorDistritos").text("Debe seleccionar un distrito");
        mostrarMensajeAdvertencia()
        return;
    }
    if (direccion === "") {
        $("#ErrorDireccion").text("Debe indicar la dirección exacta");
        mostrarMensajeAdvertencia()
        return;
    }
    if (idMedida === "0") {
        $("#ErrorMedidaPropiedad").text("Debe seleccionar una medida");
        mostrarMensajeAdvertencia()
        return;
    }
    if (valorMedida === "") {
        $("#ErrorValorMedidaPropiedad").text("Debe indicar el valor de la medida");
        mostrarMensajeAdvertencia()
        return;
    }

    if (activaPoseePozo) {

        var idTipoPozoPropiedad = $("#idTipoPozoPropiedad").val()
        var idEstatusPozoPropiedad = $("#idEstatusPozoPropiedad").val()

        if (idTipoPozoPropiedad === "0") {
            $("#ErrorTipoPozosPropiedad").text("Debe seleccionar el tipo de pozo");
            mostrarMensajeAdvertencia()
            return;
        }
        if (idEstatusPozoPropiedad === "0") {
            $("#ErrorEstatusPozosPropiedad").text("Debe seleccionar el estado legal del pozo");
            mostrarMensajeAdvertencia()
            return;
        }

    }

    var tipoPozo = $('select[name="tipoPozosPropiedad"] option:selected').text();
    var estatus = $('select[name="tipoEstatusPozosPropiedad"] option:selected').text();
    var tipoAccesoPropiedad = $('select[name="tipoIngresoPropiedades"] option:selected').text();
    var estadoAccesoPropiedad = $('select[name="tipoEstadoAccesoPropiedades"] option:selected').text();
    var topografiaSeleccionada = $('select[name="tipoTopografiaPropiedad"] option:selected').text();
    var nivelCalleSeleccionado = $('select[name="nivelCallePropiedad"] option:selected').text();
    var intencion = $("input[name='radioIntencion']:checked").val();
    var tipoVista = $("input[name='radioVistada']:checked").val();
   

    var frm = new FormData(); /*Crea un nuevo formulario de envio*/

    var activaPoseeAgua = $("#checkPoseeAgua").prop('checked');

    if (activaPoseeAgua) {
        frm.append("disponeAgua", "S");
    } else {
        frm.append("disponeAgua", "N");
    }

    frm.append("intencion", intencion);
    frm.append("tipoVistada", tipoVista);
    frm.append("cuotaMantenimiento", valorCuotaMantenimiento);
    frm.append("numeroFinca", valorNumeroFinca);
    frm.append("numeroPlano", valorNumeroPlano);
    frm.append("precioMaximo", precioMaximo);
    frm.append("precioMinimo", precioMinimo);
    frm.append("topografiaSeleccionada", topografiaSeleccionada);
    frm.append("nivelCalleSeleccionada", nivelCalleSeleccionado);
    frm.append("usoPropiedadId", idUsoPropiedad);
    frm.append("tipoPropiedadId", idTipoPropiedad);
    frm.append("tipoAcceso", tipoAccesoPropiedad);
    frm.append("estadoAcceso", estadoAccesoPropiedad);
    frm.append("distritoId", idDistrito);
    frm.append("direccionExacta", direccion);
    frm.append("barrioPoblado", barrioPoblado);
    frm.append("medidasPropiedadId", idMedida);
    frm.append("totalMedida", valorMedida);
    frm.append("pozo", tipoPozo);
    frm.append("estatusPozo", estatus);
    frm.append("idClienteVendedor", idClienteVendedor);
    frm.append("usoSueloId", idUsoSuelo);
    frm.append("linkVideo", linkVideo);
    frm.append("moneda", moneda);
    frm.append("linkVideo", linkVideo);
    frm.append("descripcionPropiedad", descripcionPropiedad);

    $.ajax({
        url: "/admin/propiedad/AgregarDatosInicialesPropiedad", // Url, envia al controlador de Propiedad
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        beforeSend: function () {
            $('#btnAgregarDatosInicialesPropiedad').attr("disabled", "disabled");
        }
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                LimpiarDatos();
                toastr.success("El registro ha sido almacenado de manera satisfactoria");
                // Actualiza el resultado HTML
                $("#ListadoPropiedadesCliente").html(result);
            } else {

                $('#muestraError').show();
                $("#mensajeError").text("Ha ocurrido un error al intentar guardar el registro : " + result.data);
                setTimeout(function () {
                    $("#muestraError").fadeOut(1500);
                }, 3000);
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            alert(xhr + status + error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {
            $("#btnAgregarDatosInicialesPropiedad").removeAttr("disabled");
        });
});

/*Mètodos de carga de informacion*/

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

/**
Recargar nuevamente los combos (van al controlador de propiedad para recargar)
 */
function RecargarCombosEstadoInicial(nombreMetodo, idCombo) {

    $.ajax({
        url: "/admin/propiedad/" + nombreMetodo, // Url
        type: "get",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (data) {
            cargarCombos(data.data, idCombo);
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.error(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
}

/**s
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

function LimpiarDatos() {

    /*vaciar los campos de texto*/
    $("#valorMedida").val("0")
    $("#valorPrecioMaximo").val("0")
    $("#valorPrecioMaximo").val("0")
    $("#valorNumeroFinca").val("0")
    $("#valorNumeroPlano").val("0")
    $("#valorCuotaMantenimiento").val("0")
    $("#valorTopografia").val("")
    $("#valorNivelCalle").val("")
    $("#valorIntencion").val("")
    $("#direccion").val("")
    $("#barrioPoblado").val("")

    /*Resetear check*/
    $("#checkPoseePozo").prop('checked', false);
    $("#checkPoseeAgua").prop('checked', false);

    /*vaciar los combos*/
    $("#idProvincia").empty();
    $("#idCanton").empty();
    $("#idDistrito").empty();
    $("#idTipoPropiedad").empty();
    $("#idTipoUsoPropiedad").empty();
    $("#idIngresoPropiedad").empty();
    $("#idEstadoAccesoPropiedad").empty();
    $("#idMedidasPropiedad").empty();
    $("#idTipoPozoPropiedad").empty();
    $("#idEstatusPozoPropiedad").empty();
    $("#idUsoSueloPropiedad").empty();
    $("#linkVideo").empty();
    $("#descripcionPropiedad").empty();
  

    /*cargar nuevamente todos los combos*/
    cargarProvincias();
    RecargarCombosEstadoInicial("CargarComboTipoPropiedad", "#idTipoPropiedad");
    RecargarCombosEstadoInicial("CargarComboTipoUsoSueloPropiedad", "#idUsoSueloPropiedad");
    RecargarCombosEstadoInicial("CargarComboUsoPropiedad", "#idTipoUsoPropiedad");
    RecargarCombosEstadoInicial("CargarComboIngresoPropiedad", "#idIngresoPropiedad");
    RecargarCombosEstadoInicial("CargarComboEstadoAccesoPropiedad", "#idEstadoAccesoPropiedad");
    RecargarCombosEstadoInicial("CargarComboMedidaPropiedad", "#idMedidasPropiedad");
    RecargarCombosEstadoInicial("CargarComboTipoPozoPropiedad", "#idTipoPozoPropiedad");
    RecargarCombosEstadoInicial("CargarComboEstatusLegalPozoPropiedad", "#idEstatusPozoPropiedad");

    /*Cerrar los desplegables*/
    $(".card-body1").toggle();
    $(".card-body2").toggle();
    $(".card-body3").toggle();
    $(".card-body4").toggle();
    $(".card-body5").toggle();
    $(".card-body6").toggle();

    /*Reestablecer otros datos iniciales*/
    /*Cambiar icono desplegable 1 y desplegarlo*/
    activarToggle(".card-body1", "#iconoColapso1");
    $("#iconoColapso1").removeClass("fas fa-plus").addClass("fas fa-minus");
    $('#desplegable2').hide();
    $('#desplegable3').hide();
    $('#desplegable4').hide();
    $('#desplegable5').hide();
    $('#desplegable6').hide();
    $('#ocultarOpcionesPozo').hide();
    $('#btnAgregarDatosInicialesPropiedad').hide();
    $('#btnCancelarRegistroPropiedad').hide("fast", 0);

    /*Activar botones de continuar nuevamente*/
    $("#btnContinuar1").show()
    $("#btnContinuar2").show()
    $("#btnContinuar3").show()
    $("#btnContinuar4").show()
    $("#btnContinuar5").show()
    $("#btnContinuar6").show()

    //$('body,html').animate({ scrollTop: 0 }, 500);

}




