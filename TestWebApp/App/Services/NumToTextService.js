(function (angular, $) {
    'use strict';

    services.factory('NumberToTextService', ['$q', '$http', function ($q, $http) {

        var self = {};

        self.ConvertToText = function (num) {
            var deferred = $q.defer();
            var data = {
                Number: num                
            };
            $http({
                method: 'POST',
                url: 'api/NumToText/ConvertToText',
                data: $.param(data),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            })
            .success(function (data) {                
                deferred.resolve({
                    results: data
                });
            })
            .error(function (data) {
                deferred.reject();
            });

            return deferred.promise;
        };

        return self;

    }]);

})(window.angular, window.jQuery);