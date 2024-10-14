(function (app) {
    app.factory('toastrService', toastrService);

    function toastrService() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 5000,
            "extendedTimeOut": 1000
        };

        function success(message) {
            toastr.success(message);
        }

        function error(message) {
            if (Array.isArray(message)) {
                message.forEach(function (msg) {
                    toastr.error(msg);
                });
            } else {
                toastr.error(message);
            }
        }

        function info(message) {
            toastr.info(message);
        }

        function warning(message) {
            toastr.warning(message);
        }

        return {
            success: success,
            error: error,
            info: info,
            warning: warning
        };

    }
})(angular.module('tnine.services'));