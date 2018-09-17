function GoToDetail(url) {
    window.location.href = url;
}
$(function () {
    $(".createPage").click(function () {
        debugger;
        var $buttonClicked = $(this);
        var item = $buttonClicked.attr('data-id');
        var CreateUrl = '/' + item + '/Create';
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: CreateUrl,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                debugger;
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});
$("#myModal").on("submit", "#form-createBobbinDebit", function (e) {
    e.preventDefault();  // prevent standard form submission

    var form = $(this);
    $.ajax({
        url: form.attr("action"),
        method: form.attr("method"),  // post
        data: form.serialize(),
        success: function (partialResult) {     
            if (partialResult === "True") {
                location.reload();
            }
            $("#createBobbinDebit").html(partialResult);
            $('.chosen-select').chosen({ width: "100%" });            
        }
    });
});
function handleDelete(e, stop, link) {
    if (stop) {
        e.preventDefault();
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: link,
                    type: "Delete",
                    dataType: "html",
                    success: function (data) {
                        swal({ title: "Deleted!", text: "Your imaginary file has been deleted.", type: "success" },
                            function () {
                                window.location.reload(true);
                            });
                    },
                    error: function (xhr, status, error) {
                        swal({ title: "Fout!", text: "Sorry, kon werf niet verwijderen.\nExtra informatie: " + error, type: "error" },
                            function () {

                            });
                    }
                });
            }
        });
    }
};
