angular.module('electronicStoreApp.screens.roles',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.services.roles',
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
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter', 'roleSvc',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter, roleSvc) {

            $scope.title = 'Danh sách quyền';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;
            $scope.totalPages = 0;
            $scope.currentPage = 1;

            var getListRoles = function () {
                var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
                roleSvc.getListRole($scope.keyword, skip, $scope.itemsPerPage).then(function (response) {
                    $scope.roles = response.data.Results;
                    console.log(response);
                    $scope.totalPages = response.data.TotalPages;
                    $scope.currentPage = response.data.CurrentPage;
                    $scope.totalItems = response.data.TotalResults;

                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }
            getListRoles();

            $scope.search = function () {
                getListRoles();
            }

            $scope.deleteRole = function (item) {
                $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.Name + " ?").then(function () {
                    var id = item.Id;
                    roleSvc.deleteRole(id).then(function (response) {
                        $state.reload();
                    }, function (error) {
                        console.log(error);
                    });
                });
            }
        }
	])
	.controller('rolesNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc', 'roleSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc, roleSvc) {

		    $scope.title = 'Thêm mới quyền';
		    $scope.role = {
		        Id: '00000000-0000-0000-0000-000000000000',
		        Name: '',
		        Description: ''
		    }

		    $scope.addNewRole = function () {
		        $scope.submitted = true;
		        if ($scope.role.Name === '' || $scope.role.Name === null || $scope.role.Name === undefined || $scope.role.Name.length > 256) {
		            return;
		        }

		        if ($scope.role.Description.length > 256) {
		            return;
		        }

		        roleSvc.addNewRole($scope.role).then(function (record) {
		            alert('Thêm mới quyền thành công!');
		            $state.go('roles');
		        }, function (error) {
		            console.log(error);
		        });
		    };

		    

		}
	])
    .controller('rolesEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc', 'roleSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc, roleSvc) {

		    $scope.title = 'Cập nhập quyền';
		    $scope.role = {};

		    function loadDetail() {
		        roleSvc.getRoleById($stateParams.id).then(function (response) {
		            $scope.role = response.data;
		        }, function(error){
		            console.log(error);
		        });
		    }
		    loadDetail();

		    $scope.updateRole = function () {
		        $scope.submitted = true;
		        if ($scope.role.Name === '' || $scope.role.Name === null || $scope.role.Name === undefined || $scope.role.Name.length > 256) {
		            return;
		        }

		        if ($scope.role.Description.length > 256) {
		            return;
		        }

		        roleSvc.updateRole($scope.role).then(function (record) {
		            alert('Cập nhật ' + $scope.role.Name + ' thành công!');
		            $state.go('roles');
		        }, function (error) {
		            console.log(error);
		        });
		    }
		}
	]);