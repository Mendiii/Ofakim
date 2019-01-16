$(document).ready(function () {

    for (var i = 0; i < 31; i++) {
        $('#day').append($('<option>', {
            value: i + 1,
            text: i + 1
        }));
    }

    for (var i = 0; i < 12; i++) {
        $('#month').append($('<option>', {
            value: i + 1,
            text: i + 1
        }));
    }

    for (var i = 1900; i < 2019; i++) {
        $('#year').append($('<option>', {
            value: i + 1,
            text: i + 1
        }));
    }

    $("#registerNewUser").click(function (e) {
        e.preventDefault();

        if ($("#email").val() === "" || $("#FullName").val() === "" || $("#PhoneNumber").val() === "" || $("#PhoneNumber").val() === "" || $("#Gender option:selected").text() === ''
            || $("#day").val() === "" || $("#month").val() === "" || $("#year").val() === ""
        ) {
            alert("Some information is missing.")
            return;
        }

        var user = {
            MailAddress: $("#email").val(),
            birthDate: $("#day").val() + '/' + $("#month").val() + '/' + $("#year").val(),
            FullName: $("#FullName").val(),
            PhoneNumber: $("#PhoneNumber").val(),
            Gender: $("#Gender option:selected").text()
        }

        $.ajax({
            type: "POST",
            url: "/api/Values",
            data: user,
            success: function (result) {
                window.location.href = '/Home/UsersList';
            },
            error: function (result) {
                alert('Something went wrong. try again!');
            }
        });
    });

    $("#registerNewUserNoDB").click(function (e) {
        e.preventDefault();

        if ($("#email").val() === "" || $("#FullName").val() === "" || $("#PhoneNumber").val() === "" || $("#PhoneNumber").val() === "" || $("#Gender option:selected").text() === ''
            || $("#day").val() === "" || $("#month").val() === "" || $("#year").val() === ""
        ) {
            alert("Some information is missing.")
            return;
        }

        var oldItems = JSON.parse(localStorage.getItem('itemsArray')) || [];

        var user = {
            MailAddress: $("#email").val(),
            birthDate: $("#day").val() + '/' + $("#month").val() + '/' + $("#year").val(),
            FullName: $("#FullName").val(),
            PhoneNumber: $("#PhoneNumber").val(),
            Gender: $("#Gender option:selected").text()
        }

        oldItems.push(user);

        localStorage.removeItem('itemsArray');
        localStorage.setItem('itemsArray', JSON.stringify(oldItems));

        window.location.href = '/Home/NoDataBase';
    });




});

