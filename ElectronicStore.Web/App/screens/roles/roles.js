angular.module('electronicStoreApp.screens.roles',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common',
        'ngSanitize',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.bootstrap.modal'
	])
	.config(function ($stateProvider) {

	    $stateProvider
			.state('roles', {
			    url: "/roles",
			    parent: 'layout',
			    templateUrl: "/app/screens/roles/list.html"
			});

	    $stateProvider
			.state('roles.new', {
			    url: "/roles/new",
			    parent: 'layout',
			    templateUrl: "app/screens/roles/new.html"
			});

	    $stateProvider
			.state('roles.edit', {
			    url: "/roles/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/roles/edit.html"
			});

	})
	.controller('rolesListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter) {

            $scope.title = 'Danh sách quyền';
        }
	])
	.controller('rolesNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc) {

		    $scope.title = 'Thêm mới quyền';


		}
	])
    .controller('rolesEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc) {

		    $scope.title = 'Cập nhập quyền';

		}
	]);