angular.module('electronicStoreApp.global.layout',
	[
		'ui.router', 'electronicStoreApp.global.services.auth'
	])
	.controller('layoutController', [
		'$scope', '$state', '$rootScope', 'authSvc', 'authData',
		function ($scope, $state, $rootScope, authSvc, authData) {
		    $rootScope.logo = 'Electronic Store';

		    $scope.authInformation = authData.authInformation;

		    $scope.logout = function () {
		        authSvc.logout();
		    };
		}
	])
;