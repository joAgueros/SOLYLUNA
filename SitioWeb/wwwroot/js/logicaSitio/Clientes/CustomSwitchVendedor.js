$('#customSwitch0').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }
    var idRefe = $("#refe0").val()

    var frm = new FormData();
    frm.append("Estado", valor);
    frm.append("idReferencias",idRefe);

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/vendedor/CambiarEstadoReferencia",
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

/********************INSTAGRAM************************/
$('#customSwitch1').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }
    var idRefe = $("#refe1").val()

    var frm = new FormData();
    frm.append("Estado", valor);
    frm.append("idReferencias", idRefe);

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/vendedor/CambiarEstadoReferencia",
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

/**********************************************/
$('#customSwitch2').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }
    var idRefe = $("#refe2").val()

    var frm = new FormData();
    frm.append("Estado", valor);
    frm.append("idReferencias", idRefe);

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/vendedor/CambiarEstadoReferencia",
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

/************************************************/
$('#customSwitch3').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }
    var idRefe = $("#refe3").val()

    var frm = new FormData();
    frm.append("Estado", valor);
    frm.append("idReferencias", idRefe);

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/vendedor/CambiarEstadoReferencia",
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
/***************************************************/
$('#customSwitch4').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }
    var idRefe = $("#refe4").val()

    var frm = new FormData();
    frm.append("Estado", valor);
    frm.append("idReferencias", idRefe);

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/vendedor/CambiarEstadoReferencia",
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
/*********************************************************/

$('#customSwitch5').click(function () {

    var valor = "";

    if ($(this).is(':checked')) {
        valor = "A";
    } else {
        valor = "I";
    }
    var idRefe = $("#refe5").val()

    var frm = new FormData();
    frm.append("Estado", valor);
    frm.append("idReferencias", idRefe);

    $.ajax({
        data: frm,
        contentType: false,
        processData: false,
        type: "post",  // Verbo HTTP
        url: "/admin/vendedor/CambiarEstadoReferencia",
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
/****************************************************/