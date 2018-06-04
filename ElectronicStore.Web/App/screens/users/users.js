angular.module('electronicStoreApp.screens.users',
	[
		'ui.router',
        'electronicStoreApp.global.services.auth',
        'electronicStoreApp.global.services',
        'electronicStoreApp.services.users',
        'electronicStoreApp.services.groups',
        'electronicStoreApp.global.directives',
        'angularUtils.directives.dirPagination',
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
 		'$scope', '$state', '$stateParams', '$rootScope', '$ngBootbox', '$filter', 'userSvc', 'groupSvc', 'authData',
        function ($scope, $state, $stateParams, $rootScope, $ngBootbox, $filter, userSvc, groupSvc, authData) {

            $scope.title = 'Danh sách người dùng';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;
            //$scope.totalPages = 0;
            //$scope.currentPage = 1;

            $scope.currentUser = authData.authInformation;

            var getListUsers = function () {
                
                userSvc.getListUsers($scope.keyword).then(function (response) {
                    $scope.users = response.data;
                    $scope.loading = false;
                    //console.log($scope.users);
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }
            getListUsers();

            $scope.search = function () {
                getListUsers();
            }

            $scope.deleteUser = function (item) {
                $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.UserName + " ?").then(function () {
                    if ($scope.currentUser.username === item.UserName) {
                        alert("Bạn không có quyền xóa người này");
                        return;
                    }

                    var id = item.Id;
                    return;
                    userSvc.deleteUser(id).then(function (response) {
                        $state.reload();
                    }, function (error) {
                        console.log(error);
                    });
                });
            }
        }
	])
	.controller('usersNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'productCategoriesSvc', 'commonSvc', 'userSvc', 'groupSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, productCategoriesSvc, commonSvc, userSvc, groupSvc) {

		    $scope.title = 'Thêm mới người dùng';
		    $scope.submitted = false;
		    $scope.user = {
		        Groups: []
		    }

		    var loadGroup = function () {
		        groupSvc.getAll().then(function (response) {
		            $scope.groups = response.data;
		            //console.log($scope.groups);
		        }, function (error) {
		            console.log(error);
		        });
		    }
		    loadGroup();

		    var getListUsers = function () {

		        userSvc.getListUsers("").then(function (response) {
		            $scope.users = response.data;           
		            //console.log($scope.users);
		        }, function (error) {
		            console.log(error);
		        });
		    }
		    getListUsers();

		    var validateEmail = function (email) {
		        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		        return re.test(email);
		    }
		    $scope.resultCompare = true;

		    var comparePassword = function (input1, input2) {
		        $scope.resultCompare = angular.equals(input1, input2);
		        return $scope.resultCompare;
		    };

		    $scope.addNewUser = function () {
		        $scope.submitted = true;
                
		        var checkName = true;
		        for (var i = 0, len = $scope.users.length; i < len; i++) {
		            if ($scope.users[i].UserName === $scope.user.UserName) {
		                checkName = false;
		                break;
		            }
		        }

		        if (checkName === false) {
		            alert("Tên tài khoản đã tồn tại");
		            return;
		        }

		        var checkEmail = true;
		        for (var i = 0, len = $scope.users.length; i < len; i++) {
		            if ($scope.users[i].Email === $scope.user.Email) {
		                checkEmail = false;
		                break;
		            }
		        }

		        if (checkEmail === false) {
		            alert("Địa chỉ email đã tồn tại");
		            return;
		        }

		        if ($scope.user.LastName === undefined || $scope.user.LastName === '' || $scope.user.LastName === null || $scope.user.LastName.length > 256) {
		            return;
		        }

		        if ($scope.user.FirstName === undefined || $scope.user.FirstName === '' || $scope.user.FirstName === null || $scope.user.FirstName.length > 256) {
		            return;
		        }

		        if ($scope.user.MiddleName != null && $scope.user.MiddleName.length > 256) {
		            return;
		        }

		        if ($scope.user.Address != null && $scope.user.Address.length > 256) {
		            return;
		        }

		        if ($scope.user.PhoneNumber != null && $scope.user.PhoneNumber.length > 50) {
		            return;
		        }

		        if ($scope.user.Email === undefined || $scope.user.Email === '' || $scope.user.Email === null || validateEmail($scope.user.Email) === false) {
		            return;
		        }

		        if ($scope.user.UserName === undefined || $scope.user.UserName === '' || $scope.user.UserName === null || $scope.user.UserName.length > 256) {
		            return;
		        }

		        if ($scope.user.Password === undefined || $scope.user.Password === '' || $scope.user.Password === null || $scope.user.Password.length < 6) {
		            return;
		        }

		        if ($scope.user.ConfirmPassword === undefined || $scope.user.ConfirmPassword === '' || $scope.user.ConfirmPassword === null || $scope.user.ConfirmPassword.length < 6) {
		            return;
		        }

		        if (!comparePassword($scope.user.Password, $scope.user.ConfirmPassword)) {
		            return;
		        }

		        userSvc.addNewUser($scope.user).then(function (response) {
		            alert('Thêm mới người dùng thành công!');
		            $state.go('users');
		        }, function (error) {
		            console.log(error);
		        });
		    }
		    

		}
	])
    .controller('usersEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'userSvc', 'groupSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, userSvc, groupSvc, commonSvc) {

		    $scope.title = 'Cập nhập người dùng';
		    $scope.submitted = false;
		    $scope.user = {};

		    var convertDate = function (inputDate) {
		        return new Date(inputDate);
		    }

		    var getListUsers = function () {

		        userSvc.getListUsers("").then(function (response) {
		            $scope.users = response.data;
		            //console.log($scope.users);
		        }, function (error) {
		            console.log(error);
		        });
		    }
		    getListUsers();

		    var loadDetail = function () {
		        userSvc.getUserById($stateParams.id).then(function (response) {
		            $scope.user = response.data;
		            $scope.BirthDay = new Date($scope.user.BirthDay);
		            //console.log($scope.user);
		            //console.log($scope.BirthDay);
		        }, function (error) {
		            console.log(error)
		        });
		    }

		    loadDetail();

		    var loadGroup = function () {
		        groupSvc.getAll().then(function (response) {
		            $scope.groups = response.data;
		            //console.log($scope.groups);
		        }, function (error) {
		            console.log(error);
		        });
		    }
		    loadGroup();

		    var validateEmail = function (email) {
		        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		        return re.test(email);
		    }


		    $scope.updateUser = function () {

		        $scope.submitted = true;


		        if ($scope.user.LastName === undefined || $scope.user.LastName === '' || $scope.user.LastName === null || $scope.user.LastName.length > 256) {
		            return;
		        }

		        if ($scope.user.FirstName === undefined || $scope.user.FirstName === '' || $scope.user.FirstName === null || $scope.user.FirstName.length > 256) {
		            return;
		        }

		        if ($scope.user.MiddleName != null && $scope.user.MiddleName.length > 256) {
		            return;
		        }

		        if ($scope.user.Address != null && $scope.user.Address.length > 256) {
		            return;
		        }

		        if ($scope.user.PhoneNumber != null && $scope.user.PhoneNumber.length > 50) {
		            return;
		        }

		        if ($scope.user.Email === undefined || $scope.user.Email === '' || $scope.user.Email === null || validateEmail($scope.user.Email) === false) {
		            return;
		        }

		        if ($scope.user.UserName === undefined || $scope.user.UserName === '' || $scope.user.UserName === null || $scope.user.UserName.length > 256) {
		            return;
		        }

		        userSvc.updateUser($scope.user).then(function (response) {
		            alert('Cập nhật người dùng thành công!');
		            $state.go('users');
		        }, function (error) {
		            console.log(error);
		        });
		    }
 
		}
	]);