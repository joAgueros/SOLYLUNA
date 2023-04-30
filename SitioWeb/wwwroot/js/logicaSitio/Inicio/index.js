var dataTable;

$(document).ready(function (e) {
    
    //cargarDataTable();

    //$("#formFiltrado").on('submit', function (e) {
    //    e.preventDefault();

    //    var provincia = $("#provincias").val();
    //    var tipo = $("#tipoPropiedades").val();
    //    var precioMax = $('select[name="valoresMaximos"] option:selected').text();
    //    var precioMin = $('select[name="valoresMinimos"] option:selected').text();
    //    var moneda = $('select[name="tipoMonedas"] option:selected').text();
    //    var tipoVista = $("input[name='radioVista']:checked").val();

    //    var fdata = new FormData();

    //    fdata.append("provincia", provincia);
    //    fdata.append("tipo", tipo);
    //    fdata.append("precioMaximo", precioMax);
    //    fdata.append("precioMinimo", precioMin);
    //    fdata.append("moneda", moneda);
    //    fdata.append("vista", tipoVista);

    //    $.ajax({ 
    //        type: 'POST',
    //        url: "/cliente/propiedades/ObtenerPropiedadPorFiltrado",
    //        data: fdata,
    //        contentType: false,
    //        cache: false,
    //        processData: false,
    //        beforeSend: function () {
    //            $('#submitBtn').attr("disabled", "disabled");
    //        },
    //        success: function (msg) {

    //            if (msg != null) {
    //                //Actualiza el resultado HTML
    //                $("#ListadoPropiedades").html(msg);
    //                $('#submitBtn').removeAttr("disabled");
    //            } else {

    //            }

    //        }, error(xhr, status, error) {
    //            alert(xhr, status, error);
    //            }
    //            // Hacer algo siempre, haya sido exitosa o no.
    //           ,always() {
    //               alert(xhr);
    //            }
    //    });
    //});

});

function cargarDataTable() {

    dataTable = $("#tblPropiedadesHome").DataTable({
        "ajax": {
            "url": "/cliente/propiedades/ObtenerListaPropiedadesHome",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
                
            { "data": "codigoTipoUsoPropiedad", "width": "5%" },
            { "data": "tipoPropiedad", "width": "10%" },
            { "data": "usoPropiedad", "width": "5%" },
            { "data": "ubicacion", "width": "30%" },
            { "data": "medidaPropiedad", "width": "10%" },
            { "data": "moneda", "width": "3%" },
            { "data": "precioMaximo", "width": "10%" },
            { "data": "intencion", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Cliente/Home/VerInformacionPropiedad/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                             Ver Detalles </a>
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
            "search": "Filtrar:",
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

    console.log(listado);
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

$("#tipoMonedas").change(function () {

    var _tipoMoneda = $('select[name="tipoMonedas"] option:selected').text();

    $.ajax({
        url: "/cliente/propiedades/ObtenerValores", // Url
        data: { tipoMoneda: _tipoMoneda },
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (valores) {

            $("#idValoresMinimos").empty();
            $("#idValoresMaximos").empty();

            cargarCombos(valores.data, "#idValoresMinimos");
            cargarCombos(valores.data, "#idValoresMaximos");
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            toastr.error(error);
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

});

$("#busquedaVenta").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "post",
            url: "/cliente/home/GetDataBusquedaVenta",
            data: { busqueda: document.getElementById('busquedaVenta').value, intencion: document.getElementById('intencionVenta').value },
            success: function (data) {
                response(data.data);
            }
        });
    }
});

$("#busquedaRenta").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "post",
            url: "/cliente/home/GetDataBusquedaRenta",
            data: { busqueda: document.getElementById('busquedaRenta').value, intencion: document.getElementById('intencionRenta').value },
            success: function (data) {
                response(data.data);
            }
        });
    }
});



 