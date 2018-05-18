angular.module('electronicStoreApp.screens.users',
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
			.state('users', {
			    url: "/users",
			    parent: 'layout',
			    templateUrl: "/app/screens/users/list.html"
			});

	    $stateProvider
			.state('users.new', {
			    url: "/users/new",
			    parent: 'layout',
			    templateUrl: "app/screens/users/new.html"
			});

	    $stateProvider
			.state('users.edit', {
			    url: "/uses/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/users/edit.html"
			});

	})
	.controller('usersListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter) {

            $scope.title = 'Danh sách người dùng';
        }
	])
	.controller('usersNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc) {

		    $scope.title = 'Thêm mới người dùng';
		    

		}
	])
    .controller('usersEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc) {

		    $scope.title = 'Cập nhập người dùng';
		    
		}
	]);