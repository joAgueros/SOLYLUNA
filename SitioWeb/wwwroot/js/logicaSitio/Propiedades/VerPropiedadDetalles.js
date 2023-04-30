var dataTableConstrucciones;
var dataTableCaracteristicasAdquiridas;
var dataTableServMunicipalesAdquiridos;
var dataTableSituacionesLegalesAdquiridos;
var dataTableServiciosPublicosAdquiridos;
var dataTableDocumentosAdquiridos;
var dataTableAccesibilidadesAdquiridos;
var dataTableIntermediariosAdquiridos;
var dataTableIntermediarios;

$(document).ready(function () {

    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    cargarDataComboTipoCuotas();
    cargarDataComboServiciosMunicipales();
    cargarDataComboSituacionPropiedad();
    cargarDataComboServiciosPublicos();
    cargarDataComboDocumentos();
    cargarDataComboAccesibilidades();
    cargarDataTableConstrucciones();
    cargarDataTableCaracteristicasAdquiridas();
    cargarDataTableCaracteristicas();
    cargarDataTableIntermediarios();
    cargarDataTableServMunicipalesAdquiridos();
    cargarDataTableSituacionLegalAdquiridos();
    cargarDataTableServciosPublicosAdquiridos();
    cargarDataTableDocumentosAdquiridos();
    cargarDataTableAccesibilidadesAdquiridos();
    cargarDataTableIntermediariosAdquiridos();
});

/*Para poder activar un calendario en los campos para fechas*/
$("#fechaVencimiento").datepicker();
$("#fechaVencimientoEdit").datepicker(); 

/*permite revisar el si el check fue clickeado para el registro de publicacion de la propiedad*/
$('#customSwitch4').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "S";
    } else {
        valor = "N";
    }
     
    var frm = new FormData();
    frm.append("publicado", valor);
    frm.append("id", $("#idPropiedad").val());

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/propiedad/CambiarPublicadoPropiedad",
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

/*permite revisar el si el check fue clickeado para el registro de estado de la propiedad*/
$('#customSwitch3').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }

    var frm = new FormData();
    frm.append("estado", valor);
    frm.append("id", $("#idPropiedad").val());

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/propiedad/CambiarEstadoPropiedad",
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


