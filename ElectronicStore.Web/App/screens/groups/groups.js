angular.module('electronicStoreApp.screens.groups',
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
			.state('groups', {
			    url: "/groups",
			    parent: 'layout',
			    templateUrl: "/app/screens/groups/list.html"
			});

	    $stateProvider
			.state('groups.new', {
			    url: "/groups/new",
			    parent: 'layout',
			    templateUrl: "app/screens/groups/new.html"
			});

	    $stateProvider
			.state('groups.edit', {
			    url: "/groups/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/groups/edit.html"
			});

	})
	.controller('groupsListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter) {

            $scope.title = 'Danh sách nhóm';
        }
	])
	.controller('groupsNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, commonSvc) {

		    $scope.title = 'Thêm mới nhóm';


		}
	])
    .controller('groupsEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, commonSvc) {

		    $scope.title = 'Cập nhập nhóm';

		}
	]);