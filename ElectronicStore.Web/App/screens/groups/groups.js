angular.module('electronicStoreApp.screens.groups',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.services.groups',
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
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter', 'groupSvc',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter, groupSvc) {

            $scope.title = 'Danh sách nhóm';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;
            $scope.totalPages = 0;
            $scope.currentPage = 1;

            var getListGroups = function () {
                var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
                groupSvc.getListGroup($scope.keyword, skip, $scope.itemsPerPage).then(function (response) {
                    $scope.groups = response.data.Results;
                    $scope.totalPages = response.data.TotalPages;
                    $scope.currentPage = response.data.CurrentPage;
                    $scope.totalItems = response.data.TotalResults;

                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }
            getListGroups();

            $scope.search = function () {
                getListGroups();
            }

            $scope.deleteGroup = function (item) {
                $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.Name + " ?").then(function () {
                    var id = item.Id;
                    groupSvc.deleteGroup(id).then(function (response) {
                        $state.reload();
                    }, function (error) {
                        console.log(error);
                    });
                });
            }
        }
	])
	.controller('groupsNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'commonSvc', 'groupSvc', 'roleSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, commonSvc, groupSvc, roleSvc) {

		    $scope.title = 'Thêm mới nhóm';
		    $scope.group = {
		        Id: 0,
		        Roles: []
		    }
		    var loadAllRoles = function() {
		        roleSvc.getAll().then(function (response) {
		            $scope.roles = response.data;
		            //console.log($scope.roles);
		        }, function (err) {
		            console.log(err);
		        });                
		    }

		    loadAllRoles();

		    $scope.addNewGroup = function () {
		        $scope.submitted = true;
		        if ($scope.group.Name === '' || $scope.group.Name === null || $scope.group.Name === undefined) {
		            return;
		        }
		        if ($scope.group.Description != null && $scope.group.Description.length > 250) {
		            return;
		        }
		        groupSvc.addNewGroup($scope.group).then(function (record) {
		            alert('Thêm mới ' + $scope.group.Name + ' thành công!');
		            $state.go('groups');
		        }, function (error) {
		            console.log(error);
		        });
		    };
		}
	])
    .controller('groupsEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'commonSvc', 'groupSvc', 'roleSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, commonSvc, groupSvc, roleSvc) {

		    $scope.title = 'Cập nhập nhóm';

		    $scope.group = {}

		    function loadDetail() {
		        groupSvc.getGroupById(parseInt($stateParams.id)).then(function (response) {
		            $scope.group = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }
		    loadDetail();

		    var loadAllRoles = function () {
		        roleSvc.getAll().then(function (response) {
		            $scope.roles = response.data;
		            //console.log($scope.roles);
		        }, function (err) {
		            console.log(err);
		        });
		    }

		    loadAllRoles();

		    $scope.updateGroup = function () {
		        $scope.submitted = true;
		        if ($scope.group.Name === '' || $scope.group.Name === null || $scope.group.Name === undefined) {
		            return;
		        }

		        if ($scope.group.Description != null && $scope.group.Description.length > 250) {
		            return;
		        }

		        groupSvc.updateGroup($scope.group).then(function (record) {
		            alert('Cập nhật ' + $scope.group.Name + ' thành công!');
		            $state.go('groups');
		        }, function (error) {
		            console.log(error);
		        });
		    };

		}
	]);