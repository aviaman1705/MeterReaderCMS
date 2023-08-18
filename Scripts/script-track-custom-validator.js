$('#trackFormId').validate({
    errorClass: 'help-block animation-slideDown invalid-feedback', // You can change the animation class for a different entrance animation - check animations page
    errorElement: 'div',
    errorPlacement: function (error, e) {
        e.parents('.form-row > div').append(error);
    },
    highlight: function (e) {
        $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
        $(e).closest('.help-block').remove();
    },
    success: function (e) {
        e.closest('.form-group').removeClass('has-success has-error');
        e.closest('.help-block').remove();
    }, rules: {
        'Date': {
            required: true
        },
        'Called': {
            required: false,
            number: true,
            min:0
        },
        'UnCalled': {
            required: false,
            number: true,
            min: 0
        },
        'Desc': {
            required: true,
            minlength: 6
        }   
    }, messages: {
        'Date': 'שדה תאריך הוא שדה חובה',
        'Called': 'לא ניתן להזין מספר שלילי',
        'UnCalled': 'לא ניתן להזין מספר שלילי',
        'Desc': {
            required: 'שדה תיאור הוא שדה חובה',
            minlength: 'תיאור חייב להיות באורך של 6 תווים לפחות'
        }
    }
});