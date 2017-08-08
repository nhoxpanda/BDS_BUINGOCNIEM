function getDistrict(provinceId) {
    $("#district").html("<option value=''>Quận/Huyện</option>");
    var defaultValue = $("#district").data("value");
    if (provinceId !== "") {
        $("#district").html("<option value=''>Loading...</option>").prop('disabled', true);
        $.ajax({
            'url': "api/Home",
            'type': "post",
            'data': { 'ProvinceId': provinceId },
            success: function (data) {
                $("#district").html("<option value=''>Quận/Huyện</option>");
                $.each(data, function (index, val) {
                    $("#district").append("<option value='" + val.Id + "' " + (defaultValue === val.Id ? "selected" : "") + ">" + val.Name + "</option>");
                });
            },
            error: function (msg) {
                $("#district").html("<option value=''>Quận/Huyện</option>");
            },
            complete: function () {
                $("#district").prop("disabled", false);
            }
        });
    }
}
jQuery(document).ready(function ($) {
    $("#province").change(function (event) {
        getDistrict($(this).val());
    });

    if ($("#province").val() !== "") {
        getDistrict($("#province").val());
    }
    $("#realSearchForm").submit(function () {
        $("#realSearchForm button[type='submit']").prop('disabled', true);
        $(this).find("input[name], select[name]").each(function () {
            if (!$(this).val()) {
                $(this).prop("disabled", true);
            }
        });
        return true;
    });
    // Un-disable form fields when page loads, in case they click back after submission
    //$( "#realSearchForm" ).find( ":input" ).prop( "disabled", false );
});