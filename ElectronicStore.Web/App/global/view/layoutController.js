angular.module('electronicStoreApp.global.layout',
	[
		'ui.router'
	])
	.controller('layoutController', [
		'$scope', '$state', '$rootScope',
		function ($scope, $state, $rootScope) {
		    $rootScope.logo = 'Electronic Store';
		    $scope.user = {
		        fullName: 'Duong Hinh',
                roleName: 'Admin'
		    }

		    $scope.logout = function () {
		        $state.go("login");
		    };
		}
	])
;