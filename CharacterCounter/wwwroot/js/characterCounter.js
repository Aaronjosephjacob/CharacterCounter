function GetCharacterCount() {
    if (!ValidateForm()) {
        return false;
    }

    $.ajax({
        type: "GET",
        url: "/CharacterCounter/GetCharacterCount",
        data: { word: $("#txtInputBox").val().trim() },
        contentType: "application/json",
        headers: {
            'Access-Control-Allow-Origin': '*',
        },
        success: function (response) {
            if (response != null) {
                if (response.status == 1) {
                    var details = '';

                    for (var i = 0; i < response.data.length; i++) {
                        details += '<tr>';
                        details += '<td>' + response.data[i].letter + '</td>';
                        details += '<td>' + response.data[i].count + '</td>';
                        details += '</tr>';
                    }

                    var html = '<table class="table table-bordered">'
                        + '<thead>'
                        + '<tr>'
                        + '<th>Character</th>'
                        + '<th>Count</th>'
                        + '</tr>'
                        + '</thead>'
                        + '<tbody>'
                        + details
                        + '</tbody>'
                        + '</table>';

                    $("#dvContent").html(html);
                }
                else if (response.status == 2) {
                    $("#spnInputBox").html(response.message);
                    $("#dvContent").html('');
                }
            }
        },
        error: function (err) {
            console.log(err, 'ajax GetCharacterCount error...');
        }
    });
}

function ValidateForm() {
    var isValid = true;

    if ($("#txtInputBox").val().trim() == "") {
        $("#spnInputBox").html("Please enter a word or a sentence");
        $("#txtInputBox").css("border-color", "red");
        $("#dvContent").html('');
        isValid = false;
    }
    else {
        $("#spnInputBox").html("");
        $("#txtInputBox").css("border-color", "");
    }

    return isValid;
}