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
        'electronicStoreApp.screens'
    ]);

angular.module('electronicStoreApp.global', []);
angular.module('electronicStoreApp.global.common', []);
angular.module('electronicStoreApp.global.filters', []);
angular.module('electronicStoreApp.global.directives', []);
angular.module('electronicStoreApp.global.layout', []);
angular.module('electronicStoreApp.global.services', []);
angular.module('electronicStoreApp.global.services.products', []);

angular.module('electronicStoreApp.screens', [
    'electronicStoreApp.screens.login',
    'electronicStoreApp.screens.home',
    'electronicStoreApp.screens.products',
    'electronicStoreApp.screens.productCategories'
]);



angular.module('electronicStoreApp.screens.login', []);
angular.module('electronicStoreApp.screens.home', []);
angular.module('electronicStoreApp.screens.products', []);
angular.module('electronicStoreApp.screens.productCategories', []);

electronicStoreApp.config([
       '$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $stateProvider.state('layout', {
                url: '',
                templateUrl: '/app/global/view/layout.html',
                abstract: true
            });
            $urlRouterProvider.otherwise("/home");
        }
])