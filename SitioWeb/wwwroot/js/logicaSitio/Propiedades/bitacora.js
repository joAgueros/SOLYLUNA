var dataTable;

$(document).ready(function () {

    cargarDataTableBitacoraPropiedades();

});

/*Para poder activar un calendario en los campos para fechas*/
$("#fechaInicio").datepicker();
$("#fechaFin").datepicker();


$("#btnEnviarFechas").click(function (e) {
    
    e.preventDefault();

    if ($("#fechaInicio").val() == "") {
        $("#ErrorFechaInicio").text("Debe seleccionar la fecha inicial");
        return;
    } else $("#ErrorFechaInicio").text("");

    if ($("#fechaFin").val() == "") {
        $("#ErrorFechaFinal").text("Debe seleccionar la fecha final");
        return;
    } else $("#ErrorFechaFinal").text("");

    var frm = new FormData();
    frm.append("fechaInicio", $("#fechaInicio").val());
    frm.append("fechaFin", $("#fechaFin").val());

    $.ajax({
        url: '/admin/propiedad/ObtenerListaBitacoraPropiedades',
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        success: function (data) {
            if (data != null) {

                llenar(data.data);
            
            } else if (data.data == "ERROR") {
                toastr.error("Ha ocurrido un error al obtener los registros");
            }
        }
    });
});

function cargarDataTableBitacoraPropiedades() {

    dataTable = $("#tblBitacoraPropiedades").DataTable({
        "ajax": {
            "url": "/admin/propiedad/CargarListaBitacoraPropiedades",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idBitacora", "width": "4%" },
            { "data": "usuario", "width": "10%" },
            { "data": "descripcion", "width": "57%" },
            { "data": "tipoOperacion", "width": "5%" },
            { "data": "fechaConcantenada", "width": "10%" },
            { "data": "registroAfectado", "width": "5%" },
            { "data": "idRegistroAfectado", "width": "3%" }       
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

function llenar(response) {
    $('#tblBitacoraPropiedades').DataTable({
        "destroy": true,
        "data": response,
        "columns": [
            { "data": "idBitacora", "width": "4%" },
            { "data": "usuario", "width": "10%" },
            { "data": "descripcion", "width": "50%" },
            { "data": "tipoOperacion", "width": "5%" },
            { "data": "fechaConcantenada", "width": "15%" },
            { "data": "registroAfectado", "width": "5%" },
            { "data": "idRegistroAfectado", "width": "3%" } 
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