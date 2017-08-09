function getChildren(provinceId, target, controller, valueDefault, domain) {
    var textValue = "";
    $(target).html(textValue);
    var defaultValue = $(target).data("value");
    if (provinceId !== "") {
        $(target).html("<option value=''>Loading...</option>").prop('disabled', true);
        $.ajax({
            'url': domain + "/Admin/api/" + controller,
            'type': "post",
            'data': { 'ProvinceId': provinceId },
            success: function (data) {
                $(target).html(textValue);
                $.each(data, function (index, val) {
                    $(target).append("<option value='" + val.Id + "' " + (defaultValue === val.Id ? "selected" : "") + ">" + val.Name + "</option>");
                });
            },
            error: function (msg) {
                $(target).html(textValue);
            },
            complete: function () {
                $(target).prop("disabled", false);
            }
        });
    }
}
