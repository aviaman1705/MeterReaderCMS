
$.validator.addMethod("validateDate",
    function (value, element) {
        return /^\d{2}\/\d{2}\/\d{4}$/.test(value);
    },
    'תאריך חייב להיות בפורמט {mm/dd/yyyy}'
);


$('#trackFormId').validate({
    errorClass: 'help-block animation-slideDown text-danger', 
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
        'NoteBookID': {
            required: true
        },
        'Date': {
            required: true,
            validateDate: true,
        },
    }, messages: {
        NoteBookID:
        {
            required: 'חובה לבחור מספר פנקס'
        },

        Date: {
            required: 'חובה לבחור תאריך'
        }
    }
})

