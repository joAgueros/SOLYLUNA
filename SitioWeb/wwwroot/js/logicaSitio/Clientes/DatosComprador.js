/*JS Para compradores */
var dataTablePropiedades;
var dataTableCaracteristicasAdquiridas;
var dataTableGestionesAdquiridas;
var dataTableResultadosSugef;
var dataTableDocumentosAdquiridos;
var dataTableResultadosSolicitante;
var idPropiedadElegido;

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    cargarDataTableCaracteristicasAdquiridas();
    cargarDataTableGestionesAdquiridas();
    cargarDataTableResultadosSugef();
    cargarDataTableResultadosSolicitante();
    cargarDataTableDocumentosAdquiridos();
    cargarDataComboTipoPropiedades();
    cargarDataComboDocumentos();
    cargarDataTablePropiedades();
    cargarDataTablePropiedadesEdit();
    $('#muestraError').hide();
    $('#muestraAdvertencia').hide();
    $('#datosPersonaJuridica').hide();
    $('#codigoPropiedadElegido').hide();
});

/*Para poder activar un calendario en los campos para fechas*/
$("#fechaEntrega").datepicker();
$("#fechaEntregaEdit").datepicker();
$("#fechaVencimiento").datepicker();
$("#fechaVencimientoEdit").datepicker(); 

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

