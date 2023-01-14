$('#addNotebookFormId').validate({
    errorClass: 'help-block animation-slideDown text-danger', // You can change the animation class for a different entrance animation - check animations page
    errorElement: 'div',
    errorPlacement: function (error, e) {
        e.parents('.form-group > div').append(error);
    },
    highlight: function (e) {
        $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
        $(e).closest('.help-block').remove();
    },
    success: function (e) {
        e.closest('.form-group').removeClass('has-success has-error');
        e.closest('.help-block').remove();
    },
    rules: {
        'Number': {
            required: true,
            minlength: 2,
            number: true
        },
        //'Password': {
        //    required: true,
        //    minlength: 6
        //},
        //'ConfirmPassword': {
        //    required: true,
        //    equalTo: '#Password'
        //}
    }, messages: {
        'Number':
        {
            required: 'שדה מספר פנקס הוא שדה חובה',
            minlength: 'מספר פנקס חייב להכיל 2 ספרות לפחות',
            number:'שדה זה מכיל רק ספרות'
        }
        //'Email': 'Please enter valid email address',
        //Password: {
        //    required: 'Please provide a password',
        //    minlength: 'Your password must be at least 6 characters long'
        //},
        //'ConfirmPassword': {
        //    required: 'Please provide a password',
        //    minlength: 'Your password must be at least 6 characters long',
        //    equalTo: 'Please enter the same password as above'
        //}
    }
})