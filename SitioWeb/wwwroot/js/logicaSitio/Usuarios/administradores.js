var dataTable;

$(document).ready(function () {

    cargarDataTable();
    $('#muestraError').hide();
    $('#muestraAdvertencia').hide();
});

function LimpiarDatos() {

    /*vaciar los campos de texto*/
    $("#nombre").val("")
    $("#apellido1").val("")
    $("#apellido2").val("")
    $("#identificacion").val("")
    $("#correo").val("")
    $("#contrasenia").val("")
    $("#confirmarContrasenia").val("")
    $(".text-danger").text("")

    /*cierra el modal*/
    $('#modal-lg-registrar-administrador').modal('hide');
}

$("#btnCancelarRegistroAdministrador").click(function () {
    LimpiarDatos();
});

$("#btnCerrarModal").click(function () {
    LimpiarDatos();
});

$(function () {
    $("#RegistrarAdministradorForm").submit(function (e) {

        e.preventDefault();

        /*Recoge la informacion de cada campo*/
        var nombre = $("#nombre").val()
        var apellido1 = $("#apellido1").val()
        var apellido2 = $("#apellido2").val()
        var identificacion = $("#identificacion").val()
        var correo = $("#correo").val()
        var contrasenia = $("#contrasenia").val()
        var confirmarContrasenia = $("#confirmarContrasenia").val()

        var frm = new FormData(); /*Nuevos datos a enviar en un FormData*/

        frm.append("nombre", nombre);
        frm.append("apellido1", apellido1);
        frm.append("apellido2", apellido2);
        frm.append("identificacion", identificacion);
        frm.append("correoElectronico", correo);
        frm.append("nuevaContrasenia", contrasenia);
        frm.append("confirmacionContrasenia", confirmarContrasenia);

        $.ajax({
            url: "/admin/account/RegistrarAdministrador", // Url
            data: frm,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $('#SubmitBtn').attr("disabled", "disabled");
                $('#btnCancelarRegistroAdministrador').attr("disabled", "disabled");
            },
            type: "post",  // Verbo HTTP
        })
            
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result.success) {
                    LimpiarDatos();
                    dataTable.ajax.reload();
                    $("#SubmitBtn").removeAttr("disabled");
                    $("#btnCancelarRegistroAdministrador").removeAttr("disabled");
                    toastr.success("El registro ha sido almacenado de manera satisfactoria");
                } else {

                    if (result.message == "Ya existe") {
                        $("#SubmitBtn").removeAttr("disabled");
                        $("#btnCancelarRegistroAdministrador").removeAttr("disabled");
                        $('#muestraAdvertencia').show();
                        $("#mensajeAdvertencia").text("Ya existe un usuario en el sistema con el correo ingresado");
                        setTimeout(function () {
                            $("#muestraAdvertencia").fadeOut(1500);
                        }, 3000);
                    }
                    else if (result.message == "Error") {
                        $("#SubmitBtn").removeAttr("disabled");
                        $("#btnCancelarRegistroAdministrador").removeAttr("disabled");
                        $('#muestraError').show();
                        $("#mensajeError").text("Ha ocurrido un error al intentar guardar el registro");
                        setTimeout(function () {
                            $("#muestraError").fadeOut(1500);
                        }, 3000);
                    }

                }

            }) 
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {
                $("#SubmitBtn").removeAttr("disabled");
                $("#btnCancelarRegistroAdministrador").removeAttr("disabled");

            });
    });
});

/*Mostrar los datos de la propiedad en la tabla */
function cargarDataTable() {

    dataTable = $("#tblAdministradores").DataTable({
        "ajax": {
            "url": "/admin/account/ObtenerAdministradores",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "correo", "width": "25%" },
            { "data": "nombreCompleto", "width": "25%" },
            { "data": "identificacion", "width": "15%" },
            { "data": "confirmacion", "width": "5%" },
            { "data": "bloqueado", "width": "5%" },
            {
                "data": "correo",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href='/Admin/Account/EditarAdministrador/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
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
