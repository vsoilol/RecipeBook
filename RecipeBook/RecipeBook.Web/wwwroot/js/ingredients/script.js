jQuery.validator.addMethod(
    "regex",
    function (value, element, regexp) {
        if (regexp.constructor != RegExp)
            regexp = new RegExp(regexp);
        else if (regexp.global)
            regexp.lastIndex = 0;
        return this.optional(element) || regexp.test(value);
    }, "erreur expression reguliere"
);

jQuery.validator.addMethod("lettersonly", function (value, element) {
    return this.optional(element) || /^[a-zа-я]+$/i.test(value);
}, "Letters only please"); 

$(".box").validate({
    rules: {
        Weight: {
            required: true,
            regex: "^[-+]?[0-9]*[,]?[0-9]+(?:[eE][-+]?[0-9]+)?$"
        },
        Name: {
            required: true,
            lettersonly: true,
        }
    },
    messages: {
        Weight: {
            required: "The field must be filled",
            regex: "Enter a number"
        },
        Name: {
            required: "The field must be filled",
            lettersonly: "The name must consist of letters",
        }
    },
});
