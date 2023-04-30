var extensionesValidas = "png, jpeg, jpg";
var imagenElegida;

$(document).ready(function (e) {

    if ($(".preloader").length) {
        $(".preloader").fadeOut();
    }

    $('#submitBtn').hide();

    $('#divImagen').hide();

    $("#fupForm").on('submit', function (e) {
        e.preventDefault();

        var fileInput = imagenElegida;

        if (fileInput == null) {
            alert('Debe seleccionar una foto');
            return;
        }

        if (!window.FileReader) {
            alert('El navegador no soporta la lectura de archivos');
            return;
        }

        var fdata = new FormData();

        fdata.append("UrlImagen", fileInput);
        fdata.append("idConstruccion", $('#idConstruccion').val());

        $.ajax({
            type: 'POST',
            url: "/admin/construccion/CargarImagen",
            data: fdata,
            contentType: false,
            cache: false,
            processData: false,
            beforeSend: function () {
                $('#submitBtn').attr("disabled", "disabled");
                $('#fupForm').css("opacity", ".5");
            },
            success: function (msg) {

                if (msg != null) {
                    $('#fupForm').css("opacity", "");
                    $("#submitBtn").removeAttr("disabled");
                    $('#submitBtn').hide();
                    $('#previa').attr('src', '');
                    $('#divImagen').hide();
                    $('#texto').text('');
                    imagenElegida = null;
                    $("#foto").val("");
                    toastr.success("La foto ha sido almacenada de manera satisfactoria");
                    //Actualiza el resultado HTML
                    $("#ListadoImagenes").html(msg);

                } else {

                }

            }
        });
    });

});

function ValidarTamano(obj) {

    var uploadFile = obj.files[0];

    if (!validarExtension(uploadFile)) {
        $('#texto').text('El archivo tiene un formato o extensión no válida.');
        $('#texto').removeClass('text-success');
        $('#texto').addClass('text-danger');
        return;
    }

    var reader = new FileReader();
    var file = obj.files[0];
    reader.readAsDataURL(file);

    // Una vez ya ha sido leído:
    reader.addEventListener("load", function () {

        var image = new Image();
        image.src = reader.result;
        image.addEventListener("load", function () {

            var altura = image.height;
            var ancho = image.width;

            if (ancho.toFixed(0) <= 370 && altura.toFixed(0) <= 280) {
                $('#texto').text('La foto no cumple con los dimensiones especificadas');
                $('#texto').removeClass('text-success');
                $('#texto').addClass('text-danger');
                $('#previa').attr('src', ''); // Renderizamos la imagen
                $('#divImagen').hide();
                $('#submitBtn').hide();
            }
            else {
                readImage(obj);
            }

        });
    });
}

/**
 *validar la extension de la imagen
 */
function validarExtension(datos) {

    var tipoImagen = datos.type;
    var array = tipoImagen.split('/');
    var extensionValida = extensionesValidas.indexOf(array[1]);

    if (extensionValida < 0) {
        return false;
    } else {
        return true;
    }
}

// Vista preliminar de la imagen.
function readImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {

            $('#previa').attr('src', e.target.result); // Renderizamos la imagen
            $('#divImagen').show();
            $('#submitBtn').show();
        }

        reader.readAsDataURL(input.files[0]);

        imagenElegida = input.files[0];
        $('#texto').text('La foto cumple con los requisitos solicitados');
        $('#texto').removeClass('text-danger');
        $('#texto').addClass('text-success');
    }

}

function EliminarImagen(id) {

    var frm = new FormData();
    frm.append("idConstruccion", $("#idConstruccion").val());
    frm.append("idImagen", id);

    $.ajax({
        url: "/admin/construccion/EliminarImagen", // Url
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (data) {

            var contiene = data.includes("Eliminar");

            if (contiene) {
                $("#ListadoImagenes").html(data);
                toastr.success("La foto ha sido eliminada correctamente");
            } else {
                location.reload(true);
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