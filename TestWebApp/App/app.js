(function (window, angular) {
    'use strict';    

    window.controllers = angular.module('controllers', []);
    window.directives = angular.module('directives', []);
    window.services = angular.module('services', []);

    window.app = angular.module('app', [        
        'ngResource',
        'ui.router',
        'services',
        'directives',
        'controllers'        
    ]);

    var exceptionHandler = function (e) {        
        console.log(e.message | e);
    };
    
    app.value('$exceptionHandler', exceptionHandler);

    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state('home', {
                url: '/home',
                views: {
                    '': {
                        templateUrl: 'app/views/home.html',
                        controller: 'HomeCtrl',
                        controllerAs: 'homevm'
                    }
                }
            })
            
    });

    app.run(['$rootScope', '$location', '$window',
        function ($rootScope, $location, $window) {
            
        }]);

})(window, angular);