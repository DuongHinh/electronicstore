angular.module('electronicStoreApp.global.directives', ['electronicStoreApp.global.common'])
 .directive('pagerDirective', function () {
     return {
         scope: {
             page: '@',
             pagesCount: '@',
             totalCount: '@',
             searchFunc: '&',
             customPath: '@'
         },
         replace: true,
         restrict: 'E',
         templateUrl: '/app/global/template/pagerDirective.html',
         controller: [
             '$scope', function ($scope) {
                 $scope.search = function (i) {
                     if ($scope.searchFunc) {
                         $scope.searchFunc({ page: i });
                     }
                 };

                 $scope.range = function () {
                     if (!$scope.pagesCount) { return []; }
                     var step = 2;
                     var doubleStep = step * 2;
                     var start = Math.max(0, $scope.page - step);
                     var end = start + 1 + doubleStep;
                     if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                     var ret = [];
                     for (var i = start; i != end; ++i) {
                         ret.push(i);
                     }

                     return ret;
                 };

                 $scope.pagePlus = function (count) {
                     return +$scope.page + count;
                 }

             }]
     }
 })
.directive('isDecimal',
        function () {
            return {
                require: '?ngModel',
                link: function (scope, element, attrs, ngModelCtrl) {
                    if (!ngModelCtrl) {
                        return;
                    }

                    ngModelCtrl.$parsers.push(function (val) {
                        if (angular.isUndefined(val)) {
                            var val = '';
                        }
                        var clean = val.replace(/[^0-9\.]/g, '');
                        var decimalCheck = clean.split('.');

                        if (!angular.isUndefined(decimalCheck[1])) {
                            decimalCheck[1] = decimalCheck[1].slice(0, 2);
                            clean = decimalCheck[0] + '.' + decimalCheck[1];
                        }

                        if (val !== clean) {
                            ngModelCtrl.$setViewValue(clean);
                            ngModelCtrl.$render();
                        }
                        return clean;
                    });

                    element.bind('keypress',
                        function (event) {
                            if (event.keyCode === 32) {
                                event.preventDefault();
                            }
                        });
                }
            };
        })
.directive('isInteger',
        function () {
            return {
                require: '?ngModel',
                link: function (scope, element, attrs, ngModelCtrl) {
                    if (!ngModelCtrl) {
                        return;
                    }

                    ngModelCtrl.$parsers.push(function (val) {
                        var clean = val.replace(/[^0-9]+/g, '');
                        if (val !== clean) {
                            ngModelCtrl.$setViewValue(clean);
                            ngModelCtrl.$render();
                        }
                        return clean;
                    });

                    element.bind('keypress',
                        function (event) {
                            if (event.keyCode === 32) {
                                event.preventDefault();
                            }
                        });
                }
            };
        });
