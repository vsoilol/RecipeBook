
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$("#ImageDataFile").change(function () {
    readURL(this);
});

//$(document).ready(function () {
//    $("#ImageData").change(function () {
//        readURL(this);
//    });
//});

jQuery.validator.addMethod("lettersonly", function (value, element) {
    return this.optional(element) || /^[a-zа-я]+$/i.test(value);
}, "Letters only please");

$(".box").validate({
    rules: {
        Name: {
            required: true,
            lettersonly: true,
        },
        //ImageData: {
        //    required: true,
        //},
    },
    messages: {
        Name: {
            required: "The field must be filled",
            lettersonly: "The name must consist of letters",
        },
        //ImageData: {
        //    required: "The field must be filled",
        //},
    },
});