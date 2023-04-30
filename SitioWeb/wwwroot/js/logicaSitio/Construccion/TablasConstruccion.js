﻿
var dataTableEquipamientos;
var dataTableEquipamientosAdquiridos;
var dataTableCaracteristicas;
var dataTableCaracteristicasAdquiridas;
var dataTableDivisionesAdquiridas;
var dataTableCableadosAdquiridos;

var materiales = new Array();
var cableados = new Array();

function Cableado(id, nombreDescriptivo, esEntubado, observacion, guid) {

    this.id = id;
    this.nombreDescriptivo = nombreDescriptivo;
    this.esEntubado = esEntubado;
    this.observacion = observacion;
    this.guid = guid;

    //GET
    this.getId = function () {
        return this.id;
    }
    
    this.getNombreDescriptivo = function () {
        return this.nombreDescriptivo;
    }

    this.getEsEntubado = function () {
        return this.esEntubado;
    }

    this.getObservacion = function () {
        return this.observacion;
    }
    this.getGUID = function () {
        return this.guid;
    }
}

function Material(id, nombre, descripcion) {

    this.id = id;
    this.nombre = nombre;
    this.descripcion = descripcion;

    //GET
    this.getId = function () {
        return this.id;
    }

    this.getNombre = function () {
        return this.nombre;
    }

    this.getDescripcion = function () {
        return this.descripcion;
    }
}

function MaterialEditar(idMaterial, nombre, descripcion, idDivisionMaterial, idDivision, idConstruccionDivision) {

    this.idMaterial = idMaterial;
    this.nombre = nombre;
    this.descripcion = descripcion;
    this.idDivisionMaterial = idDivisionMaterial;
    this.idDivision = idDivision;
    this.idConstruccionDivision = idConstruccionDivision;

    //GET
    this.getId = function () {
        return this.idMaterial;
    }

    this.getNombre = function () {
        return this.nombre;
    }

    this.getDescripcion = function () {
        return this.descripcion;
    }

    this.getIdDivisionMaterial = function () {
        return this.idDivisionMaterial;
    }

    this.getIdDivision = function () {
        return this.idDivision;
    }

    this.getIdConstruccionDivision = function () {
        return this.idConstruccionDivision;
    }
}

$(document).ready(function () {

    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    cargarDataTableEquipamientos();
    cargarDataTableEquipamientosAdquiridos();
    cargarDataTableCaracteristicas();
    cargarDataTableCaracteristicasAdquiridas();
    cargarDataTableDivisionesAdquiridas();
    cargarDataTableCableadosAdquiridos();
    cargarDataComboDivisiones();
    cargarDataComboMateriales();
    cargarDataComboTiposCableado();

});


/*permite revisar el si el check fue clickeado para activar o desactivar el registro de construcción*/
$('#customSwitch3').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }

    var frm = new FormData();
    frm.append("estado", valor);
    frm.append("idConstruccion", $("#idConstruccion").val());

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/construccion/CambiarEstadoConstruccion",
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

