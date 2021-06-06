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


$(".box").validate({
    rules: {
        Email: {
            required: true,
            regex: "^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$",
            },
Password: {
    required: true,
        minlength: 4
},
ConfirmPassword: {
    required: true,
        minlength: 4,
            equalTo: "#Password"
}
        },
messages: {
    Email: {
        required: "The field must be filled",
            regex: "Your email address must be in the format of name@domain.com",
            },
    Password: {
        required: "The field must be filled",
            minlength: "The length must be more than 4 characters"
    },
    ConfirmPassword: {
        required: "The field must be filled",
            minlength: "The length must be more than 4 characters",
                equalTo: "Passwords must match"
    }
},
    });