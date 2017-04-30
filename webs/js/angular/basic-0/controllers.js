'use strict';

/* Controllers */

var storeApp = angular.module('storeApp', []);

storeApp.controller('inventory', function($scope) {
  $scope.fruits = [{
    'name': 'apple'
  }, {
    'name': 'orange'
  }, {
    'name': 'banana'
  }];
});
