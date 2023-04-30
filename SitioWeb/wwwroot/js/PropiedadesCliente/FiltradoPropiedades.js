
var dataTable;

$(document).ready(function () {

    cargarDataTable();
});

function cargarDataTable() {

    dataTable = $("#tblPropiedadesHome").DataTable({
        "ajax": {
            "url": "/cliente/Home/ObtenerListaPropiedadesHome",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "codigoTipoUsoPropiedad", "width": "10%" },
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
                            <a href='/Admin/Propiedad/VerInformacionPropiedad/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-edit'></i> Ver </a>
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