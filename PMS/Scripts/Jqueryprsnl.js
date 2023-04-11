function ClearFields() {
    $('#ProductGenericName').val("");
    $('#ProductDescription').val("");
    $('#ProductTitle').val("");
    $('#ProductWeight').val("");
    $('#ProductPrice').val("");
    $('#ImageFile').val("");


}

$(document).ready(function () {
    var textboxes = $('input[type="text"]');
    textboxes.focus(function () {
        var helpdiv = $(this).attr('id') + 'Helpdiv';
        $('#' + helpdiv).load('Pages #' + helpdiv);
    });
    var Numberbox = $('input[type ="number"]');
    Numberbox.focus(function () {
        var helpdiv = $(this).attr('id') + 'Helpdiv';
        $('#' + helpdiv).load('Pages #' + helpdiv);
    });
    textboxes.blur(function () {
        var helpdiv = $(this).attr('id') + 'Helpdiv';
        $('#' + helpdiv).html('');
    });
    Numberbox.blur(function () {
        var helpdiv = $(this).attr('id') + 'Helpdiv';
        $('#' + helpdiv).html('');
    });
});