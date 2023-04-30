$(document).ready(function () {

    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    CargarIntencionElegida();
    CargarVistaElegida();
    ValidarPoseePozoPropiedad();

    /*mensaje advertencia y error*/
    $('#muestraAdvertencia').hide("fast", 0);
    $('#muestraError').hide("fast");

});

function CargarIntencionElegida() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/CargarIntencionElegida",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.result == "Venta") {
                    $("#radioVenta").prop("checked", true);
                    $("#radioRenta").prop("checked", false);
                } else if (data.result == "Renta") {
                    $("#radioRenta").prop("checked", true);
                    $("#radioVenta").prop("checked", false);
                }    
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function CargarVistaElegida() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/CargarVistaElegida",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {

                if (data.result == "Vista al Mar") {
                    $("#radioVistaMar").prop("checked", true);
                    $("#radioVistaMontania").prop("checked", false);
                    $("#radioVistaValle").prop("checked", false);
                    $("#radioSinVista").prop("checked", false);
                } else if (data.result == "Vista a la montaña") {
                    $("#radioVistaMar").prop("checked", false);
                    $("#radioVistaMontania").prop("checked", true);
                    $("#radioVistaValle").prop("checked", false);
                    $("#radioSinVista").prop("checked", false);
                } else if (data.result == "Vista al Valle") {
                    $("#radioVistaMar").prop("checked", false);
                    $("#radioVistaMontania").prop("checked", false);
                    $("#radioVistaValle").prop("checked", true);
                    $("#radioSinVista").prop("checked", false);
                } else if (data.result == "Sin vista") {
                    $("#radioVistaMar").prop("checked", false);
                    $("#radioVistaMontania").prop("checked", false);
                    $("#radioVistaValle").prop("checked", false);
                    $("#radioSinVista").prop("checked", true);
                }

            }
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function ValidarPoseePozoPropiedad() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/ValidarPoseePozoPropiedad",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.message == "Si") {
                    $('#ocultarOpcionesPozo').show();
                    $("#checkPoseePozo").prop('checked', true);
                }else {
                    $('#ocultarOpcionesPozo').hide("fast", 0);
                    $("#checkPoseePozo").prop('checked', false);
                }
            }
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
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

function mostrarMensajeAdvertencia() {
    $('#muestraAdvertencia').show();
    $("#mensajeAdvertencia").text("Faltan datos por completar en el formulario");
    setTimeout(function () {
        $("#muestraAdvertencia").fadeOut(1500);
    }, 2000);
}

$("#btnEditarDatosInicialesPropiedad").click(function () {
    
    /*Se recogen todos los valores del formulario */
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
    var moneda = $("#idTipoMoneda").val()
    var linkVideo = $("#linkVideo").val()
    var descripcionPropiedad = $("#descripcionPropiedad").val()

    var frm = new FormData(); /*Crea un nuevo formulario de envio*/

    var activaPoseeAgua = $("#checkPoseeAgua").prop('checked');

    if (activaPoseeAgua) {
        frm.append("disponeAgua", true);
    } else {
        frm.append("disponeAgua", false);
    } 

    frm.append("intencion", intencion);
    frm.append("tipoVista", tipoVista);
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
    frm.append("usoSueloId", idUsoSuelo);
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("linkVideo", linkVideo);
    frm.append("moneda", moneda);
    frm.append("linkVideo", linkVideo);
    frm.append("descripcionPropiedad", descripcionPropiedad);

    $.ajax({
        url: "/admin/propiedad/EditarDatosInicialesPropiedad", // Url, envia al controlador de Propiedad
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        beforeSend: function () {
            $('#btnEditarDatosInicialesPropiedad').attr("disabled", "disabled");
        }
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                if (result.success) {
                    toastr.success("El registro ha sido actualizado de manera satisfactoria");
                }
                else {
                    $('#muestraError').show();
                    $("#mensajeError").text("Ha ocurrido un error al intentar actualizar el registro : " + result.data);
                    setTimeout(function () {
                        $("#muestraError").fadeOut(1500);
                    }, 3000);
                }
            } 
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            alert(xhr + status + error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {
            $("#btnEditarDatosInicialesPropiedad").removeAttr("disabled");
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