/*Mostrar los datos de la propiedad en la tabla */
function cargarDataTableConstrucciones() {

    dataTableConstrucciones = $("#tblConstrucciones").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaConstrucciones",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idConstruccion", "width": "5%" },
            { "data": "descripcion", "width": "60%" },
            { "data": "fechaRegistra", "width": "20%" },
            { "data": "estado", "width": "15%" },
            {
                "data": "idConstruccion",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Admin/Construccion/Index/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-eye'></i> Ver </a>
                            </div>
                            `;
                }, "width": "100s%"
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

$("#btnAgregarConstruccion").click(function () {

    swal({
        title: "Agregar construcción a la propiedad",
        type: "input",
        cancelButtonText: "Cancelar",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Ingrese un nombre descriptivo para la construcción",
        confirmButtonText: "Aceptar",
        confirmButtonColor: "#DD6B55",
        closeOnconfirm: true
    },
        function (inputValue) {

            if (inputValue === false) return false;

            if (inputValue === "") {
                swal.showInputError("Debes ingresar la información en el campo de texto");
                return false
            }

            if (inputValue.length >= 50) {
                swal.showInputError("La descripción no debe contener mas de 50 caracteres");
                return false
            }

            var frm = new FormData();
            frm.append("Descripcion", inputValue)
            frm.append("idPropiedad", $("#idPropiedad").val())

            $.ajax({
                url: "/admin/propiedad/AgregarNuevaConstruccion", // Url
                data: frm,
                contentType: false,
                processData: false,
                type: "post",  // Verbo HTTP
            })
                // Se ejecuta si todo fue bien.
                .done(function (result) {
                    if (result != null) {
                        dataTableConstrucciones.ajax.reload();
                        swal("Satisfactorio", "La construcción nombrada  " + inputValue + " ha sido creada correctamente", "success");
                    }
                })
                // Se ejecuta si se produjo un error.
                .fail(function (xhr, status, error) {

                })
                // Hacer algo siempre, haya sido exitosa o no.
                .always(function () {

                });
        });
})


/** ************************************************************ CARACTERISTICAS **********************************************/

function cargarDataTableCaracteristicasAdquiridas() {

    dataTableCaracteristicasAdquiridas = $("#tblCaracteristicasAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaCaracteristicasPropiedadAdquiridas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idPropiedadCaracteristica", "width": "5%" },
            { "data": "descripcion", "width": "60%" },
            { "data": "cantidad", "width": "5%" },
            {
                "data": "idPropiedadCaracteristica",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=AumentarCaracteristicaPropiedad("/Admin/Propiedad/AumentarCaracteristicaPropiedad/${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-plus'></i>
                            </a>
                            <a onclick=DisminuirCaracteristicaPropiedad("/Admin/Propiedad/DisminuirCaracteristicaPropiedad/${data}") class='btn btn-warning text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-minus'></i> 
                            </a>
                            <a onclick=EliminarCaracteristicaPropiedad("/Admin/Propiedad/EliminarCaracteristicaPropiedad/${data}") class='btn btn-danger text-white caracteristica' style='cursor:pointer; width:50px;'>
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

function AumentarCaracteristicaPropiedad(url) {

    $.ajax({
        type: 'post',
        url: url,
        success: function (data) {
            if (data.success) {
                dataTableCaracteristicasAdquiridas.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta aumentar la cantidad no existe");
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function DisminuirCaracteristicaPropiedad(url) {

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success) {

                if (data.message == "OK") {
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                } else {
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                }
            }
            else {

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

function EliminarCaracteristicaPropiedad(url) {

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success("Característica eliminada correctamente");
                dataTableCaracteristicasAdquiridas.ajax.reload();
            }
            else {

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

function AgregarCaracteristicasPropiedad(id) {

    var frm = new FormData();

    frm.append("idCaracteristica", id);
    frm.append("idPropiedad", $("#idPropiedad").val());

    $.ajax({
        url: "/admin/propiedad/AgregarCaracteristicaPropiedad", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                if (result.data == "OK") {
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                    toastr.success("Característica agregada correctamente");
                } else {
                    toastr.success(result.data);
                }
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.success(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
}

function cargarDataTableCaracteristicas() {

    dataTableCaracteristicas = $("#tblCaracteristicas").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaCaracteristicasPropiedad",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCaracteristica", "width": "5%" },
            { "data": "descripcion", "width": "100%" },
            {
                "data": "idCaracteristica",
                "render": function (data) {
                    return `<a onclick=AgregarCaracteristicasPropiedad(${data}) class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-plus'></i>
                            </a>`;
                }, "width": "100%"
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


/* ****************************************************** SERVICIOS MUNICIPALES ***********************************************/

function cargarDataComboServiciosMunicipales() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/ObtenerListaServiciosMunicipales",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#idServMunicipal",);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function cargarDataComboTipoCuotas() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/ObtenerListaCuotas",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                cargarCombos(data.data, "#idCuota");
                cargarCombos(data.data, "#idCuotaSituacion");
            } else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function cargarDataTableServMunicipalesAdquiridos() {

    dataTableServMunicipalesAdquiridos = $("#tblServiciosMunicipalesAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListadoServiciosMunicipalesAdquiridos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idServicioMunicipal", "width": "5%" },
            { "data": "descripcionServicio", "width": "60%" },
            { "data": "costo", "width": "5%" },
            { "data": "descripcionCuota", "width": "5%" },
            { "data": "estado", "width": "5%" },
            { "data": "observacion", "width": "5%" },
            {
                "data": "idServicioMunicipal",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarServicioMunicipal("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarTipoServicioMunicipal("/Admin/Propiedad/EliminarTipoServicioMunicipal/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function EliminarTipoServicioMunicipal(url) {

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableServMunicipalesAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableServMunicipalesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarServicioMunicipal(id) {

    var frm = new FormData();
    frm.append("idServicioMunicipal", id);

    $.ajax({
        url: '/admin/propiedad/ObtenerServicioMunicipal',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {

                cargarDatosServicioMunicipal(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableServMunicipalesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarDatosServicioMunicipal(data) {

    if (data.estado == "Activo") {
        $("#checkActivoEdit").prop("checked", true);
    }

    $("#idCuotaEdit").empty();
    $("#idServMunicipalEdit").empty();

    RecargarCombosEstadoInicial("ObtenerListaCuotas", "#idCuotaEdit");
    RecargarCombosEstadoInicial("ObtenerListaServiciosMunicipales", "#idServMunicipalEdit");

    $("#btnActivarComboCuotasEdit").text(data.descripcionCuota);
    $("#btnActivarComboTipoServicioEdit").text(data.descripcionServicio);

    $("#idTipoCuotaActual").val(data.idCuota);
    $("#idTipoServMunicipalActual").val(data.idTipoServicio);

    $("#idCuotaEdit").hide();
    $("#idServMunicipalEdit").hide();

    $("#costoEdit").val(data.costo);
    $("#observacionServicioMunicipalEdit").val(data.observacion);
    $("#idServicioMunicipal").val(data.idServicioMunicipal);

    $("#modal-lg-editar-servMunicipal").modal("show");

}

$("#btnEnviarDatosServMunicipal").click(function (e) {

    e.preventDefault();

    if ($("#idCuota").val() == "0") {
        $("#ErrorTipoCuota").text("Debe seleccionar el tipo de cuota")
        return;
    } else $("#ErrorTipoCuota").text("")

    if ($("#idServMunicipal").val() == "0") {
        $("#ErrorServicioMunicipal").text("Debe seleccionar el tipo de servicio")
        return;
    } else $("#ErrorServicioMunicipal").text("")

    if ($("#costo").val() == "0" || $("#costo").val() == "") {
        $("#ErrorCostoServicioMunicipal").text("Debe indicar el costo")
        return;
    } else $("#ErrorCostoServicioMunicipal").text("")

    if ($("#observacionServicioMunicipal").val().length >= 200) {
        $("#ErrorObservacionServicioMunicipal").text("La observación no debe superar los 200 caracteres")
        return;
    } else $("#ErrorObservacionServicioMunicipal").text("")

    var frm = new FormData();
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("observacion", $("#observacionServicioMunicipal").val());
    frm.append("costo", $("#costo").val());
    frm.append("idTipoServicio", $("#idServMunicipal").val());
    frm.append("idCuota", $("#idCuota").val());

    var activa = $("#checkActivo").prop('checked');

    if (!activa) {
        frm.append("estado", "I");
    } else {
        frm.append("estado", "A");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosServiciosMunicipales',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarAgregadoServiciosMunicipales();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableServMunicipalesAdquiridos.ajax.reload();
                $("#modal-lg-agregar-servMunicipales").modal("hide");
            } else {
                toastr.error(data.message);
            }
        }
    });
});

$("#btnCancelarModalAgregarServicioMunicipal").click(function () {
    LimpiarAgregadoServiciosMunicipales();
});

$("#CerrarModalAgregarServicioMunicipal").click(function () {
    LimpiarAgregadoServiciosMunicipales();
});

function LimpiarAgregadoServiciosMunicipales() {
    $("#costo").val("0");
    $("#observacionServicioMunicipal").val("");
    $("#checkActivo").prop("checked", false);
    $("#idServMunicipal").empty();
    $("#idCuota").empty();
    $("#ErrorServicioMunicipal").text("")
    $("#ErrorTipoCuota").text("")
    $("#ErrorCostoServicioMunicipal").text("")
    $("#ErrorObservacionServicioMunicipal").text("")
    /*cargar nuevamente todos los combos*/
    RecargarCombosEstadoInicial("ObtenerListaServiciosMunicipales", "#idServMunicipal");
    RecargarCombosEstadoInicial("ObtenerListaCuotas", "#idCuota");
}

$("#btnEditarDatosServMunicipal").click(function (e) {

    e.preventDefault();

    var idTipoCuota = $("#idTipoCuotaActual").val();
    var idTipoServicio = $("#idTipoServMunicipalActual").val();

    if (idTipoCuota === "0") {

        var valorCombo = $("#idCuotaEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoCuotaEdit").text("Debe seleccionar el tipo de cuota");
            return;
        } else {
            idTipoCuota = $("#idCuotaEdit").val();
            $("#ErrorTipoCuotaEdit").text("");
        }
    }
    if (idTipoServicio === "0") {

        var valorCombo = $("#idServMunicipalEdit").val();

        if (valorCombo === "0") {
            $("#ErrorServicioMunicipalEdit").text("Debe seleccionar el tipo de servicio municipal");
            return;
        } else {
            idTipoServicio = $("#idServMunicipalEdit").val();
            $("#ErrorServicioMunicipalEdit").text("");
        }
    }

    if ($("#costoEdit").val() == "0" || $("#costoEdit").val() == "") {
        $("#ErrorCostoServicioMunicipalEdit").text("Debe indicar el costo");
        return;
    } else $("#ErrorCostoServicioMunicipalEdit").text("");

    if ($("#observacionServicioMunicipalEdit").val().length >= 200) {
        $("#ErrorObservacionServicioMunicipalEdit").text("La observación no debe superar los 200 caracteres")
        return;
    } else $("#ErrorObservacionServicioMunicipalEdit").text("")

    var activa = $("#checkActivoEdit").prop('checked');

    var frm = new FormData();
    frm.append("idServicioMunicipal", $("#idServicioMunicipal").val());
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("observacion", $("#observacionServicioMunicipalEdit").val());
    frm.append("costo", $("#costoEdit").val());
    frm.append("idTipoServicio", idTipoServicio);
    frm.append("idCuota", idTipoCuota);

    if (!activa) {
        frm.append("estado", "I");
    } else {
        frm.append("estado", "A");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosEditadosServiciosMunicipales',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosEditarServicioMunicipal();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableServMunicipalesAdquiridos.ajax.reload();
                $("#modal-lg-editar-servMunicipal").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta editar no existe");
                    dataTableServMunicipalesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-servMunicipal").modal("hide");
            }
        }
    });

});

$("#btnCancelarModalEditarServicioMunicipal").click(function () {
    LimpiarDatosEditarServicioMunicipal();
});

$("#CerrarModalEditarServicioMunicipal").click(function () {
    LimpiarDatosEditarServicioMunicipal();
});

function LimpiarDatosEditarServicioMunicipal() {
    /*deja vacios los combos*/
    $("#idServMunicipalEdit").empty();
    $("#idCuotaEdit").empty();
    $("#ErrorTipoCuotaEdit").text("");
    $("#ErrorTipoCuotaEdit").text("");
    $("#ErrorCostoServicioMunicipalEdit").text("");
    $("#ErrorObservacionServicioMunicipalEdit").text("")
    /*cargar nuevamente todos los combos*/
    RecargarCombosEstadoInicial("ObtenerListaServiciosMunicipales", "#idServMunicipalEdit");
    RecargarCombosEstadoInicial("ObtenerListaCuotas", "#idCuotaEdit");
    /*los oculta ya recargados*/
    $("#idCuotaEdit").hide();
    $("#idServMunicipalEdit").hide();
    /*muestra nuevamente los botones*/
    $("#btnActivarComboTipoServicioEdit").show();
    $("#btnActivarComboCuotasEdit").show();
}

$("#btnActivarComboCuotasEdit").click(function (e) {

    e.preventDefault();

    $("#idCuotaEdit").show();
    $("#btnActivarComboCuotasEdit").hide();
    $("#idTipoCuotaActual").val("0");

});

$("#btnActivarComboTipoServicioEdit").click(function (e) {

    e.preventDefault();

    $("#idServMunicipalEdit").show();
    $("#btnActivarComboTipoServicioEdit").hide();
    $("#idTipoServMunicipalActual").val("0");
});


/* **************************************************** SITUACIONES LEGAL PROPIEDAD *********************************************/

function cargarDataComboSituacionPropiedad() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/CargarComboSituacionesPropiedad",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#idTipoSituacion");
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

$("#btnEnviarDatosSituacion").click(function (e) {

    e.preventDefault();

    if ($("#idCuotaSituacion").val() == "0") {
        $("#ErrorTipoCuotaLegal").text("Debe seleccionar el tipo de cuota");
        return;
    } else $("#ErrorTipoCuotaLegal").text("");

    if ($("#idTipoSituacion").val() == "0") {
        $("#ErrorTipoSituacionLegal").text("Debe seleccionar el tipo de situación");
        return;
    } else $("#ErrorTipoSituacionLegal").text("");

    if ($("#monto").val() == "0" || $("#monto").val() == "") {
        $("#ErrorTipoMontoSituacioLegal").text("Debe indicar el monto");
        return;
    } else $("#ErrorTipoMontoSituacioLegal").text("");

    if ($("#nombreEntidad").val() == "") {
        $("#ErrorEntidadSituacioLegal").text("Debe indicar la entidad");
        return;
    } else $("#ErrorEntidadSituacioLegal").text("");

    if ($("#nombreEntidad").val().length >= 50) {
        $("#ErrorEntidadSituacioLegal").text("El nombre de la entidad no debe superar los 50 caracteres");
        return;
    } else $("#ErrorEntidadSituacioLegal").text("");

    if ($("#observacionSituacionLegal").val().length >= 200) {
        $("#ErrorObservacionSituacioLegal").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionSituacioLegal").text("");

    var frm = new FormData();
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("observacion", $("#observacionSituacionLegal").val());
    frm.append("nombreEntidad", $("#nombreEntidad").val());
    frm.append("monto", $("#monto").val());
    frm.append("idTipoSituacion", $("#idTipoSituacion").val());
    frm.append("idCuota", $("#idCuotaSituacion").val());

    var activa = $("#checkActivoSituacion").prop('checked');

    if (!activa) {
        frm.append("estado", "I");
    } else {
        frm.append("estado", "A");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosSituacionPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosAgregarSituacionLegal();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableSituacionesLegalesAdquiridos.ajax.reload();
                $("#modal-lg-agregar-situacionLegal").modal("hide");
            } else {
                toastr.error("Ha ocurrido un error al agregar el registro : " + data.message);
            }
        }
    });
});

$("#btnCancelarModalAgregarSituacionLegal").click(function () {
    LimpiarDatosAgregarSituacionLegal();
});

$("#cerrarModalAgregarSituacionLegal").click(function () {
    LimpiarDatosAgregarSituacionLegal();
});

function LimpiarDatosAgregarSituacionLegal() {
    $("#checkActivoSituacion").prop("checked", false);
    $("#monto").val("0");
    $("#observacionSituacionLegal").val("");
    $("#nombreEntidad").val("");
    $("#idTipoSituacion").empty();
    $("#idCuotaSituacion").empty();
    $("#ErrorTipoCuotaLegal").text("")
    $("#ErrorTipoSituacionLegal").text("")
    $("#ErrorTipoMontoSituacioLegal").text("")
    $("#ErrorEntidadSituacioLegal").text("")
    $("#ErrorObservacionSituacioLegal").text("");
    /*cargar nuevamente todos los combos*/
    RecargarCombosEstadoInicial("CargarComboSituacionesPropiedad", "#idTipoSituacion");
    RecargarCombosEstadoInicial("ObtenerListaCuotas", "#idCuotaSituacion");
}

function cargarDataTableSituacionLegalAdquiridos() {

    dataTableSituacionesLegalesAdquiridos = $("#tblSituacionLegalAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListadoSituacionLegalPropiedad",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idSituacionLegalPropiedad", "width": "5%" },
            { "data": "nombreEntidad", "width": "60%" },
            { "data": "descripcionSituacion", "width": "10%" },
            { "data": "monto", "width": "5%" },
            { "data": "descripcionCuota", "width": "5%" },
            { "data": "estado", "width": "5%" },
            { "data": "observacion", "width": "65%" },
            {
                "data": "idSituacionLegalPropiedad",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarSituacionLegal("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarTipoSituacionPropiedad("/Admin/Propiedad/EliminarTipoSituacionPropiedad/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function EliminarTipoSituacionPropiedad(url) {

    $.ajax({
        type: 'Post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableSituacionesLegalesAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableSituacionesLegalesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarSituacionLegal(id) {

    var frm = new FormData();
    frm.append("idSituacionLegalPropiedad", id);

    $.ajax({
        url: '/admin/propiedad/ObtenerSituacionLegal',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarDatosSituacionLegal(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableSituacionesLegalesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarDatosSituacionLegal(data) {

    if (data.estado == "Activo") {
        $("#checkActivoSituacionEdit").prop("checked", true);
    }

    $("#idCuotaSituacionEdit").empty();
    $("#idTipoSituacionEdit").empty();

    RecargarCombosEstadoInicial("ObtenerListaCuotas", "#idCuotaSituacionEdit");
    RecargarCombosEstadoInicial("CargarComboSituacionesPropiedad", "#idTipoSituacionEdit");

    $("#btnActivarComboCuotasSituacionEdit").text(data.descripcionCuota);
    $("#btnActivarComboTipoSituacionEdit").text(data.descripcionSituacion);

    $("#idTipoCuotaSituacionActual").val(data.idCuota);
    $("#idTipoSituacionLegalActual").val(data.idTipoSituacion);

    $("#idCuotaSituacionEdit").hide();
    $("#idTipoSituacionEdit").hide();

    $("#montoEdit").val(data.monto);
    $("#observacionSituacionLegalEdit").val(data.observacion);
    $("#nombreEntidadEdit").val(data.nombreEntidad);
    $("#idSituacionLegalPropiedad").val(data.idSituacionLegalPropiedad);

    $("#modal-lg-editar-situacionLegal").modal("show");

}

$("#btnEnviarDatosSituacionEdit").click(function (e) {

    var idTipoCuota = $("#idTipoCuotaSituacionActual").val();
    var idTipoSituacion = $("#idTipoSituacionLegalActual").val();

    if (idTipoCuota === "0") {

        var valorCombo = $("#idCuotaSituacionEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoCuotaSituacionEdit").text("Debe seleccionar el tipo de cuota");
            return;
        } else {
            idTipoCuota = $("#idCuotaSituacionEdit").val();
            $("#ErrorTipoCuotaSituacionEdit").text("");
        }
    }

    if (idTipoSituacion === "0") {

        var valorCombo = $("#idTipoSituacionEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoSituacionEdit").text("Debe seleccionar el tipo de situación");
            return;
        } else {
            idTipoSituacion = $("#idTipoSituacionEdit").val();
            $("#ErrorTipoSituacionEdit").text("");
        }
    }

    if ($("#montoEdit").val() == "0" || $("#montoEdit").val() == "") {
        $("#ErrorMontoSituacionEdit").text("Debe indicar el monto");
        return;
    } else $("#ErrorMontoSituacionEdit").text("");

    if ($("#nombreEntidadEdit").val() == "") {
        $("#ErrorNombreEntidadEdit").text("Debe indicar la entidad");
        return;
    } else $("#ErrorNombreEntidadEdit").text("");

    if ($("#nombreEntidadEdit").val().length >= 50) {
        $("#ErrorNombreEntidadEdit").text("El nombre de la entidad no debe superar los 50 caracteres");
        return;
    } else $("#ErrorNombreEntidadEdit").text("");

    if ($("#observacionSituacionLegalEdit").val().length >= 200) {
        $("#ErrorObservacionSituacionLegalEdit").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionSituacionLegalEdit").text("");

    var frm = new FormData();
    frm.append("idSituacionLegalPropiedad", $("#idSituacionLegalPropiedad").val());
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("observacion", $("#observacionSituacionLegalEdit").val());
    frm.append("nombreEntidad", $("#nombreEntidadEdit").val());
    frm.append("monto", $("#montoEdit").val());
    frm.append("idTipoSituacion", idTipoSituacion);
    frm.append("idCuota", idTipoCuota);

    var activa = $("#checkActivoSituacionEdit").prop('checked');

    if (!activa) {
        frm.append("estado", "I");
    } else {
        frm.append("estado", "A");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosEditadosSituacionPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosEditarSituacionLegal();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableSituacionesLegalesAdquiridos.ajax.reload();
                $("#modal-lg-editar-situacionLegal").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta editar no existe");
                    dataTableSituacionesLegalesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-situacionLegal").modal("hide");
            }
        }
    });
});

$("#btnActivarComboCuotasSituacionEdit").click(function (e) {

    e.preventDefault();

    $("#idCuotaSituacionEdit").show();
    $("#btnActivarComboCuotasSituacionEdit").hide();
    $("#idTipoCuotaSituacionActual").val("0");

});

$("#btnActivarComboTipoSituacionEdit").click(function (e) {

    e.preventDefault();

    $("#idTipoSituacionEdit").show();
    $("#btnActivarComboTipoSituacionEdit").hide();
    $("#idTipoSituacionLegalActual").val("0");
});

$("#btnCancelarModalEditarSituacionLegal").click(function () {
    LimpiarDatosEditarSituacionLegal();
});

$("#cerrarModalEditarSituacionLegal").click(function () {
    LimpiarDatosEditarSituacionLegal();
});

function LimpiarDatosEditarSituacionLegal() {
    $("#checkActivoSituacionEdit").prop("checked", false);
    $("#montoEdit").val("");
    $("#observacionSituacionLegalEdit").val("");
    $("#nombreEntidadEdit").val("");
    /*deja vacios los combos*/
    $("#idCuotaSituacionEdit").empty();
    $("#idTipoSituacionEdit").empty();
    $("#ErrorTipoCuotaSituacionEdit").text("")
    $("#ErrorTipoSituacionEdit").text("")
    $("#ErrorMontoSituacionEdit").text("")
    $("#ErrorNombreEntidadEdit").text("")
    $("#ErrorObservacionSituacionLegalEdit").text("");
    /*cargar nuevamente todos los combos*/
    RecargarCombosEstadoInicial("CargarComboSituacionesPropiedad", "#idTipoSituacionEdit");
    RecargarCombosEstadoInicial("ObtenerListaCuotas", "#idCuotaSituacionEdit");
    /*los oculta ya recargados*/
    $("#idCuotaSituacionEdit").hide();
    $("#idTipoSituacionEdit").hide();
    /*muestra nuevamente los botones*/
    $("#btnActivarComboCuotasSituacionEdit").show();
    $("#btnActivarComboTipoSituacionEdit").show();
}

/*************************************************  *SERVICIOS PUBLICOS **********************************************/

function cargarDataComboServiciosPublicos() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/CargarComboTiposServiciosPublicos",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#idServicioPublico",);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

$("#btnEnviarDatosServicioPublico").click(function (e) {

    e.preventDefault();

    if ($("#idServicioPublico").val() == "0") {
        $("#ErrorTipoServicioPublico").text("Debe seleccionar el tipo de servicio");
        return;
    } else $("#ErrorTipoServicioPublico").text("");

    if ($("#costoServicio").val() == "0" || $("#costoServicio").val() == "") {
        $("#ErrorCostoServicioPublico").text("Debe indicar el costo del servicio");
        return;
    } else $("#ErrorCostoServicioPublico").text("");

    if ($("#nombreEmpresa").val() == "") {
        $("#ErrorNombreEmpresa").text("Debe indicar el nombre de la empresa");
        return;
    } else $("#ErrorNombreEmpresa").text("");

    if ($("#distanciaEmpresa").val() == "0" || $("#distanciaEmpresa").val() == "") {
        $("#ErrorDistanciaServicioPublico").text("Debe indicar la distancia con respecto al servicio");
        return;
    } else $("#ErrorDistanciaServicioPublico").text("");

    /*validar la longitud de los campos observacion y nombre empresa*/
    if ($("#nombreEmpresa").val().length >= 50) {
        $("#ErrorNombreEmpresa").text("El nombre de empresa no debe superar los 50 caracteres");
        return;
    } else $("#ErrorNombreEmpresa").text("");

    if ($("#observacionServicioPublico").val().length >= 200) {
        $("#ErrorObservacionServicioPublico").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionServicioPublico").text("");

    var frm = new FormData();
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("observacion", $("#observacionServicioPublico").val());
    frm.append("empresa", $("#nombreEmpresa").val());
    frm.append("costo", $("#costoServicio").val());
    frm.append("distancia", $("#distanciaEmpresa").val());
    frm.append("idTipoServicio", $("#idServicioPublico").val());

    var activa = $("#checkActivoServicioPublico").prop('checked');

    if (!activa) {
        frm.append("estado", "I");
    } else {
        frm.append("estado", "A");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosServiciosPublicos',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarCamposAgregarServicioPublico();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableServiciosPublicosAdquiridos.ajax.reload();
                $("#modal-lg-agregar-servicioPublico").modal("hide");
            } else {
                toastr.warning("Ha ocurrido un error al agregar el registro : " + data.message);
            }
        }
    });
});

function cargarDataTableServciosPublicosAdquiridos() {

    dataTableServiciosPublicosAdquiridos = $("#tblServiciosPublicosAdquiridos").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListadoServiciosPublicosAdquiridos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idServicioPublico", "width": "5%" },
            { "data": "empresa", "width": "10%" },
            { "data": "distancia", "width": "10%" },
            { "data": "descripcionServicio", "width": "5%" },
            { "data": "costo", "width": "5%" },
            { "data": "estado", "width": "5%" },
            { "data": "observacion", "width": "65%" },
            {
                "data": "idServicioPublico",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarServicioPublico("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarTipoServicioPublico("/Admin/Propiedad/EliminarTipoServicioPublico/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function EliminarTipoServicioPublico(url) {

    $.ajax({
        type: 'Post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableServiciosPublicosAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableServiciosPublicosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EditarServicioPublico(id) {

    var frm = new FormData();
    frm.append("idServicioPublico", id);

    $.ajax({
        url: '/admin/propiedad/ObtenerServicioPublico',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarDatosServicioPublico(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableServiciosPublicosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarDatosServicioPublico(data) {

    if (data.estado == "Activo") {
        $("#checkActivoServicioPublicoEdit").prop("checked", true);
    }

    $("#idServicioPublicoEdit").empty();

    RecargarCombosEstadoInicial("CargarComboTiposServiciosPublicos", "#idServicioPublicoEdit");

    $("#btnActivarTipoServPublicoEdit").text(data.descripcionServicio);

    $("#idTipoServicioPublico").val(data.idTipoServicio);

    $("#idServicioPublicoEdit").hide();

    $("#costoServicioEdit").val(data.costo);
    $("#observacionServicioPublicoEdit").val(data.observacion);
    $("#nombreEmpresaEdit").val(data.empresa);
    $("#distanciaEmpresaEdit").val(data.distancia);
    $("#idServicioPublico").val(data.idServicioPublico);

    $("#modal-lg-editar-servicioPublico").modal("show");

}

$("#btnEnviarDatosServicioPublicoEdit").click(function (e) {

    var idTipoServicioPublico = $("#idTipoServicioPublico").val();

    if (idTipoServicioPublico === "0") {

        var valorCombo = $("#idServicioPublicoEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoServicioPublicoEdit").text("Debe seleccionar el tipo de servicio");
            return;
        } else {
            idTipoServicioPublico = $("#idServicioPublicoEdit").val();
            $("#ErrorTipoServicioPublicoEdit").text("");
        }
    }

    if ($("#nombreEmpresaEdit").val() == "") {
        $("#ErrorNombreEmpresaServicioPublicoEdit").text("Debe indicar el nombre de la empresa");
        return;
    } else $("#ErrorNombreEmpresaServicioPublicoEdit").text("");

    if ($("#costoServicioEdit").val() == "0" || $("#costoServicioEdit").val() == "") {
        $("#ErrorCostoServicioPublicoEdit").text("Debe indicar el costo del servicio");
        return;
    } else $("#ErrorCostoServicioPublicoEdit").text("");

    if ($("#distanciaEmpresaEdit").val() == "0" || $("#distanciaEmpresaEdit").val() == "") {
        $("#ErrorDistanciaEmpresaServicioPublicoEdit").text("Debe indicar la distancia con respecto al servicio");
        return;
    } else $("#ErrorDistanciaEmpresaServicioPublicoEdit").text("");

    /*validar la longitud de los campos observacion y nombre empresa*/
    if ($("#nombreEmpresaEdit").val().length >= 50) {
        $("#ErrorNombreEmpresaServicioPublicoEdit").text("El nombre de empresa no debe superar los 50 caracteres");
        return;
    } else $("#ErrorNombreEmpresaServicioPublicoEdit").text("");

    if ($("#observacionServicioPublicoEdit").val().length >= 200) {
        $("#ErrorObservacionServicioPublicoEdit").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionServicioPublicoEdit").text("");

    var activa = $("#checkActivoServicioPublicoEdit").prop('checked');

    var frm = new FormData();
    frm.append("idServicioPublico", $("#idServicioPublico").val());
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("observacion", $("#observacionServicioPublicoEdit").val());
    frm.append("empresa", $("#nombreEmpresaEdit").val());
    frm.append("costo", $("#costoServicioEdit").val());
    frm.append("idTipoServicio", idTipoServicioPublico);
    frm.append("distancia", $("#distanciaEmpresaEdit").val());

    if (!activa) {
        frm.append("estado", "I");
    } else {
        frm.append("estado", "A");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosEditadosServiciosPublicos',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosEditarServicioPublico();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableServiciosPublicosAdquiridos.ajax.reload();
                $("#modal-lg-editar-servicioPublico").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta editar no existe");
                    dataTableServiciosPublicosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-servicioPublico").modal("hide");
            }
        }
    });
});

$("#btnActivarTipoServPublicoEdit").click(function (e) {

    e.preventDefault();

    $("#idServicioPublicoEdit").show();
    $("#btnActivarTipoServPublicoEdit").hide();
    $("#idTipoServicioPublico").val("0");

});

$("#cerrarModalAgregarServicioPublico").click(function () {
    LimpiarCamposAgregarServicioPublico();
});

$("#btnCancelarModalAgregarServicioPublico").click(function () {
    LimpiarCamposAgregarServicioPublico();
});

function LimpiarCamposAgregarServicioPublico() {
    $("#costoServicio").val("0");
    $("#nombreEmpresa").val("");
    $("#distanciaEmpresa").val("0");
    $("#observacionServicioPublico").val("");
    $("#checkActivoServicioPublico").prop("checked", false);
    $("#idServicioPublico").empty();
    $("#ErrorTipoServicioPublico").text("");
    $("#ErrorNombreEmpresaServicioPublico").text("");
    $("#ErrorCostoServicioPublico").text("");
    $("#ErrorDistanciaEmpresaServicioPublico").text("");
    $("#ErrorObservacionServicioPublico").text("");
    /*cargar nuevamente todos los combos*/
    RecargarCombosEstadoInicial("CargarComboTiposServiciosPublicos", "#idServicioPublico");
}

$("#btnCancelarModalEditarServicioPublico").click(function () {
    LimpiarDatosEditarServicioPublico();
});

$("#cerrarModalEditarServicioPublico").click(function () {
    LimpiarDatosEditarServicioPublico();
});

function LimpiarDatosEditarServicioPublico() {
    $("#distanciaEmpresaEdit").val("")
    $("#observacionServicioPublicoEdit").val("")
    $("#nombreEmpresaEdit").val("")
    $("#costoServicioEdit").val("")
    $("#ErrorTipoServicioPublicoEdit").text("");
    $("#ErrorNombreEmpresaServicioPublicoEdit").text("");
    $("#ErrorCostoServicioPublicoEdit").text("");
    $("#ErrorDistanciaEmpresaServicioPublicoEdit").text("");
    $("#ErrorObservacionServicioPublicoEdit").text("");
    /*deja vacios el combo*/
    $("#idServicioPublicoEdit").empty();
    /*cargar nuevamente el combo*/
    RecargarCombosEstadoInicial("CargarComboSituacionesPropiedad", "#idServicioPublicoEdit");
    /*lo oculta ya recargado*/
    $("#idServicioPublicoEdit").hide();
    /*muestra nuevamente el boton*/
    $("#btnActivarTipoServPublicoEdit").show();
}

/****************************************************** DOCUMENTOS PROPIEDAD *****************************************************/

function cargarDataComboDocumentos() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/CargarComboTiposDocumentos",
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

    if ($("#notasPropiedad").val().length >= 200) {
        $("#ErrorNotasSituacionLegal").text("Las notas de la propiedad no debe superar los 200 caracteres");
        return;
    } else $("#ErrorNotasSituacionLegal").text("");

    var frm = new FormData();
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("notas", $("#notasPropiedad").val());
    frm.append("fechaVencimientoString", $("#fechaVencimiento").val());
    frm.append("idTipoDocumento", $("#idTipoDocumento").val());

    var activa = $("#checkActivoDocumento").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "S");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosDocumentos',
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
            "url": "/admin/propiedad/ObtenerListadoDocumentosAdquiridos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idDocumentoPropiedad", "width": "5%" },
            { "data": "descripcionDocumento", "width": "10%" },
            { "data": "fechaVencimientoString", "width": "10%" },
            { "data": "estado", "width": "5%" },
            { "data": "notas", "width": "60%" },
            { "data": "fechaRegistroString", "width": "5%" },
            {
                "data": "idDocumentoPropiedad",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarDocumentoPropiedad("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarDocumentoPropiedad("/Admin/Propiedad/EliminarDocumentoPropiedad/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function EliminarDocumentoPropiedad(url) {

    $.ajax({
        type: 'Post',
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

function EditarDocumentoPropiedad(id) {

    var frm = new FormData();
    frm.append("idDocumentoPropiedad", id);

    $.ajax({
        url: '/admin/propiedad/ObtenerDocumentoPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarDatosDocumentoPropiedad(data.data);

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

function cargarDatosDocumentoPropiedad(data) {

    if (data.estado == "Si") {
        $("#checkActivoDocumentoEdit").prop("checked", true);
    }

    $("#idTipoDocumentoEdit").empty();

    RecargarCombosEstadoInicial("CargarComboTiposDocumentos", "#idTipoDocumentoEdit");

    $("#btnActivarTipoDocumentoEdit").text(data.descripcionDocumento);

    $("#idTipoDocumento").val(data.idTipoDocumento);

    $("#idTipoDocumentoEdit").hide();

    $("#notasPropiedadEdit").val(data.notas);
    $("#fechaVencimientoEdit").val(data.fechaVencimientoString);
    $("#idDocumentoPropiedad").val(data.idDocumentoPropiedad);

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

    if ($("#notasPropiedadEdit").val().length >= 200) {
        $("#ErrorNotasDocumentoPropiedadEdit").text("Las notas de la propiedad no debe superar los 200 caracteres");
        return;
    } else $("#ErrorNotasDocumentoPropiedadEdit").text("");

    var frm = new FormData();
    frm.append("idDocumentoPropiedad", $("#idDocumentoPropiedad").val());
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("notas", $("#notasPropiedadEdit").val());
    frm.append("idTipoDocumento", idTipoDocumento);
    frm.append("fechaVencimientoString", $("#fechaVencimientoEdit").val());

    var activa = $("#checkActivoDocumentoEdit").prop('checked');

    if (!activa) {
        frm.append("estado", "N");
    } else {
        frm.append("estado", "S");
    }

    $.ajax({
        url: '/admin/propiedad/AgregarDatosEditadosDocumentosPropiedad',
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
    $("#notasPropiedad").val("");
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
    $("#notasPropiedadEdit").val("");
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


/* **********************************************************   ACCESIBILIDADES *************************************************/

function cargarDataTableAccesibilidadesAdquiridos() {

    dataTableAccesibilidadesAdquiridos = $("#tblAccesibilidades").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListadoRecorridosPropiedadAdquiridos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idRecorrido", "width": "5%" },
            { "data": "descripcionAccesibilidad", "width": "10%" },
            { "data": "recorrido", "width": "10%" },
            { "data": "descripcion", "width": "45%" },
            {
                "data": "idRecorrido",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarRecorridoPropiedad("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarRecorridoPropiedad("/Admin/Propiedad/EliminarRecorridoPropiedad/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

$("#btnEnviarAcceso").click(function (e) {

    e.preventDefault();

    if ($("#idAcceso").val() == "0") {
        $("#ErrorAcceso").text("Debe seleccionar el tipo de acceso");
        return;
    } else $("#ErrorAcceso").text("");

    if ($("#recorrido").val() == "0" || $("#recorrido").val() == "") {
        $("#ErrorRecorridoAccesoPropiedad").text("Debe seleccionar el recorrido en kilómetros");
        return;
    } else $("#ErrorRecorridoAccesoPropiedad").text("");

    if ($("#observacionAcceso").val().length >= 200) {
        $("#ErrorDescripcionAcceso").text("La descripción no debe superar los 200 caracteres");
        return;
    } else $("#ErrorDescripcionAcceso").text("");

    var frm = new FormData();
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("descripcion", $("#observacionAcceso").val());
    frm.append("idTipoAccesibilidad", $("#idAcceso").val());
    frm.append("recorrido", $("#recorrido").val());

    $.ajax({
        url: '/admin/propiedad/AgregarDatosAccesibilidades',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosAgregarRecorrido();
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableAccesibilidadesAdquiridos.ajax.reload();
                $("#modal-lg-agregar-acceso").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableAccesibilidadesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-agregar-acceso").modal("hide");
            }
        }
    });
});

function cargarDataComboAccesibilidades() {

    $.ajax({
        type: "GET",
        url: "/admin/propiedad/CargarComboAccesibilidades",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#idAcceso",);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function EliminarRecorridoPropiedad(url) {

    $.ajax({
        type: 'Post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableAccesibilidadesAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableAccesibilidadesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function cargarDatosRecorridoPropiedad(data) {

    $("#idAccesoEdit").empty();

    RecargarCombosEstadoInicial("CargarComboAccesibilidades", "#idAccesoEdit");

    $("#btnActivarTipoAccesoEdit").text(data.descripcionAccesibilidad);

    $("#idTipoAccesoEdit").val(data.idTipoAccesibilidad);

    $("#idAccesoEdit").hide();

    $("#observacionAccesoEdit").val(data.descripcion);
    $("#recorridoEdit").val(data.recorrido);
    $("#idRecorrido").val(data.idRecorrido);

    $("#modal-lg-editar-acceso").modal("show");

}

function EditarRecorridoPropiedad(id) {

    var frm = new FormData();
    frm.append("idRecorrido", id);

    $.ajax({
        url: '/admin/propiedad/ObtenerRecorridoPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.data) {

                cargarDatosRecorridoPropiedad(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableAccesibilidadesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

$("#btnActivarTipoAccesoEdit").click(function (e) {

    e.preventDefault();

    $("#idAccesoEdit").show();
    $("#btnActivarTipoAccesoEdit").hide();
    $("#idTipoAccesoEdit").val("0");

});

$("#btnEditarDatosRecorrido").click(function (e) {

    var idTipoAcceso = $("#idTipoAccesoEdit").val();

    if (idTipoAcceso === "0") {

        var valorCombo = $("#idAccesoEdit").val();

        if (valorCombo === "0") {
            $("#ErrorTipoAccesoEdit").text("Debe seleccionar el tipo de acceso");
            return;
        } else {
            idTipoAcceso = $("#idAccesoEdit").val();
            $("#ErrorTipoAccesoEdit").text("");
        }
    }

    if ($("#recorridoEdit").val() == "0" || $("#recorridoEdit").val() == "") {
        $("#ErrorRecorridoEdit").text("Debe seleccionar el recorrido en kilómetros");
        return;
    } else $("#ErrorRecorridoEdit").text("");

    if ($("#observacionAccesoEdit").val().length >= 200) {
        $("#ErrorObservacionAccesoEdit").text("La descripción no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionAccesoEdit").text("");

    var frm = new FormData();
    frm.append("idRecorrido", $("#idRecorrido").val());
    frm.append("idPropiedad", $("#idPropiedad").val());
    frm.append("descripcion", $("#observacionAccesoEdit").val());
    frm.append("idTipoAccesibilidad", idTipoAcceso);
    frm.append("recorrido", $("#recorridoEdit").val());

    $.ajax({
        url: '/admin/propiedad/AgregarDatosEditadosRecorridoPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosEditarRecorrido();
                swal("¡Excelente!", "El registro ha sido editado correctamente", "success");
                dataTableAccesibilidadesAdquiridos.ajax.reload();
                $("#modal-lg-editar-acceso").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableAccesibilidadesAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-acceso").modal("hide");
            }
        }
    });
});

$("#CerrarModalAgregarAcceso").click(function (e) {
    LimpiarDatosAgregarRecorrido();
});

$("#btnCancelarModalAgregarAcceso").click(function (e) {
    LimpiarDatosAgregarRecorrido();
});

function LimpiarDatosAgregarRecorrido() {
    $("#observacionAcceso").val("");
    $("#recorrido").val("0");
    $("#idAcceso").empty();
    $("#ErrorAcceso").text("")
    $("#ErrorRecorridoAccesoPropiedad").text("")
    $("#ErrorDescripcionAcceso").text("");
    /*cargar nuevamente el combo*/
    RecargarCombosEstadoInicial("CargarComboAccesibilidades", "#idAcceso");
}

$("#btnCancelarModalEditarRecorrido").click(function () {
    LimpiarDatosEditarRecorrido();
});

$("#CerrarModalEditarRecorrido").click(function () {
    LimpiarDatosEditarRecorrido();
});

function LimpiarDatosEditarRecorrido() {
    $("#observacionAccesoEdit").val("");
    $("#recorridoEdit").val("0");
    $("#idAccesoEdit").empty();
    $("#ErrorTipoAccesoEdit").text("")
    $("#ErrorRecorridoEdit").text("")
    $("#ErrorObservacionAccesoEdit").text("");
    /*cargar nuevamente el combo*/
    RecargarCombosEstadoInicial("CargarComboAccesibilidades", "#idAccesoEdit");
    /*lo oculta ya recargado*/
    $("#idAccesoEdit").hide();
    /*muestra nuevamente el boton*/
    $("#btnActivarTipoAccesoEdit").show();
}


/********************************************************** INTERMEDIARIOS **********************************************************/

function cargarDataTableIntermediarios() {

    dataTableIntermediarios = $("#tblIntermediarios").DataTable({
        "ajax": {
            "url": "/admin/intermediarios/ObtenerListaIntermediarios",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "intermediario", "width": "45%" },
            { "data": "identificacion", "width": "5%" },
            { "data": "tipoInter", "width": "5%" },
            { "data": "correo", "width": "10%" },
            { "data": "estado", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=AgregarIntermediario("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-plus'></i>
                            </a>
                            </div>`;
                }, "width": "100%"
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