function RecargarCombosEstadoInicial(nombreMetodo, idCombo) {

    $.ajax({
        url: "/admin/comprador/" + nombreMetodo, // Url
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


function cargarDataComboTipoPropiedades() {

    $.ajax({
        type: "GET",
        url: "/admin/comprador/ObtenerTiposPropiedad",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#tipoPropiedad",);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

/*Mostrar los datos de la propiedad en la tabla */
function cargarDataTablePropiedades() {

    dataTablePropiedades = $("#tblPropiedades").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaPropiedades",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "codigoTipoUsoPropiedad", "width": "5%" },
            { "data": "tipoPropiedad", "width": "5%" },
            { "data": "nombreClienteV", "width": "5%" },
            { "data": "usoPropiedad", "width": "5%" },
            { "data": "ubicacion", "width": "30%" },
            { "data": "medidaPropiedad", "width": "5%" },
            { "data": "precioMaximo", "width": "5%" },
            { "data": "precioMinimo", "width": "10%" },
            { "data": "topografia", "width": "5%" },
            { "data": "publicado", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=AgregarPropiedad("${data}")
                                    class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa-hand'></i> Elegir 
                                </a>
                            </div>
                            `;
                }, "width": "20%"
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

function cargarDataTablePropiedadesEdit() {

    dataTablePropiedades = $("#tblPropiedadesEdit").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaPropiedades",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "codigoTipoUsoPropiedad", "width": "5%" },
            { "data": "tipoPropiedad", "width": "5%" },
            { "data": "nombreClienteV", "width": "5%" },
            { "data": "usoPropiedad", "width": "5%" },
            { "data": "ubicacion", "width": "30%" },
            { "data": "medidaPropiedad", "width": "5%" },
            { "data": "precioMaximo", "width": "5%" },
            { "data": "precioMinimo", "width": "10%" },
            { "data": "topografia", "width": "5%" },
            { "data": "publicado", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=AgregarPropiedadEdit("${data}")
                                    class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa'></i> Elegir 
                                </a>
                            </div>
                            `;
                }, "width": "20%"
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

function cargarDataTableCaracteristicasAdquiridas() {

    dataTableCaracteristicasAdquiridas = $("#tblCaracteristicasAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/comprador/ObtenerCaracteristicasPropiedadObtenida",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nompreTipoPropiedad", "width": "5%" },
            { "data": "lugar", "width": "5%" },
            { "data": "poseePropiedadEspecifica", "width": "5%" },
            { "data": "presupuesto", "width": "5%" },
            { "data": "codigoPropiedad", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=EditarCaracteristica("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-edit'></i>
                                </a>
                                <a onclick=EliminarCaracteristicaPropiedadAdquirida("/Admin/Comprador/EliminarCaracteristicaPropiedadAdquirida/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-trash-alt'></i> 
                                </a>
                            </div>`;
                }, "width": "10%"
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

function AgregarPropiedad(id) {

    idPropiedadElegido = id;
    $('#codigoPropiedadElegido').show();
    $('#codigoPropiedadElegido').show();
    $('#codigoPropiedadElegido').text(idPropiedadElegido);
    $('#tituloSeleccionePropiedad').text("Propiedad Elegida");
    $("#ErrorCodigoPropiedad").text("")
    toastr.success("Propiedad seleccionada : " + idPropiedadElegido);
}


$("#formularioCaracteristicas").submit(function (e) {

    e.preventDefault();

    if ($("#tipoPropiedad").val() == "0") {
        $("#ErrorTipoPropiedad").text("Debe seleccionar el tipo de propiedad")
        return;
    } else $("#ErrorTipoPropiedad").text("")

    if ($("#lugarCompra").val() == "") {
        $("#ErrorLugarCompra").text("Debe seleccionar el lugar de compra")
        return;
    } else $("#ErrorLugarCompra").text("")

    if ($("#lugarCompraEdit").val().length >= 50) {
        $("#ErrorObservacionAccesoEdit").text("La descripción no debe superar los 50 caracteres");
        return;
    } else $("#ErrorObservacionAccesoEdit").text("");

    if ($("#presupuesto").val() == "0" || $("#presupuesto").val() == "") {
        $("#ErrorPresupuesto").text("Debe indicar el presupuesto")
        return;
    } else $("#ErrorPresupuesto").text("")

    if ($("#codigoPropiedadElegido").text() == "") {
        $("#ErrorCodigoPropiedad").text("Debe seleccionar la propiedad")
        return;
    } else $("#ErrorCodigoPropiedad").text("")

    var frm = new FormData();
    frm.append("idTipoPropiedad", $("#tipoPropiedad").val());
    frm.append("lugar", $("#lugarCompra").val());
    frm.append("codigoPropiedad", $("#codigoPropiedadElegido").text());
    frm.append("presupuesto", $("#presupuesto").val());
    frm.append("idComprador", $("#idClienteComprador").val());

    var activa = $("#CheckPoseePropiedad").prop('checked');

    if (!activa) {
        frm.append("poseePropiedadEspecifica", "N");
    } else {
        frm.append("poseePropiedadEspecifica", "S");
    }

    $.ajax({
        url: '/admin/comprador/AgregarDatosCompraPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data == "OK") {
                LimpiarAgregadoCaracteristicas();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableCaracteristicasAdquiridas.ajax.reload();
                $("#modal-lg-agregar-caracteristicas").modal("hide");
            } else {
                toastr.error(data.message);
            }
        }
    });

});

function LimpiarAgregadoCaracteristicas() {
    idPropiedadElegido = "";
    $("#presupuesto").val("0");
    $("#lugarCompra").val("");
    $("#CheckPoseePropiedad").prop("checked", false);
    $("#tituloSeleccionePropiedad").text("Seleccione la propiedad")
    $("#tituloSeleccionePropiedad").show()
    $("#codigoPropiedadElegido").text("")
    $("#ErrorTipoPropiedad").text("")
    $("#ErrorLugarCompra").text("")
    $("#ErrorPresupuesto").text("")
    $("#ErrorCodigoPropiedad").text("")
    /*cargar nuevamente todos los combos*/
    $("#tipoPropiedad").empty();
    RecargarCombosEstadoInicial("ObtenerTiposPropiedad", "#tipoPropiedad");
}

function EliminarCaracteristicaPropiedadAdquirida(url) {

    $.ajax({
        type: 'DELETE',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableCaracteristicasAdquiridas.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarCaracteristica(id) {

    var frm = new FormData();
    frm.append("id", id);

    $.ajax({
        url: '/admin/comprador/ObtenerCaracteristicasPropiedadElegida',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarDatosCaracteristicas(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarDatosCaracteristicas(data) {

    if (data.poseePropiedadEspecifica == "Si") {
        $("#CheckPoseePropiedadEdit").prop("checked", true);
    }

    $("#tipoPropiedadEdit").empty();

    RecargarCombosEstadoInicial("ObtenerTiposPropiedad", "#tipoPropiedadEdit");

    $("#btnActivarTipoPropiedadEdit").text(data.nompreTipoPropiedad);

    $("#idTipoPropiedad").val(data.idTipoPropiedad);

    $("#tipoPropiedadEdit").hide();

    $("#btnCambiarCodigoPropiedad").show();
    $("#tituloSeleccionePropiedadEdit").text("Propiedad Elegida");
    $("#codigoPropiedadElegidoEdit").text(data.codigoPropiedad);
    idPropiedadElegido = data.codigoPropiedad;

    $("#lugarCompraEdit").val(data.lugar);
    $("#presupuestoEdit").val(data.presupuesto);
    $("#idCaracteristica").val(data.id);

    $("#modal-lg-editar-caracteristicas").modal("show");

}

$("#btnCambiarCodigoPropiedad").click(function (e) {
    $("#btnCambiarCodigoPropiedad").hide();
    $("#tituloSeleccionePropiedadEdit").text("Seleccione la propiedad");
    $("#codigoPropiedadElegidoEdit").text("");
});

$("#btnActivarTipoPropiedadEdit").click(function (e) {

    e.preventDefault();

    $("#tipoPropiedadEdit").show();
    $("#btnActivarTipoPropiedadEdit").hide();
    $("#idTipoPropiedad").val("0");

});

$("#SubmitBtnEditarCaract").click(function (e) {

    e.preventDefault();

    var idTipoPropiedad = $("#idTipoPropiedad").val();

    if (idTipoPropiedad === "0") {

        var valorCombo = $("#tipoPropiedadEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoPropiedadEdit").text("Debe seleccionar el tipo de propiedad");
            return;
        } else {
            idTipoPropiedad = $("#tipoPropiedadEdit").val();
            $("#ErrorTipoPropiedadEdit").text("");
        }
    }

    if  ($("#lugarCompraEdit").val() == "") {
        $("#ErrorLugarCompraEdit").text("Debe seleccionar el lugar");
        return;
    } else $("#ErrorLugarCompraEdit").text("");

    if ($("#lugarCompraEdit").val().length >= 50) {
        $("#ErrorLugarCompraEdit").text("La descripción no debe superar los 50 caracteres");
        return;
    } else $("#ErrorLugarCompraEdit").text("");

    if ($("#presupuestoEdit").val() == "0" || $("#presupuestoEdit").val() == "") {
        $("#ErrorPresupuestoEdit").text("Debe seleccionar el presupuesto");
        return;
    } else $("#ErrorPresupuestoEdit").text("");

    if (idPropiedadElegido == "") {
        $("#ErrorCodigoPropiedadEdit").text("Debe seleccionar la propiedad")
        return;
    } else $("#ErrorCodigoPropiedadEdit").text("")

    var frm = new FormData();
    frm.append("idTipoPropiedad", idTipoPropiedad);
    frm.append("codigoPropiedad", idPropiedadElegido);
    frm.append("lugar", $("#lugarCompraEdit").val());
    frm.append("presupuesto", $("#presupuestoEdit").val());
    frm.append("id", $("#idCaracteristica").val());

    var activa = $("#CheckPoseePropiedadEdit").prop('checked');

    if (!activa) {
        frm.append("poseePropiedadEspecifica", "N");
    } else {
        frm.append("poseePropiedadEspecifica", "S");
    }

    $.ajax({
        url: '/admin/comprador/EditarCaracteristicasPropiedadElegida',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosCaracteristica();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableCaracteristicasAdquiridas.ajax.reload();
                $("#modal-lg-editar-caracteristicas").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-caracteristicas").modal("hide");
            }
        }
    });
});

function AgregarPropiedadEdit(id) {

    idPropiedadElegido = id;
    $('#codigoPropiedadElegidoEdit').show();
    $('#codigoPropiedadElegidoEdit').text(idPropiedadElegido);
    $('#tituloSeleccionePropiedadEdit').text("Propiedad Elegida");
    $("#ErrorCodigoPropiedadEdit").text("")
    toastr.success("Propiedad seleccionada : " + idPropiedadElegido);
}

$("#btnCancelarEditarCaracteristica").click(function () {
    LimpiarDatosCaracteristica();
});

$("#cerrarModalEditarCaracteristicas").click(function () {
    LimpiarDatosCaracteristica();
});

function LimpiarDatosCaracteristica() {
    $("#lugarCompraEdit").val("");
    $("#presupuesto").val("0");
    $("#ErrorTipoPropiedadEdit").text("")
    $("#ErrorLugarCompraEdit").text("")
    $("#ErrorPresupuestoEdit").text("");
    $("#ErrorCodigoPropiedadEdit").text("");
    $("#tituloSeleccionePropiedadEdit").text("");
    idPropiedadElegido = "";
    /*cargar nuevamente el combo*/
    $("#tipoPropiedadEdit").empty();
    RecargarCombosEstadoInicial("ObtenerTiposPropiedad", "#tipoPropiedadEdit");
    /*lo oculta ya recargado*/
    $("#tipoPropiedadEdit").hide();
    /*muestra nuevamente el boton*/
    $("#btnActivarTipoPropiedadEdit").show();

}

/***************************************************************** GESTIONES ******************************************************/

$("#formularioGestiones").submit(function (e) {

    e.preventDefault();

    if ($("#descripcionGestion").val() == "") {
        $("#ErrorDescripcionGestion").text("Debe ingresar la descripción")
        return;
    } else $("#ErrorDescripcionGestion").text("")

    if ($("#fechaEntrega").val() == "") {
        $("#ErrorFechaEntregaGestion").text("Debe ingresar la fecha de entrega");
        return;
    } else $("#ErrorFechaEntregaGestion").text("");

    var frm = new FormData();
    frm.append("descripcion", $("#descripcionGestion").val());
    frm.append("fechaEntregaString", $("#fechaEntrega").val());
    frm.append("idComprador", $("#idClienteComprador").val());

    var activa = $("#CheckActivo").prop('checked');

    if (!activa) {
        frm.append("activo", "P");
    } else {
        frm.append("activo", "E");
    }
    
    $.ajax({
        url: '/admin/comprador/AgregarDatosGestion',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data == "OK") {
                LimpiarDatosGestion();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableGestionesAdquiridas.ajax.reload();
                $("#modal-lg-agregar-gestiones").modal("hide");
            } else {
                toastr.error(data.message);
            }
        }
    });

});

$("#btnCancelarAgregarGestion").click(function () {
    LimpiarDatosGestion();
});

$("#cerrarModalAgregarGestion").click(function () {
    LimpiarDatosGestion();
});

function LimpiarDatosGestion() {
    $("#CheckActivo").prop("checked", false);
    $("#descripcionGestion").val("")
    $("#fechaEntrega").val("")
    $("#ErrorDescripcionGestion").text("")
    $("#ErrorFechaEntregaGestion").text("");
}

function cargarDataTableGestionesAdquiridas() {

    dataTableGestionesAdquiridas = $("#tblGestionesAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/comprador/ObtenerGestiones",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "descripcion", "width": "50%" },
            { "data": "fechaEntregaString", "width": "5%" },
            { "data": "fechaSolicitaString", "width": "5%" },
            { "data": "activo", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=EditarGestion("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-edit'></i>
                                </a>
                                <a onclick=EliminarGestion("/Admin/Comprador/EliminarGestion/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-trash-alt'></i> 
                                </a>
                            </div>`;
                }, "width": "20%"
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

function EliminarGestion(url) {

    $.ajax({
        type: 'DELETE',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableGestionesAdquiridas.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableGestionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarGestion(id) {

    var frm = new FormData();
    frm.append("id", id);

    $.ajax({
        url: '/admin/comprador/ObtenerGestion',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarGestion(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableGestionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarGestion(data) {

    if (data.activo == "Entregado") {
        $("#CheckActivoEdit").prop("checked", true);
    }

    $("#idGestion").val(data.id);
    $("#descripcionGestionEdit").val(data.descripcion);
    $("#fechaEntregaEdit").val(data.fechaEntregaString);

    $("#modal-lg-editar-gestiones").modal("show");

}

$("#SubmitBtnAgregarGestionEdit").click(function (e) {

    e.preventDefault();

    if ($("#descripcionGestionEdit").val() == "") {
        $("#ErrorDescripcionGestionEdit").text("Debe ingresar la descripción")
        return;
    } else $("#ErrorDescripcionGestionEdit").text("")

    if ($("#fechaEntregaEdit").val() == "") {
        $("#ErrorFechaEntregaGestionEdit").text("Debe ingresar la fecha de entrega");
        return;
    } else $("#ErrorFechaEntregaGestionEdit").text("");

    var frm = new FormData();
    frm.append("descripcion", $("#descripcionGestionEdit").val());
    frm.append("fechaEntregaString", $("#fechaEntregaEdit").val());
    frm.append("id", $("#idGestion").val());

    var activa = $("#CheckActivoEdit").prop('checked');

    if (!activa) {
        frm.append("activo", "P");
    } else {
        frm.append("activo", "E");
    }

    $.ajax({
        url: '/admin/comprador/EditarGestion',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosGestionEdit();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableGestionesAdquiridas.ajax.reload();
                $("#modal-lg-editar-gestiones").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableGestionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-gestiones").modal("hide");
            }
        }
    });
});

$("#btnCancelarAgregarDivisionEdit").click(function () {
    LimpiarDatosGestionEdit();
});

$("#cerrarModalEditarGestion").click(function () {
    LimpiarDatosGestionEdit();
});

function LimpiarDatosGestionEdit() {
    $("#CheckActivoEdit").prop("checked", false);
    $("#descripcionGestionEdit").val("")
    $("#fechaEntregaEdit").val("")
    $("#ErrorDescripcionGestionEdit").text("")
    $("#ErrorFechaEntregaGestionEdit").text("");
}


/******************************************************* RESULTADO SUGEF ***************************************************/

$("#formularioSugef").submit(function (e) {

    e.preventDefault();

    if ($("#observacionResultadoSugef").val().length >= 200) {
        $("#ErrorObservacionResultadoSugef").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionResultadoSugef").text("");

    var frm = new FormData();
    frm.append("observacion", $("#observacionResultadoSugef").val());
    frm.append("idComprador", $("#idClienteComprador").val());

    var activa = $("#CheckEstadoResultado").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "C");
    }

    $.ajax({
        url: '/admin/comprador/AgregarDatosResultadoSugef',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data == "OK") {
                LimpiarDatosAgregarResultadoSugef();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableResultadosSugef.ajax.reload();
                $("#modal-lg-agregar-sugef").modal("hide");
            } else {
                toastr.error(data.message);
            }
        }
    });

});

$("#btnCancelarAgregarResultadoSugef").click(function () {
    LimpiarDatosAgregarResultadoSugef();
});

$("#cerrarModalAgregarSugef").click(function () {
    LimpiarDatosAgregarResultadoSugef();
});

function LimpiarDatosAgregarResultadoSugef() {
    $("#CheckEstadoResultado").prop("checked", false);
    $("#observacionResultadoSugef").val("");
    $("#ErrorObservacionResultadoSugef").text("")
}

function cargarDataTableResultadosSugef() {

    dataTableResultadosSugef = $("#tblResultadosSugef").DataTable({
        "ajax": {
            "url": "/admin/comprador/ObtenerResultadosSugef",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "fechaRegistroString", "width": "5%" },
            { "data": "estado", "width": "5%" },
            { "data": "observacion", "width": "50%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=EditarResultadoSugef("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-edit'></i>
                                </a>
                                <a onclick=EliminarResultadoSugef("/Admin/Comprador/EliminarResultadosSugef/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-trash-alt'></i> 
                                </a>
                            </div>`;
                }, "width": "20%"
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

function EliminarResultadoSugef(url) {

    $.ajax({
        type: 'DELETE',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableResultadosSugef.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableResultadosSugef.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarResultadoSugef(id) {

    var frm = new FormData();
    frm.append("id", id);

    $.ajax({
        url: '/admin/comprador/ObtenerResultadoSugef',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarResultadoSugef(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableResultadosSugef.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarResultadoSugef(data) {

    if (data.estado == "Califica") {
        $("#CheckEstadoResultadoEdit").prop("checked", true);
    }

    $("#idResultadoSugef").val(data.id);
    $("#observacionResultadoSugefEdit").val(data.observacion);

    $("#modal-lg-editar-sugef").modal("show");

}

$("#SubmitBtnAgregarResultadoSugefEdit").click(function (e) {

    e.preventDefault();

    if ($("#observacionResultadoSugefEdit").val().length >= 200) {
        $("#ErrorObservacionResultadoSugefEdit").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionResultadoSugefEdit").text("");

    var frm = new FormData();
    frm.append("observacion", $("#observacionResultadoSugefEdit").val());
    frm.append("idComprador", $("#idClienteComprador").val());
    frm.append("id", $("#idResultadoSugef").val());

    var activa = $("#CheckEstadoResultadoEdit").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "C");
    }

    $.ajax({
        url: '/admin/comprador/EditarResultadoSugef',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosResultadoSugefEdit();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableResultadosSugef.ajax.reload();
                $("#modal-lg-editar-sugef").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableResultadosSugef.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-sugef").modal("hide");
            }
        }
    });
});

$("#btnCancelarResultadoSugefEdit").click(function () {
    LimpiarDatosResultadoSugefEdit();
});

$("#cerrarModalEditarSugef").click(function () {
    LimpiarDatosResultadoSugefEdit();
});

function LimpiarDatosResultadoSugefEdit() {
    $("#CheckEstadoResultadoEdit").prop("checked", false);
    $("#observacionResultadoSugefEdit").val("")
    $("#ErrorObservacionResultadoSugefEdit").text("")
}


/******************************************************* RESULTADO SOLICITANTE ***************************************************/

$("#formularioSolicitante").submit(function (e) {

    e.preventDefault();

    if ($("#observacionResultadoSolicitante").val().length >= 500) {
        $("#ErrorObservacionResultadoSolicitante").text("La observación no debe superar los 500 caracteres");
        return;
    } else $("#ErrorObservacionResultadoSolicitante").text("");

    var frm = new FormData();
    frm.append("observacion", $("#observacionResultadoSolicitante").val());
    frm.append("idComprador", $("#idClienteComprador").val());

    var activa = $("#CheckEstadoResultadoSolicitante").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "P");
    }

    $.ajax({
        url: '/admin/comprador/AgregarDatosResultadoSolicitante',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data == "OK") {
                LimpiarDatosAgregarResultadoSolicitante();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableResultadosSolicitante.ajax.reload();
                $("#modal-lg-agregar-solicitante").modal("hide");
            } else {
                toastr.error(data.message);
            }
        }
    });

});

$("#btnCancelarAgregarSolicitante").click(function () {
    LimpiarDatosAgregarResultadoSolicitante();
});

$("#cerrarModalAgregarSolicitante").click(function () {
    LimpiarDatosAgregarResultadoSolicitante();
});

function LimpiarDatosAgregarResultadoSolicitante() {
    $("#CheckEstadoResultadoSolicitante").prop("checked", false);
    $("#observacionResultadoSolicitante").val("");
    $("#ErrorObservacionResultadoSolicitante").text("")
}

function cargarDataTableResultadosSolicitante() {

    dataTableResultadosSolicitante = $("#tblResultadosSolicitante").DataTable({
        "ajax": {
            "url": "/admin/comprador/ObtenerResultadosSolicitante",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "fechaRegistroString", "width": "5%" },
            { "data": "estado", "width": "5%" },
            { "data": "observacion", "width": "50%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=EditarResultadoSolicitante("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-edit'></i>
                                </a>
                                <a onclick=EliminarResultadoSolicitante("/Admin/Comprador/EliminarResultadosSolicitante/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                                <i class='fas fa-trash-alt'></i> 
                                </a>
                            </div>`;
                }, "width": "20%"
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

function EliminarResultadoSolicitante(url) {

    $.ajax({
        type: 'DELETE',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableResultadosSolicitante.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableResultadosSolicitante.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarResultadoSolicitante(id) {

    var frm = new FormData();
    frm.append("id", id);

    $.ajax({
        url: '/admin/comprador/ObtenerResultadoSolicitante',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarResultadoSolicitante(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableResultadosSolicitante.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarResultadoSolicitante(data) {

    if (data.estado == "Positivo") {
        $("#CheckEstadoResultadoSolicitanteEdit").prop("checked", true);
    }

    $("#idResultadoSolicitante").val(data.id);
    $("#observacionResultadoSolicitanteEdit").val(data.observacion);

    $("#modal-lg-editar-solicitante").modal("show");

}

$("#SubmitBtnAgregarResultadoSolicitanteEdit").click(function (e) {

    e.preventDefault();

    if ($("#observacionResultadoSolicitanteEdit").val().length >= 500) {
        $("#ErrorObservacionResultadoSolicitanteEdit").text("La observación no debe superar los 500 caracteres");
        return;
    } else $("#ErrorObservacionResultadoSolicitanteEdit").text("");

    var frm = new FormData();
    frm.append("observacion", $("#observacionResultadoSolicitanteEdit").val());
    frm.append("idComprador", $("#idClienteComprador").val());
    frm.append("id", $("#idResultadoSolicitante").val());

    var activa = $("#CheckEstadoResultadoSolicitanteEdit").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "P");
    }

    $.ajax({
        url: '/admin/comprador/EditarResultadoSolicitante',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosResultadoSugefEdit();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableResultadosSolicitante.ajax.reload();
                $("#modal-lg-editar-solicitante").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableResultadosSolicitante.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-solicitante").modal("hide");
            }
        }
    });
});

$("#btnCancelarResultadoSolicitanteEdit").click(function () {
    LimpiarDatosResultadoSolicitanteEdit();
});

$("#cerrarModalEditarSolicitante").click(function () {
    LimpiarDatosResultadoSolicitanteEdit();
});

function LimpiarDatosResultadoSolicitanteEdit() {
    $("#CheckEstadoResultadoSolicitanteEdit").prop("checked", false);
    $("#observacionResultadoSolicitanteEdit").val("")
    $("#ErrorObservacionResultadoSolicitanteEdit").text("")
}

/*********************************************************** DOCUMENTOS COMPRADOR *********************************************************/

function cargarDataComboDocumentos() {

    $.ajax({
        type: "GET",
        url: "/admin/comprador/CargarComboTiposDocumentos",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#idTipoDocumento",);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

$("#btnEnviarDatosDocumentos").click(function (e) {

    e.preventDefault();

    if ($("#idTipoDocumento").val() == "0") {
        $("#ErrorTipoDocumento").text("Debe seleccionar el tipo de documento");
        return;
    } else $("#ErrorTipoDocumento").text("");

    if ($("#fechaVencimiento").val() == "") {
        $("#ErrorFechaVencimientoDocumento").text("Debe seleccionar la fecha de vencimiento");
        return;
    } else $("#ErrorFechaVencimientoDocumento").text("");

    if ($("#notasComprador").val().length >= 200) {
        $("#ErrorNotasSituacionLegal").text("Las notas de la propiedad no debe superar los 200 caracteres");
        return;
    } else $("#ErrorNotasSituacionLegal").text("");

    var frm = new FormData();
    frm.append("idComprador", $("#idClienteComprador").val());
    frm.append("notas", $("#notasComprador").val());
    frm.append("fechaVencimientoString", $("#fechaVencimiento").val());
    frm.append("idTipoDocumento", $("#idTipoDocumento").val());

    var activa = $("#checkActivoDocumento").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "S");
    }

    $.ajax({
        url: '/admin/comprador/AgregarDatosDocumentos',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosAgregarDocumento();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableDocumentosAdquiridos.ajax.reload();
                $("#modal-lg-agregar-documento").modal("hide");
            } else {
                toastr.error("Ha ocurrido un error al agregar el registro. Asegúrate de ingresar una fecha con formato correcto : " + data.message);
            }
        }
    });
});

function cargarDataTableDocumentosAdquiridos() {

    dataTableDocumentosAdquiridos = $("#tblDocumentosAdquiridos").DataTable({
        "ajax": {
            "url": "/admin/comprador/ObtenerListadoDocumentosAdquiridos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idDocumentoComprador", "width": "5%" },
            { "data": "descripcionDocumento", "width": "10%" },
            { "data": "fechaVencimientoString", "width": "10%" },
            { "data": "estado", "width": "5%" },
            { "data": "notas", "width": "50%" },
            { "data": "fechaRegistroString", "width": "5%" },
            {
                "data": "idDocumentoComprador",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarDocumentoComprador("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarDocumentoComprador("/Admin/Comprador/EliminarDocumentoComprador/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-trash-alt'></i> 
                            </a>
                            </div>`;
                }, "width": "30%"
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

function EliminarDocumentoComprador(url) {

    $.ajax({
        type: 'DELETE',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableDocumentosAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableDocumentosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarDocumentoComprador(id) {

    var frm = new FormData();
    frm.append("idDocumentoComprador", id);

    $.ajax({
        url: '/admin/comprador/ObtenerDocumentoComprador',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarDatosDocumentoComprador(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableDocumentosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarDatosDocumentoComprador(data) {

    if (data.estado == "Si") {
        $("#checkActivoDocumentoEdit").prop("checked", true);
    }

    $("#idTipoDocumentoEdit").empty();

    RecargarCombosEstadoInicial("CargarComboTiposDocumentos", "#idTipoDocumentoEdit");

    $("#btnActivarTipoDocumentoEdit").text(data.descripcionDocumento);

    $("#idTipoDocumento").val(data.idTipoDocumento);

    $("#idTipoDocumentoEdit").hide();

    $("#notasCompradorEdit").val(data.notas);
    $("#fechaVencimientoEdit").val(data.fechaVencimientoString);
    $("#idDocumentoComprador").val(data.idDocumentoComprador);

    $("#modal-lg-editar-documento").modal("show");

}

$("#btnEnviarDatosDocumentosEdit").click(function (e) {

    var idTipoDocumento = $("#idTipoDocumento").val();

    if (idTipoDocumento === "0") {

        var valorCombo = $("#idTipoDocumentoEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoDocumentoEdit").text("Debe seleccionar el tipo de documento");
            return;
        } else {
            idTipoDocumento = $("#idTipoDocumentoEdit").val();
            $("#ErrorTipoDocumentoEdit").text("");
        }
    }

    if ($("#fechaVencimientoEdit").val() == "") {
        $("#ErrorFechaVencimientoDocumentoEdit").text("Debe seleccionar la fecha de vencimiento");
        return;
    } else $("#ErrorFechaVencimientoDocumentoEdit").text("");

    if ($("#notasCompradorEdit").val().length >= 200) {
        $("#ErrorNotasDocumentoPropiedadEdit").text("Las notas de la propiedad no debe superar los 200 caracteres");
        return;
    } else $("#ErrorNotasDocumentoPropiedadEdit").text("");

    var frm = new FormData();
    frm.append("idDocumentoComprador", $("#idDocumentoComprador").val());
    frm.append("idComprador", $("#idClienteComprador").val());
    frm.append("notas", $("#notasCompradorEdit").val());
    frm.append("idTipoDocumento", idTipoDocumento);
    frm.append("fechaVencimientoString", $("#fechaVencimientoEdit").val());

    var activa = $("#checkActivoDocumentoEdit").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "S");
    }

    $.ajax({
        url: '/admin/comprador/AgregarDatosEditadosDocumentosComprador',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosEditarDocumento();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableDocumentosAdquiridos.ajax.reload();
                $("#modal-lg-editar-documento").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta editar no existe");
                    dataTableDocumentosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-documento").modal("hide");
            }
        }
    });
});

$("#btnActivarTipoDocumentoEdit").click(function (e) {

    e.preventDefault();

    $("#idTipoDocumentoEdit").show();
    $("#btnActivarTipoDocumentoEdit").hide();
    $("#idTipoDocumento").val("0");

});

$("#cerrarModalAgregarDocumento").click(function (e) {
    LimpiarDatosAgregarDocumento();
});

$("#btnCancelarAgregarDocumento").click(function (e) {
    LimpiarDatosAgregarDocumento();
});

function LimpiarDatosAgregarDocumento() {
    $("#checkActivoDocumento").prop("checked", false);
    $("#fechaVencimiento").val("");
    $("#notasComprador").val("");
    $("#idTipoDocumento").empty();
    $("#ErrorTipoDocumento").text("");
    $("#ErrorFechaVencimientoDocumento").text("")
    $("#ErrorNotasSituacionLegal").text("");
    /*cargar nuevamente el combo*/
    RecargarCombosEstadoInicial("CargarComboTiposDocumentos", "#idTipoDocumento");
}

$("#btnCancelarEditarDocumento").click(function () {
    LimpiarDatosEditarDocumento();
});

$("#cerrarModalEditarDocumento").click(function () {
    LimpiarDatosEditarDocumento();
});

function LimpiarDatosEditarDocumento() {
    $("#checkActivoDocumentoEdit").prop("checked", false);
    $("#fechaVencimientoEdit").val("");
    $("#notasCompradorEdit").val("");
    $("#ErrorTipoDocumentoEdit").text("");
    $("#ErrorFechaVencimientoDocumentoEdit").text("");
    $("#ErrorNotasDocumentoPropiedadEdit").text("");
    /*deja vacios el combo*/
    $("#idTipoDocumentoEdit").empty();
    /*cargar nuevamente el combo*/
    RecargarCombosEstadoInicial("CargarComboTiposDocumentos", "#idTipoDocumentoEdit");
    /*lo oculta ya recargado*/
    $("#idTipoDocumentoEdit").hide();
    /*muestra nuevamente el boton*/
    $("#btnActivarTipoDocumentoEdit").show();
}