function cargarDataComboDivisiones() {

    $.ajax({
        type: "GET",
        url: "/admin/construccion/ObtenerListaDivisiones",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                cargarCombos(data.data, "#tipoDivision",);
            else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function cargarDataComboTiposCableado() {

    $.ajax({
        type: "GET",
        url: "/admin/construccion/ObtenerListaTiposCableado",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null){
                cargarCombos(data.data, "#tipoCableado");
            } else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function cargarDataComboMateriales() {

    $.ajax({
        type: "GET",
        url: "/admin/construccion/ObtenerListaMateriales",
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                cargarCombos(data.data, "#tipoMaterial");
                cargarCombos(data.data, "#tipoMaterialDivision")
            } else {

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error al cargar los datos!!');
        }
    });
}

function RecargarCombosEstadoInicial(nombreMetodo, idCombo) {

    $.ajax({
        url: "/admin/construccion/" + nombreMetodo, // Url
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

/**
 * EQUIPAMIENTOS
 * 
 */
function AgregarEquipamiento(id) {

    var frm = new FormData();

    frm.append("idEquipamiento", id);
    frm.append("idConstruccion", $("#idConstruccion").val());

    $.ajax({
        url: "/admin/construccion/AgregarEquipamiento", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                if (result.data == "OK") {
                    dataTableEquipamientosAdquiridos.ajax.reload();
                    toastr.success("Equipamiento agregado correctamente");
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

function cargarDataTableEquipamientos() {

    dataTableEquipamientos = $("#tblEquipamientos").DataTable({
        "ajax": {
            "url": "/admin/construccion/ObtenerListaEquipamientos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idEquipamiento", "width": "5%" },
            { "data": "descripcion", "width": "100%" },
            {
                "data": "idEquipamiento",
                "render": function (data) {
                    return `<a onclick=AgregarEquipamiento(${data}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-plus-alt'></i> Agregar
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

function cargarDataTableEquipamientosAdquiridos() {

    dataTableEquipamientosAdquiridos = $("#tblEquipamientosAdquiridos").DataTable({
        "ajax": {
            "url": "/admin/construccion/ObtenerListaEquipamientosAdquiridos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idConstruccionEquipamiento", "width": "5%" },
            { "data": "descripcion", "width": "60%" },
            { "data": "cantidad", "width": "5%" },
            {
                "data": "idConstruccionEquipamiento",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=AumentarEquipamiento("/Admin/Construccion/AumentarEquipamiento/${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-plus'></i>
                            </a>
                            <a onclick=DisminuirEquipamiento("/Admin/Construccion/DisminuirEquipamiento/${data}") class='btn btn-warning text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-minus'></i> 
                            </a>
                            <a onclick=EliminarEquipamiento("/Admin/Construccion/EliminarEquipamiento/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function DisminuirEquipamiento(url) {

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success && data.message == "OK") {

                toastr.success("El registro ha sido disminuido correctamente");
                dataTableEquipamientosAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta disminuir la cantidad no existe");
                    dataTableEquipamientosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EliminarEquipamiento(url) {

    $.ajax({
        type: 'Post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableEquipamientosAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta eliminar no existe");
                    dataTableEquipamientosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function AumentarEquipamiento(url) {

    $.ajax({
        type: 'post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTableEquipamientosAdquiridos.ajax.reload();
            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta aumentar la cantidad no existe");
                    dataTableEquipamientosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

/**
 * DIVISIONES
 *
 */
 
$("#formDivisionEditar").submit(function (e) {

    e.preventDefault();

    var nombreDescriptivo = $("#nombreDescriptivoDivision").val()
    var observacionDivision = $("#observacionAdicionalDivision").val()

    if (nombreDescriptivo === "") {
        toastr.warning("Debe seleccionar el nombre descriptivo para la división");
        return;
    }

    if ($("#nombreDescriptivoDivision").val().length >= 50) {
        $("#ErrorNombreDescriptivoDivisionEdit").text("La observación no debe superar los 50 caracteres");
        return;
    } else $("#ErrorNombreDescriptivoDivisionEdit").text("");

    if ($("#observacionAdicionalDivision").val().length >= 200) {
        $("#ErrorObservacionDivisionEdit").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionDivisionEdit").text("");

    if (materiales.length === 0) {
        swal({
            title: "Atención",
            text: "Debe seleccionar al menos un material para la división",
            timer: 1500,
            showConfirmButton: false
        });
        return;
    }

    var frm = new FormData();
    frm.append("idDivision", $("#idDivision").val());
    frm.append("idConstruccion", $("#idConstruccion").val());
    frm.append("idConstruccionDivision", $("#idConstruccionDivision").val());
    frm.append("observacion", observacionDivision);
    frm.append("nombreDescriptivo", nombreDescriptivo);

    $.ajax({
        url: '/admin/construccion/EditarDivision',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarCamposEditarDivision();
                swal("¡Excelente!", "La división ha sido editada correctamente", "success");
                dataTableDivisionesAdquiridas.ajax.reload();              
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta editar no existe");
                    dataTableDivisionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-division").modal("hide");
            }
        }
    });

});

$("#tipoDivision").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoDivision").text("Debe seleccionar un tipo de división");
    else $("#ErrorTipoDivision").text("");
});

function cargarDataTableDivisionesAdquiridas() {

    dataTableDivisionesAdquiridas = $("#tblDivisionesAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/construccion/ObtenerListaDivisionesAdquiridas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idConstruccionDivision", "width": "5%" },
            { "data": "descripcion", "width": "10%" },
            { "data": "nombreDescriptivo", "width": "10%" },
            { "data": "observacion", "width": "50%" },
            {
                "data": "idConstruccionDivision",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=ObtenerDivision("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarDivisionAgregada("${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function LimpiarCamposDivision() {

    $("#listaMateriales").empty(); /*limpiar el tbody de la tabla*/
    materiales = new Array(); /*inicializar el arreglo*/

    /*limpiar los campos de texto*/
    $("#nombreDescriptivo").val("")
    $("#observacion").val("")
    $("#descripcionMaterial").val("")
    $("#ErrorObservacionDivision").text("");
    /*limpiar los combos*/
    $("#tipoMaterial").empty();
    $("#tipoDivision").empty();

    /*recargar los combos*/
    RecargarCombosEstadoInicial("ObtenerListaDivisiones", "#tipoDivision");
    RecargarCombosEstadoInicial("ObtenerListaMateriales", "#tipoMaterial");

}
 
$("#btnCancelarAgregarDivision").click(function () {
    LimpiarCamposDivision();
});

$("#cerrarModalAgregarDivision").click(function () {
    LimpiarCamposDivision();
});

function LimpiarCamposEditarDivision() {

    $("#listaMaterialesDivision").empty(); /*limpiar el tbody de la tabla*/
    materiales = new Array(); /*inicializar el arreglo*/
    
    /*limpiar los campos de texto*/
    $("#nombreDescriptivoDivision").val("")
    $("#observacionAdicionalDivision").val("")
    $("#descripcionMaterialDivision").val("")
    $("#ErrorObservacionDivision").text("");
    $("#ErrorTipoDivision").text("");
    $("#ErrorObservacionDivisionEdit").text("");
    $("#ErrorNombreDescriptivoDivisionEdit").text("");
    /*limpiar los combos*/
    $("#tipoMaterialDivision").empty();

    /*recargar los combos*/
    RecargarCombosEstadoInicial("ObtenerListaMateriales", "#tipoMaterialDivision");

    $("#modal-lg-editar-division").modal("hide");

}

$("#btnCerrarEditarDivision").click(function () {
    if (materiales.length === 0) {
        swal({
            title: "Atención",
            text: "Debe seleccionar al menos un material para la división",
            timer: 1500,
            showConfirmButton: false
        });
        return;
    } else LimpiarCamposEditarDivision();
});

$("#cerrarModalEditarDivision").click(function () {
    if (materiales.length === 0) {
        swal({
            title: "Atención",
            text: "Debe seleccionar al menos un material para la división",
            timer: 1500,
            showConfirmButton: false
        });
        return;
    } else LimpiarCamposEditarDivision();
});

function ObtenerDivision(id) {

    var frm = new FormData();
    frm.append("idConstruccion", $("#idConstruccion").val());
    frm.append("idConstruccionDivision", id);

    $.ajax({
        url: "/admin/construccion/ObtenerDatosDivisionEspecifica", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (data) {
            if (data.data != null) {

                cargarDatosDivision(data.data);
                
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableDivisionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-division").modal("hide");

            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.success(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
};

function EliminarDivisionAgregada(id) {

    var frm = new FormData();
    frm.append("idConstruccionDivision", id);

    $.ajax({
        url: '/admin/construccion/EliminarDivisionAgregada',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                //Si todo es correcto lo agrega
                dataTableDivisionesAdquiridas.ajax.reload();
                toastr.success("División eliminada correctamente");

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableDivisionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function cargarDatosDivision(data) {

    $("#nombreDivision").text(data.descripcion);
    $("#nombreDescriptivoDivision").val(data.nombreDescriptivo);
    $("#observacionAdicionalDivision").val(data.observacion);
    $("#idDivision").val(data.idDivision);
    $("#idConstruccionDivision").val(data.idConstruccionDivision);

    var listaMateriales = data.materiales;
    var listaTemporal = [];

    for (i = 0; i < listaMateriales.length; i++) {

        var IdMaterial = data.materiales[i].idMaterial;
        var IdDivision = data.materiales[i].idDivision;
        var IdConstruccionDivision = data.materiales[i].idConstruccionDivision;
        var IdDivisionMateriales = data.materiales[i].idDivisionMateriales;
        var Nombre = data.materiales[i].nombre; 
        var Descripcion = data.materiales[i].descripcion;

        const material = new MaterialEditar(IdMaterial, Nombre, Descripcion, IdDivisionMateriales, IdDivision, IdConstruccionDivision);
        listaTemporal.push(material);
    }

    recargarMaterialesAgregados(listaTemporal);

    $("#modal-lg-editar-division").modal("show");
}

function ValidarMaterialYaIngresado(id) {

    for (i = 0; i < materiales.length; i++) {

        if (materiales[i].getId() === id) {
            toastr.warning("El material ya ha sido ingresado.");
            return true;
        }

    }

}

function ValidarMaterialYaIngresadoEdit(id) {

    for (i = 0; i < materiales.length; i++) {

        var idObtenido = materiales[i].getId();

        if (Number.parseInt(idObtenido) === Number.parseInt(id)) {
            toastr.warning("El material ya ha sido ingresado.");
            return true;
        }

    }

}

function recargarMaterialesAgregados(lista) {

    $("#listaMaterialesDivision").empty();
    const listaMateriales = $("#listaMaterialesDivision");

    for (i = 0; i < lista.length; i++) {

        var id = lista[i].idMaterial;
        var nombre = lista[i].nombre;
        var descripcion = lista[i].descripcion;

        var contenido = "";

        const element = document.createElement("tr");

        contenido += "<th scope='row'>" + id + "</th>";
        contenido += "<td>" + nombre + "</td>";
        contenido += "<td>" + descripcion + "</td>";
        contenido += `<td class='text-center'> <a onclick=EliminarMaterial(${id}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                         <i class='fas fa-trash'></i> 
                      </a> </td>`

        element.innerHTML = contenido;
        listaMateriales.append(element);
        materiales.push(lista[i]);
    }

}

function agregarMaterial(material) {

    $("#listaMateriales").empty();

    materiales.push(material);

    const listaMateriales = $("#listaMateriales");

    for (i = 0; i < materiales.length; i++) {

        var id = materiales[i].getId();
        var nombre = materiales[i].getNombre();
        var descripcion = materiales[i].getDescripcion();

        var contenido = "";

        const element = document.createElement("tr");

        contenido += "<th scope='row'>" + id + "</th>";
        contenido += "<td>" + nombre + "</td>";
        contenido += "<td>" + descripcion + "</td>";
        contenido += `<td class='text-center'> <a onclick=EliminarMaterialDeTabla(${id}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-trash'></i> 
                      </a> </td>`

        element.innerHTML = contenido;
        listaMateriales.append(element);
    }

}

function EliminarMaterial(id) {

    var material = new Material();

    for (i = 0; i < materiales.length; i++) {

        var idObtenido = Number.parseInt(materiales[i].getId());

        if (idObtenido == id) {
            var descripcion = materiales[i].getDescripcion();
            material.descripcion = descripcion;
        }
    }

    var frm = new FormData();
    frm.append("idDivision", $("#idDivision").val());
    frm.append("idMaterial", id);
    frm.append("idConstruccionDivisiones", $("#idConstruccionDivision").val());
    frm.append("descripcion", material.getDescripcion());

    $.ajax({
        url: '/admin/construccion/EliminarMaterialDivision',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {

                //Si todo es correcto lo elimina
                dataTableDivisionesAdquiridas.ajax.reload();
                materiales = [];
                ObtenerDivision($("#idConstruccionDivision").val());
                toastr.success("Material eliminado correctamente");

            } else {

                if (data.message == "No existe division") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableDivisionesAdquiridas.ajax.reload();
                    $("#modal-lg-editar-division").modal("hide");
                } else if (data.message == "No existe material") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableDivisionesAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });

}

function agregarMaterialListaRecargada(material) {

    $("#listaMaterialesDivision").empty();

    materiales.push(material);

    const listaMateriales = $("#listaMaterialesDivision");

    for (i = 0; i < materiales.length; i++) {

        var id = materiales[i].getId();
        var nombre = materiales[i].getNombre();
        var descripcion = materiales[i].getDescripcion();

        var contenido = "";

        const element = document.createElement("tr");

        contenido += "<th scope='row'>" + id + "</th>";
        contenido += "<td>" + nombre + "</td>";
        contenido += "<td>" + descripcion + "</td>";
        contenido += `<td class='text-center'> <a onclick=EliminarMaterialEditar(${id}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-trash'></i> 
                      </a> </td>`

        element.innerHTML = contenido;
        listaMateriales.append(element);
    }

}

function EliminarMaterialDeTabla(id) {

    $("#listaMateriales").empty(); 

    var arregloTemporal = new Array();

    /*Excluyo del arreglo el objeto con el id indicado*/
    for (var h = 0; h < materiales.length; h++) {

        var idObtenido = materiales[h].getId();
        var descripcionObtenida = materiales[h].getDescripcion();

        if (idObtenido != id && descripcionObtenida != descripcion) {
            arregloTemporal.push(materiales[h]);
        }
    }

    materiales = arregloTemporal;

    const listaMateriales = $("#listaMateriales");

    /*Se recorre nuevamente el arreglo con los objetos agregados sin tomar en cuenta el eliminado claramente*/
    for (var i = 0; i < arregloTemporal.length; i++) {

        var id = arregloTemporal[i].getId();
        var nombre = arregloTemporal[i].getNombre();
        var descripcion = arregloTemporal[i].getDescripcion();

        var contenido = "";

        const element = document.createElement("tr");

        contenido += "<th scope='row'>" + id + "</th>";
        contenido += "<td>" + nombre + "</td>";
        contenido += "<td>" + descripcion + "</td>";
        contenido += `<td class='text-center'> <a onclick=EliminarMaterial(${id}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-trash'></i> 
                      </a> </td>`

        element.innerHTML = contenido;
        listaMateriales.append(element);
    }

}

$("#formMaterialesEditar").submit(function (e) {

    e.preventDefault();

    var observacion = $("#descripcionMaterialDivision").val()
    var idMaterial = $("#tipoMaterialDivision").val()

    /*valida que no sea 0 el id del elemento del combo*/
    if (idMaterial == 0) {
        toastr.warning("Debe seleccionar un tipo de material");
        return;
    }

    if (observacion.length >= 200) {
        toastr.warning("La descripción del material no debe superar los 200 caracteres");
        return;
    }

    if (ValidarMaterialYaIngresadoEdit(idMaterial)) return;

    var frm = new FormData();
    frm.append("idDivision", $("#idDivision").val());
    frm.append("idMaterial", idMaterial);
    frm.append("idConstruccionDivisiones", $("#idConstruccionDivision").val());
    frm.append("descripcion", observacion);

    $.ajax({
        url: '/admin/construccion/AgregarMaterialDivision',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                //Si todo es correcto lo agrega
                $("#descripcionMaterialDivision").val("");
                dataTableDivisionesAdquiridas.ajax.reload();
                materiales = [];
                ObtenerDivision($("#idConstruccionDivision").val());
                toastr.success("Material agregado correctamente");

                $("#tipoMaterialDivision").empty();
                RecargarCombosEstadoInicial("ObtenerListaMateriales", "#tipoMaterialDivision");

            } else {
                toastr.error("Ha ocurrido un error al intentar agregar la división");
            }
        }
    });

});

$("#formDivision").submit(function (e) {

    e.preventDefault();

    // Obtener los valores de cada campo
    var nombreDescriptivo = $("#nombreDescriptivo").val()
    var observacion = $("#observacion").val()
    var idDivision = $("#tipoDivision").val()
    var idConstruccion = $("#idConstruccion").val();

    /*valida que no sea 0 el id del elemento del combo*/
    if (idDivision == "0") {
        toastr.warning("Debe seleccionar el tipo de división");
        return;
    }

    if (nombreDescriptivo === "") {
        toastr.warning("Debe seleccionar el nombre descriptivo para la división");
        return;
    }

    if ($("#nombreDescriptivo").val().length >= 50) {
        $("#ErrorNombreDescriptivoDivision").text("La observación no debe superar los 50 caracteres");
        return;
    } else $("#ErrorNombreDescriptivoDivision").text("");

    if ($("#observacion").val().length >= 200) {
        $("#ErrorObservacionDivision").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionDivision").text("");

    if (materiales.length === 0) {
        swal({
            title: "Atención",
            text: "Debe seleccionar al menos un material para la división",
            timer: 1500,
            showConfirmButton: false
        });
        return;
    }

    // esta deberia ser la forma en la cual declaras tu objeto datos para que la pueda parsear a Json
    var list = {
        'materiales': [],
        'idDivision': idDivision,
        'nombreDescriptivo': nombreDescriptivo,
        'descripcion': observacion,
        'idConstruccion': idConstruccion
    };

    //guardas los datos
    for (var i = 0; i < materiales.length; i++) {

        list.materiales.push({
            "idMaterial": materiales[i].getId(),
            "descripcion": materiales[i].getDescripcion()
        });
    };

    var json = JSON.stringify(list); // aqui tienes la lista de objetos en Json
    var obj = JSON.parse(json); //Parsea el Json al objeto anterior.

    $.ajax({
        url: '/admin/construccion/AgregarDivisiones',
        type: 'POST',
        data: obj,
        success: function (data) {
            if (data.success) {
                LimpiarCamposDivision();
                swal("¡Excelente!", "La división ha sido agregada correctamente", "success");
                dataTableDivisionesAdquiridas.ajax.reload();
                $("#modal-lg-agregar-divisiones").modal("hide");
            } else {
                toastr.error("Ha ocurrido un error al intentar agregar la división " + data.message);
            }
        }
    });

});

$("#formMateriales").submit(function (e) {

    e.preventDefault();

    // Obtener los valores de cada campo
    var nombreDescriptivo = $('select[name="tipoMaterial"] option:selected').text();
    var observacion = $("#descripcionMaterial").val()
    var idMaterial = $("#tipoMaterial").val()

    /*valida que no sea 0 el id del elemento del combo*/
    if (idMaterial == 0) {
        toastr.warning("Debe seleccionar un tipo de material");
        return;
    }

    if (observacion.length >= 200) {
        toastr.warning("La descripción del material no debe superar los 200 caracteres");
        return;
    }

    if (ValidarMaterialYaIngresado(idMaterial)) return;

    // Crea un nuevo objeto material
    const nuevoMaterial = new Material(idMaterial, nombreDescriptivo, observacion);

    //Si todo es correcto lo agrega
    agregarMaterial(nuevoMaterial);
    toastr.success("Material agregado correctamente");
    $("#descripcionMaterial").val("");

    $("#tipoMaterial").empty();
    RecargarCombosEstadoInicial("ObtenerListaMateriales", "#tipoMaterial");

});

$("#tipoMaterial").change(function () {

    var valor = $(this).val();

    if (valor == 0) $("#ErrorTipoMaterial").text("Debe seleccionar un tipo de material");
    else $("#ErrorTipoMaterial").text("");
});


/*CABLEADO*/

function EliminarTipoCableadoAdquirido(id) {

    var frm = new FormData();
    frm.append("idConstruccionCableado", id);

    $.ajax({
        url: '/admin/construccion/EliminarTipoCableadoAdquirido',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                //Si todo es correcto lo agrega
                dataTableCableadosAdquiridos.ajax.reload();
                toastr.success("Tipo de cableado eliminado correctamente");
            }else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableCableadosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
     
}

function cargarDataTableCableadosAdquiridos() {

    dataTableCableadosAdquiridos = $("#tblTiposCableadoAdquiridos").DataTable({
        "ajax": {
            "url": "/admin/construccion/ObtenerListadoTiposCableadoObtenidos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idConstruccionCableado", "width": "5%" },
            { "data": "descripcion", "width": "25%" },
            { "data": "observacion", "width": "40%" },
            { "data": "entubado", "width": "5%" },
            {
                "data": "idConstruccionCableado",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=EditarTipoCableadoAdquirido("${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-edit'></i>
                            </a>
                            <a onclick=EliminarTipoCableadoAdquirido("${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-trash-alt'></i>
                            </a>
                            </div>`;
                }, "width": "50%"
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
  
function EditarTipoCableadoAdquirido(id) {

    var frm = new FormData();
    frm.append("idConstruccion", $("#idConstruccion").val());
    frm.append("idConstruccionCableado", id);

    $.ajax({
        url: "/admin/construccion/ObtenerDatosCableadoAgregado", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (data) {
            if (data.data != null) {

                cargarDatosCableadoObtenido(data.data);

            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta acceder no existe");
                    dataTableCableadosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-servMunicipal").modal("hide");
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.success(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
};

function cargarDatosCableadoObtenido(data) {

    if (data.esEntubado == "Si") {
        $("#checkEntubadoEdit").prop("checked", true);
    }

    $("#nombreTipoCableado").text(data.nombre);
    $("#observacionCableadoEdit").val(data.observacion);
    $("#idConstruccionCableado").val(data.idConstruccionCableado);

    $("#modal-lg-editar-tipoCableado").modal("show");

}

function agregarTipoCableado(tipoCableado) {

    $("#listaTiposCableado").empty();

    cableados.push(tipoCableado);

    const listaTiposCableado = $("#listaTiposCableado");

    for (i = 0; i < cableados.length; i++) {

        var id = cableados[i].getId();
        var nombreTipoCable = cableados[i].getNombreDescriptivo();
        var esEntubado = cableados[i].getEsEntubado();
        var observacion = cableados[i].getObservacion();
        var guid = cableados[i].getGUID();

        var contenido = "";

        const element = document.createElement("tr");

        contenido += "<th scope='row'>" + id + "</th>";
        contenido += "<td>" + nombreTipoCable + "</td>";
        contenido += "<td>" + observacion + "</td>";
        contenido += "<td>" + esEntubado + "</td>";
        contenido += `<td class='text-center'> <a onclick=EliminarTipoCableadoTabla(${guid}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-trash'></i> 
                      </a> </td>`

        element.innerHTML = contenido;
        listaTiposCableado.append(element);
    }

}

function EliminarTipoCableadoTabla(guid) {

    $("#listaTiposCableado").empty();

    var arregloTemporal = new Array();

    /*Excluyo del arreglo el objeto con la observacion indicada*/
    for (var h = 0; h < cableados.length; h++) {

        var guidObtenido = cableados[h].getGUID();

        if (guidObtenido != guid) {
            arregloTemporal.push(cableados[h]);
        }
    }

    cableados = arregloTemporal;

    const listaCableados = $("#listaTiposCableado");

    /*Se recorre nuevamente el arreglo con los objetos agregados sin tomar en cuenta el eliminado claramente*/
    for (var i = 0; i < arregloTemporal.length; i++) {

        var id = arregloTemporal[i].getId();
        var nombre = arregloTemporal[i].getNombreDescriptivo();
        var observacion = arregloTemporal[i].getObservacion();
        var esEntubado = arregloTemporal[i].getEsEntubado();
        var guid = cableados[i].getGUID();

        var contenido = "";

        const element = document.createElement("tr");

        contenido += "<th scope='row'>" + id + "</th>";
        contenido += "<td>" + nombre + "</td>";
        contenido += "<td>" + observacion + "</td>";
        contenido += "<td>" + esEntubado + "</td>";
        contenido += `<td class='text-center'> <a onclick=EliminarTipoCableadoTabla(${guid}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-trash'></i> 
                      </a> </td>`

        element.innerHTML = contenido;
        listaCableados.append(element);
    }

}

$("#formTipoCableado").submit(function (e) {

    e.preventDefault();

    // Obtener los valores de cada campo
    var nombreDescriptivo = $('select[name="tipoCableado"] option:selected').text();
    var observacion = $("#observacionCableado").val()
    var idCableado = $("#tipoCableado").val()
    var valorCheck = $('input:checkbox[name=checkEntubado]:checked').val();

    if ($("#observacionCableado").val().length >= 200) {
        $("#ErrorObservacionEntubado").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionEntubado").text("");

    var activa = $("#checkEntubado").prop('checked');

    if (!activa) valorCheck = "No";

    /*valida que no sea 0 el id del elemento del combo*/
    if (idCableado == 0) {
        toastr.warning("Debe seleccionar un tipo de cableado");
        return;
    }

    // Crea un nuevo objeto tipo de cableado
    var d = new Date();
    var h = addZero(d.getHours(), 2);
    var m = addZero(d.getMinutes(), 2);
    var s = addZero(d.getSeconds(), 2);
    var ms = addZero(d.getMilliseconds(), 3);
    var guid = h + m + s + ms + h + m + s + ms;

    const nuevoTipoCableado = new Cableado(idCableado, nombreDescriptivo, valorCheck, observacion, guid);

    //Si todo es correcto lo agrega
    agregarTipoCableado(nuevoTipoCableado);
    toastr.success("Tipo de cableado agregado correctamente");
    $("#observacionCableado").val("");
    $("#tipoCableado").empty();
    RecargarCombosEstadoInicial("ObtenerListaTiposCableado", "#tipoCableado");
    $("#checkEntubado").prop("checked", false);
});

function addZero(x,n) {

    while (x.toString().length < n) {
        x = "0" + x;
    }

    return x;
};

$("#btnGuardarTiposCableadoEdit").click(function (e) {

    e.preventDefault();

    var frm = new FormData();
    frm.append("idConstruccion", $("#idConstruccion").val());
    frm.append("idConstruccionCableado", $("#idConstruccionCableado").val());
    frm.append("observacion", $("#observacionCableadoEdit").val());

    if ($("#observacionCableadoEdit").val().length >= 200) {
        $("#ErrorObservacionEntubadoEdit").text("La observación no debe superar los 200 caracteres");
        return;
    } else $("#ErrorObservacionEntubadoEdit").text("");

    var activa = $("#checkEntubadoEdit").prop('checked');

    if (!activa) {
        frm.append("esEntubado", "N");
    } else {
        frm.append("esEntubado", "S");
    }

    $.ajax({
        url: '/admin/construccion/EditarTipoCableado',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data.success) {
                LimpiarDatosEditarCableado();
                swal("¡Excelente!", "El tipo de cableado ha sido editado correctamente", "success");
                dataTableCableadosAdquiridos.ajax.reload();
                $("#modal-lg-editar-tipoCableado").modal("hide");
            } else {

                if (data.message == "No existe") {
                    toastr.error("El recurso que intenta editar no existe");
                    dataTableCableadosAdquiridos.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }

                $("#modal-lg-editar-tipoCableado").modal("hide");
            }
        }
    });

});

$("#btnGuardarTiposCableado").click(function () {

    var idConstruccion = $("#idConstruccion").val();

    if (cableados.length === 0) {
        swal({
            title: "Atención",
            text: "Debe seleccionar al menos un tipo de cableado",
            timer: 1500,
            showConfirmButton: false
        });
        return;
    }

    // esta deberia ser la forma en la cual declaras tu objeto datos para que la pueda parsear a Json
    var list = {
        'cableados': [],
        'idConstruccion': idConstruccion
    };

    //guardas los datos
    for (var i = 0; i < cableados.length; i++) {

        list.cableados.push({
            "idTipoCableado": cableados[i].getId(),
            "observacion": cableados[i].getObservacion(),
            "esEntubado": cableados[i].getEsEntubado()
        });
    };

    var json = JSON.stringify(list); // aqui tienes la lista de objetos en Json
    var obj = JSON.parse(json); //Parsea el Json al objeto anterior.

    $.ajax({
        url: '/admin/construccion/AgregarTiposDeCableadoConstruccion',
        type: 'POST',
        data: obj,
        success: function (data) {
            if (data.success) {
                LimpiarDatosAgregarCableado();
                swal("¡Excelente!", "El tipo de cableado ha sido agregada correctamente", "success");
                dataTableCableadosAdquiridos.ajax.reload();
            } else {
                toastr.error("Ha ocurrido un error al intentar agregar el tipo de cableado " + data.message);
            }
        }
    });
});

$("#cerrarModalAgregarCableado").click(function () {
    LimpiarDatosAgregarCableado();
});

$("#btnCancelarAgregarCableado").click(function () {
    LimpiarDatosAgregarCableado();
});

function LimpiarDatosAgregarCableado() {
    cableados = new Array();
    $("#tipoCableado").empty();
    RecargarCombosEstadoInicial("ObtenerListaTiposCableado", "#tipoCableado");
    $("#checkEntubado").prop("checked", false);
    $("#observacionCableado").val("");
    $("#ErrorObservacionEntubado").text("");
    $("#listaTiposCableado").empty();
    $("#modal-lg-agregar-tipos-cableado").modal("hide");
}

$("#cerrarModalEditarTipoCableado").click(function () {
    LimpiarDatosEditarCableado();
});

$("#btnCancelarEditarTipoCableado").click(function () {
    LimpiarDatosEditarCableado();
});

function LimpiarDatosEditarCableado() {
    cableados = new Array();
    $("#ErrorObservacionEntubadoEdit").text("");
    $("#checkEntubadoEdit").prop("checked", false);
    $("#observacionCableadoEdit").val("");
    $("#modal-lg-agregar-tipos-cableado").modal("hide");
}

/**´-*****************************************************  CARACTERISTICAS **********************************************************/

function cargarDataTableCaracteristicasAdquiridas() {

    dataTableCaracteristicasAdquiridas = $("#tblCaracteristicasAdquiridas").DataTable({
        "ajax": {
            "url": "/admin/construccion/ObtenerListaCaracteristicasConstruccionAdquiridas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idConstruccionCaracteristica", "width": "5%" },
            { "data": "descripcion", "width": "60%" },
            { "data": "cantidad", "width": "5%" },
            {
                "data": "idConstruccionCaracteristica",
                "render": function (data) {
                    return `<div class="text-center">
                            <a onclick=AumentarCaracteristicaConstruccion("/Admin/Construccion/AumentarCaracteristicaConstruccion/${data}") class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-plus'></i>
                            </a>
                            <a onclick=DisminuirCaracteristicaConstruccion("/Admin/Construccion/DisminuirCaracteristicaConstruccion/${data}") class='btn btn-warning text-white' style='cursor:pointer; width:50px;'>
                            <i class='fas fa-minus'></i> 
                            </a>
                            <a onclick=EliminarCaracteristicaConstruccion("/Admin/Construccion/EliminarCaracteristicaConstruccion/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
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

function AumentarCaracteristicaConstruccion(url) {

    $.ajax({
        type: 'post',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
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

function DisminuirCaracteristicaConstruccion(url) {

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success && data.message == "OK") {

                toastr.success("El registro ha sido disminuido correctamente");
                dataTableCaracteristicasAdquiridas.ajax.reload();

            }
            else {

                if (data.message == "No existe") {
                    toastr.error("El recurso al que intenta disminuir la cantidad no existe");
                    dataTableCaracteristicasAdquiridas.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        }
    });
}

function EliminarCaracteristicaConstruccion(url) {

    $.ajax({
        type: 'Post',
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

function AgregarCaracteristicasConstruccion(id) {

    var frm = new FormData();

    frm.append("idCaracteristica", id);
    frm.append("idConstruccion", $("#idConstruccion").val());

    $.ajax({
        url: "/admin/construccion/AgregarCaracteristicaConstruccion", // Url
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
            "url": "/admin/construccion/ObtenerListaCaracteristicasConstruccion",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCaracteristica", "width": "5%" },
            { "data": "descripcion", "width": "100%" },
            {
                "data": "idCaracteristica",
                "render": function (data) {
                    return `<a onclick=AgregarCaracteristicasConstruccion(${data}) class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                                <i class='fas fa-plus-alt'></i> Agregar
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