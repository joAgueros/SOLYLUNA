var dataTable;

$(document).ready(function () {

    cargarDataTable();
});

/*Mostrar los datos de la propiedad en la tabla */
function cargarDataTable() {

    dataTable = $("#tblPropiedades").DataTable({
        "ajax": {
            "url": "/admin/propiedad/ObtenerListaPropiedades",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Admin/Propiedad/VerInformacionPropiedad/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-edit'></i> Ver </a>
                            <a onclick=EliminarPropiedad("/Admin/Propiedad/EliminarPropiedad/${data}") class='btn btn-danger text-white m-1' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-trash'></i> </a>
                            </div>
                            `;
                }, "width": "20%" },
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
                "data": "id", "width": "5%"
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

function EliminarPropiedad(url) {

    new swal({
        title: '¿Está seguro(a) de eliminar la propiedad?',
        text: "",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar!',
        cancelButtonText: 'No, cancelar!',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger ml-2',
        buttonsStyling: false
    }).then(function (value) {

        if (value.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: url,
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        Swal.fire('Propiedad eliminada correctamente!', '', 'success')
                    }
                    else {

                        if (data.message == "No existe") {
                            toastr.error("El recurso al que intenta acceder no existe");
                            dataTable.ajax.reload();
                        }
                        else {
                            toastr.error(data.message);
                        }
                    }
                }
            });
        }

        
    })
      
}

