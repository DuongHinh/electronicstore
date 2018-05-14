var account = {
    init: function () {
        account.registerEvents();
    },
    registerEvents: function () {
        $('#btnSignOut').off('click').on('click', function (res) {
            res.preventDefault();
            $('#formSignOut').submit();
        });
    }
}
account.init();