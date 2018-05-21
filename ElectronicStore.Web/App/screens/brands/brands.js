angular.module('electronicStoreApp.screens.brands',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.services.brands',
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
			.state('brands', {
			    url: "/brands",
			    parent: 'layout',
			    templateUrl: "/app/screens/brands/list.html"
			});
	    $stateProvider
			.state('brands.new', {
			    url: "/brands/new",
			    parent: 'layout',
			    templateUrl: "app/screens/brands/new.html"
			});

	    $stateProvider
			.state('brands.edit', {
			    url: "/brands/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/brands/edit.html"
			});
	})
	.controller('brandListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'orderSvc', 'brandSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, orderSvc, brandSvc) {

            $scope.title = 'Danh sách hãng sản xuất';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;
            $scope.totalPages = 0;
            $scope.currentPage = 1

            var getListBrand = function () {
                var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
                brandSvc.getListBrand($scope.keyword, skip, $scope.itemsPerPage).then(function (response) {
                    $scope.brands = response.data.Results;
                    $scope.totalPages = response.data.TotalPages;
                    $scope.currentPage = response.data.CurrentPage;
                    $scope.totalItems = response.data.TotalResults;
                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }
            getListBrand();

            $scope.search = function () {
                getListBrand();
            }

            $scope.deleteBrand = function (item) {
                $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.Name + " ?").then(function () {
                    var id = item.Id;
                    brandSvc.deleteBrand(id).then(function (response) {
                        $state.reload();
                    }, function (error) {
                        console.log(error);
                    });
                });
            }
        }
	])
.controller('brandNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'commonSvc', 'brandSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, commonSvc, brandSvc) {

		    $scope.title = 'Thêm mới hãng sản xuất';
		    $scope.submitted = false;

		    $scope.brand = {
		        Status: true,
		        Name: '',
		        Alias: '',
		        Logo: ""
		    }

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.brand.Logo = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    $scope.addNewBrand = function () {
		        $scope.submitted = true;

		        if ($scope.brand.Name === undefined || $scope.brand.Name === '' || $scope.brand.Name === null || $scope.brand.Name.length > 256) {
		            return;
		        }

		        if ($scope.brand.Alias === undefined || $scope.brand.Alias === '' || $scope.brand.Alias === null || $scope.brand.Alias.length > 256) {
		            return;
		        }

		        brandSvc.addNewBrand($scope.brand).then(function (record) {
		            alert('Thêm mới ' + $scope.brand.Name + ' thành công!');
		            $state.go('brands');
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.brand.Alias = commonSvc.getAlias(input);
		    }
		}
	])
.controller('brandEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productSvc', 'commonSvc', 'brandSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productSvc, commonSvc, brandSvc) {

		    $scope.title = 'Cập nhật hãng sản xuất';
		    $scope.submitted = false;
		    $scope.brand = {};

		    var loadDetail = function () {
		        brandSvc.getBrandById(parseInt($stateParams.id)).then(function (response) {
		            $scope.brand = response.data;
		            console.log($scope.brand);
		        }, function (err) {
		            console.log(err);
		        });
		    }

		    loadDetail();

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.brand.Logo = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    $scope.updateBrand = function () {
		        $scope.submitted = true;

		        if ($scope.brand.Name === undefined || $scope.brand.Name === '' || $scope.brand.Name === null || $scope.brand.Name.length > 256) {
		            return;
		        }

		        if ($scope.brand.Alias === undefined || $scope.brand.Alias === '' || $scope.brand.Alias === null || $scope.brand.Alias.length > 256) {
		            return;
		        }

		        brandSvc.updateBrand($scope.brand).then(function (record) {
		            alert('Cập nhật ' + $scope.brand.Name + ' thành công!');
		            $state.go('brands');
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.brand.Alias = commonSvc.getAlias(input);
		    }
		}
	]);