angular.module('electronicStoreApp.screens.contact',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.services.contact',
        'angularUtils.directives.dirPagination',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common',
        'ngSanitize',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.bootstrap.modal'
	])
	.config(function ($stateProvider) {

	    $stateProvider
			.state('contact-us', {
			    url: "/contact-us",
			    parent: 'layout',
			    templateUrl: "/app/screens/systems/contact/contact.html"
			});
	})
	.controller('contactController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter', 'contactSvc', 
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter, contactSvc) {

            $scope.title = 'Thông tin liên hệ';

            $scope.contact = {};
            
            var loadData = function () {
                contactSvc.getContactInfo().then(function (response) {
                    $scope.contact = response.data;
                    
                }, function (response) {
                    console.log(response);
                });
            }

            loadData();

            $scope.update = function () {
                contactSvc.updateContactInfo($scope.contact).then(function (response) {
                    $state.go($state.current, {}, { reload: true });
                }, function (response) {
                    console.log(response);
                });
            }
        }
	]);