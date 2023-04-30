$(document).ready(function () {

    $(function () {

        $('.validaCed')
            .keypress(function (event) {
                if (event.which < 48 || event.which > 57 || this.value.length === 18) {
                    return false;
                }
            })
            .on("cut copy paste", function (event) {
                event.preventDefault();

            });

    });

    $(function () {

        $('.validanumericos')
            .keypress(function (event) {
                if (event.which < 48 || event.which > 57 || this.value.length === 13) {
                    return false;
                }
            })
            .on("cut copy paste", function (event) {
                event.preventDefault();

            });

    });

  
});