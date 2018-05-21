angular.module('electronicStoreApp.screens.productCategories',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.global.services.productCategories',
        'angularUtils.directives.dirPagination',
        'electronicStoreApp.global.directives',
        'electronicStoreApp.global.common',
        'ngSanitize'
	])
	.config(function ($stateProvider) {

	    $stateProvider
			.state('productCategories', {
			    url: "/productCategories",
			    parent: 'layout',
			    templateUrl: "/app/screens/productCategories/list.html"
			});

	    $stateProvider
			.state('productCategories.new', {
			    url: "/productCategories/new",
			    parent: 'layout',
			    templateUrl: "app/screens/productCategories/new.html"
			});

	    $stateProvider
			.state('productCategories.edit', {
			    url: "/productCategories/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/productCategories/edit.html"
			});

	})
	.controller('productCategoriesListController',
	[
		'$scope', '$state', '$stateParams', '$rootScope', 'productCategoriesSvc', '$ngBootbox',
		function ($scope, $state, $stateParams, $rootScope, productCategoriesSvc, $ngBootbox) {
		    $scope.title = 'Danh mục sản phẩm';
		    $scope.loading = true;
		    $scope.keyword = '';
		    $scope.itemsPerPage = 10;
		    $scope.totalPages = 0;
		    $scope.currentPage = 1;

		    $scope.selectedCategoryIds = [];

		    var init = function () {
		        var skip = ($scope.currentPage - 1) * $scope.itemsPerPage;
		        productCategoriesSvc.getList($scope.keyword, skip, $scope.itemsPerPage).then(function (response) {
		            $scope.productCategories = response.data.Results;
		            $scope.totalPages = response.data.TotalPages;
		            $scope.currentPage = response.data.CurrentPage;
		            $scope.totalItems = response.data.TotalResults;

		            $scope.loading = false;
		        }, function (error) {
		            $scope.loading = false;
		            console.log(error);
		        });
		    }
		    init();

		    $scope.search = function () {
		        init();
		    }

		    $scope.delete = function (item) {
		        $ngBootbox.confirm("Are you sure delete " + item.Name + " ?").then(function () {
		            var id = item.Id;
		            productCategoriesSvc.delete(id).then(function (response) {
		                $state.reload();
		            }, function (error) {
		                console.log(error);
		            });
		        });
		    }

		    $scope.selectAll = function () {
		        if ($scope.selectedCategoryIds.length === $scope.productCategories.length) {
		            $scope.selectedCategoryIds = [];
		        } else {
		            // get all id of selectedProducts
		            $scope.selectedCategoryIds = _.pluck($scope.productCategories, 'Id');
		        }
		    }

		    $scope.selected = function (productCategory) {
		        return $scope.selectedCategoryIds.indexOf(productCategory.Id) !== -1;
		    };

		    $scope.select = function (productCategory) {
		        var index = $scope.selectedCategoryIds.indexOf(productCategory.Id);
		        if (index === -1) {
		            // add
		            $scope.selectedCategoryIds.push(productCategory.Id);
		        } else {
		            // remove
		            $scope.selectedCategoryIds.splice(index, 1);
		        }
		    };

		    $scope.deleteMulti = function () {
		        $ngBootbox.confirm("Are you sure delete " + $scope.selectedCategoryIds.length + " product categories?").then(function () {
		            var listCategoryIds = $scope.selectedCategoryIds.join();
		            productCategoriesSvc.deleteMulti(listCategoryIds).then(function (response) {
		                $state.reload();
		            }, function (error) {
		                console.log(error);
		            });
		        });
		    }
		   
		}
	])
	.controller('productCategoriesNewController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productCategoriesSvc, commonSvc) {
		    $scope.title = 'Thêm mới danh mục sản phẩm';
		    $scope.submitted = false;
		    $scope.category = {
		        Status: true,
		        Name: '',
		        Alias: ''
		    }

		    $scope.$on("fileProgress", function (e, progress) {
		        $scope.progress = progress.loaded / progress.total;
		    });

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.category.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    var loadParent = function () {
		        productCategoriesSvc.getAllCategories('').then(function (response) {
		            $scope.parents = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    loadParent();

		    $scope.addNew = function () {
		        $scope.submitted = true;
		        if ($scope.category.Name === '' || $scope.category.Name === null || $scope.category.Name.length > 256) {
		            return;
		        }

		        if ($scope.category.Alias === '' || $scope.category.Alias === null || $scope.category.Alias.length > 256) {
		            return;
		        }

		        productCategoriesSvc.addNew($scope.category).then(function (record) {
		            alert('Add new product category success!');
		            $state.go('productCategories', { reload: true });
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.category.Alias = commonSvc.getAlias(input);
		    }

		}
	])
    .controller('productCategoriesEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productCategoriesSvc', 'commonSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productCategoriesSvc, commonSvc) {
		    $scope.title = 'Cập nhật danh mục sản phẩm';
		    $scope.submitted = false;
		    $scope.getAlias = function (input) {
		        $scope.category.Alias = commonSvc.getAlias(input);
		    }

		    var loadCategorytDetail = function () {
		        productCategoriesSvc.getById(parseInt($stateParams.id)).then(function (response) {
		            $scope.category = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    loadCategorytDetail();

		    var loadParent = function () {
		        productCategoriesSvc.getAllCategories('').then(function (response) {
		            $scope.parents = response.data;
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    loadParent();

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.category.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }

		    $scope.update = function () {
		        $scope.submitted = true;
		        if ($scope.category.Name === undefined || $scope.category.Name === '' || $scope.category.Name === null || $scope.category.Name.length > 256) {
		            return;
		        }

		        if ($scope.category.Alias === undefined || $scope.category.Alias === '' || $scope.category.Alias === null || $scope.category.Alias.length > 256) {
		            return;
		        }
                
		        productCategoriesSvc.update($scope.category).then(function (record) {
		            alert('Thêm mới danh mục sản phẩm thành công!');
		            $state.go('productCategories', { reload: true });
		        }, function (error) {
		            console.log(error);
		        });
		    }
		}
	]);