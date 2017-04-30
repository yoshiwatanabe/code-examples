'use strict';

/* Controllers */

/* https://docs.angularjs.org/guide/module */
var storeApp = angular.module('storeApp', []);

/* https://docs.angularjs.org/api/ng/service/$http */
storeApp.controller('inventory', ['$scope', '$http', function($scope, $http) {
  $http.get('https://raw.githubusercontent.com/yoshiwatanabe/web-samples/master/js/angular/basic-1/fruits.json').success(function(data){
    $scope.fruits = data;
  });
}]);
