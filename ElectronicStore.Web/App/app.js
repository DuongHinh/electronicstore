/// <reference path="/Assets/Admin/js/angular/angular.min.js" />
var electronicStoreApp = angular.module('electronicStoreApp',
    [
        'ui.router',
        'electronicStoreApp.global',
        'electronicStoreApp.global.common',
        'electronicStoreApp.global.filters',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.layout',
        'electronicStoreApp.screens',
    ]);

angular.module('electronicStoreApp.global', []);
angular.module('electronicStoreApp.global.common', []);
angular.module('electronicStoreApp.global.filters', []);
angular.module('electronicStoreApp.global.directives', []);
angular.module('electronicStoreApp.global.layout', []);
angular.module('electronicStoreApp.global.services', []);


angular.module('electronicStoreApp.screens', [
    'electronicStoreApp.screens.login',
    'electronicStoreApp.screens.home',
    'electronicStoreApp.screens.products',
    'electronicStoreApp.screens.productCategories',
    'electronicStoreApp.screens.users',
    'electronicStoreApp.screens.roles',
    'electronicStoreApp.screens.groups',
    'electronicStoreApp.screens.orders',
    'electronicStoreApp.screens.feedbacks',
    'electronicStoreApp.screens.statistics',
    'electronicStoreApp.screens.brands'
]);



angular.module('electronicStoreApp.screens.login', []);
angular.module('electronicStoreApp.screens.home', []);
angular.module('electronicStoreApp.screens.products', []);
angular.module('electronicStoreApp.screens.productCategories', []);
angular.module('electronicStoreApp.screens.users', []);
angular.module('electronicStoreApp.screens.roles', []);
angular.module('electronicStoreApp.screens.groups', []);
angular.module('electronicStoreApp.screens.orders', []);
angular.module('electronicStoreApp.screens.feedbacks', []);
angular.module('electronicStoreApp.screens.statistics', []);
angular.module('electronicStoreApp.screens.brands', [])

electronicStoreApp.config([
       '$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $stateProvider.state('layout', {
                url: '',
                templateUrl: '/app/global/template/layout.html',
                abstract: true
            });
            $urlRouterProvider.otherwise("/home");
        }
]).config([
    '$stateProvider', '$httpProvider',
     function ($stateProvider, $httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
]);