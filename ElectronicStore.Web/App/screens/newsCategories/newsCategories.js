angular.module('electronicStoreApp.screens.newsCategories',
	[
		'ui.router',
        'electronicStoreApp.global.services',
        'electronicStoreApp.global.services.products',
        'electronicStoreApp.global.services.productCategories',
        'electronicStoreApp.services.brands',
        'electronicStoreApp.services.newsCategories',
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
			.state('newsCategories', {
			    url: "/newsCategories",
			    parent: 'layout',
			    templateUrl: "/app/screens/newsCategories/list.html"
			});

	    $stateProvider
			.state('newsCategories.new', {
			    url: "/newsCategories/new",
			    parent: 'layout',
			    templateUrl: "app/screens/newsCategories/new.html"
			});

	    $stateProvider
			.state('newsCategories.edit', {
			    url: "/newsCategories/edit/{id}",
			    parent: 'layout',
			    templateUrl: "app/screens/newsCategories/edit.html"
			});

	})
	.controller('newsCategoryListController',
	[
 		'$scope', '$state', '$stateParams', '$rootScope', 'productSvc', '$ngBootbox', '$filter', 'newsCategoriesSvc',
        function ($scope, $state, $stateParams, $rootScope, productSvc, $ngBootbox, $filter, newsCategoriesSvc) {

            $scope.title = 'Danh sách loại tin tức';
            $scope.loading = true;
            $scope.keyword = '';
            $scope.itemsPerPage = 10;


            var getListNewsCategories = function () {
                newsCategoriesSvc.getListNewsCategory($scope.keyword).then(function (response) {
                    $scope.newsCategories = response.data;

                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                    console.log(error);
                });
            }

            getListNewsCategories();

            $scope.search = function () {
                getListNewsCategories();
            }

            $scope.deleteNewsCategories = function (item) {
                $ngBootbox.confirm("Bạn có chắc muốn xóa " + item.Name + " ?").then(function () {
                    var id = item.Id;
                    newsCategoriesSvc.deleteNewsCategory(id).then(function (response) {
                        $state.reload();
                    }, function (error) {
                        console.log(error);
                    });
                });
            }

            $scope.print = function () {
                window.print();
            }
        }
	])
.controller('newsCategoriesAddController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productCategoriesSvc', 'commonSvc', 'newsCategoriesSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productCategoriesSvc, commonSvc, newsCategoriesSvc) {
		    $scope.title = 'Thêm mới danh mục tin tức';
		    $scope.submitted = false;
		    $scope.category = {
		        Status: true,
		        Name: '',
		        Alias: ''
		    }

		    $scope.chooseImage = function () {
		        var finder = new CKFinder();
		        finder.selectActionFunction = function (fileUrl) {
		            $scope.$apply(function () {
		                $scope.category.Image = fileUrl;
		            })
		        }
		        finder.popup();
		    }


		    $scope.addNew = function () {
		        $scope.submitted = true;
		        if ($scope.category.Name === '' || $scope.category.Name === null || $scope.category.Name.length > 256) {
		            return;
		        }

		        if ($scope.category.Alias === '' || $scope.category.Alias === null || $scope.category.Alias.length > 256) {
		            return;
		        }

		        if ($scope.category.Description != null && $scope.category.Description.length > 500) {
		            return;
		        }

		        newsCategoriesSvc.addNewsCategory($scope.category).then(function (record) {
		            alert('Thêm mới danh mục tin tức thành công!');
		            $state.go('newsCategories', {}, { reload: true });
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.category.Alias = commonSvc.getAlias(input);
		    }

		}
	])


.controller('newsCategoriesEditController',
	[
		'$scope', '$state', '$log', '$stateParams', '$rootScope', 'productCategoriesSvc', 'commonSvc', 'newsCategoriesSvc',
		function ($scope, $state, $log, $stateParams, $rootScope, productCategoriesSvc, commonSvc, newsCategoriesSvc) {
		    $scope.title = 'Cập nhật danh mục tin tức';
		    $scope.submitted = false;
		    $scope.category = {
		        Status: true,
		        Name: '',
		        Alias: ''
		    }

		    var loadDetail = function () {
		        newsCategoriesSvc.getNewsCategoryById($stateParams.id).then(function (response) {
		            $scope.category = response.data;
		        }, function (err) {
		            console.log(err);
		        });
		    }

		    loadDetail();

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
		        if ($scope.category.Name === '' || $scope.category.Name === null || $scope.category.Name.length > 256) {
		            return;
		        }

		        if ($scope.category.Alias === '' || $scope.category.Alias === null || $scope.category.Alias.length > 256) {
		            return;
		        }

		        if ($scope.category.Description != null && $scope.category.Description.length > 500) {
		            return;
		        }

		        newsCategoriesSvc.updateNewsCategory($scope.category).then(function (record) {
		            alert('Cập nhật danh mục tin tức thành công!');
		            $state.go('newsCategories', {}, { reload: true });
		        }, function (error) {
		            console.log(error);
		        });
		    }

		    $scope.getAlias = function (input) {
		        $scope.category.Alias = commonSvc.getAlias(input);
		    }

		}
	])