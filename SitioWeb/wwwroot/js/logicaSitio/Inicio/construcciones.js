$(document).ready(function (e) {

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