function cargarDataTableIntermediariosAdquiridos() {

    dataTableIntermediariosAdquiridos = $("#tblIntermediariosAdquiridos").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaIntermediariosPropiedad",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "intermediario", "width": "45%" },
            { "data": "identificacion", "width": "5%" },
            { "data": "tipoInter", "width": "5%" },
            { "data": "correo", "width": "10%" },
            { "data": "estado", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center p-1">
                            <a href='/Admin/intermediarios/VerRegistroIntermediario/${data}' class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fa fa-user-astronaut nav-icon'></i>
                            </a>
                            
                            <a onclick=EliminarIntermediario("/Admin/Intermediarios/EliminarIntermediarioPropiedad/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-trash-alt'></i> 
                            </a>
                            </div>`;
                }, "width": "100%"
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

function EliminarIntermediario(url) {

    $.ajax({
        type: 'Post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableIntermediariosAdquiridos.ajax.reload();

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableIntermediariosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function AgregarIntermediario(id) {

    var frm = new FormData();
    frm.append("idIntermediario", id);
    frm.append("idPropiedad", $("#idPropiedad").val());

    $.ajax({
        url: '/Admin/Intermediarios/AgregarIntermediarioPropiedad',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                swal("¡Excelente!", "El registro ha sido agregado correctamente", "success");
                dataTableIntermediariosAdquiridos.ajax.reload();
            }
            else {
                if (data.message == "Ya existe") toastr.warning("El intermediario ya ha sido agregado anteriormente");
                else toastr.error("Ha ocurrido un error al agregar el registro : " + data.message);
            }
        }

    });
}