(function (angular, modernizr) {
    'use strict';

    controllers.controller('HomeCtrl', ['$scope', '$rootScope', 'NumberToTextService', function ($scope, $rootScope, NumberToTextService) {
        var homevm = this;
        $scope.title = 'Home';        
        homevm.user = {
            name: null,
            number: null
        };
        homevm.response = {
            name: null,
            numToText: null
        }
        function reset() {
            homevm.response = {
                name: null,
                numToText: null,
                error: null
            }
        }

        homevm.Convert = function () {
            reset();
            NumberToTextService.ConvertToText(homevm.user.number).then(function (data) {
                if (data && data.hasOwnProperty('results')) {
                    if (data.results.hasOwnProperty('Error') && data.results.Error != null) {
                        homevm.response.error = data.results.Error;
                    } else {
                        homevm.response.error = null;
                        homevm.response.numToText = data.results.Output;
                        homevm.response.name = homevm.user.name;
                    }
                }
            });
        }

    }]);

})(window.angular, Modernizr);