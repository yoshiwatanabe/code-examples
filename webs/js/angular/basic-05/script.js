(function(angular) {
  'use strict';
angular.module('docsScopeProblemExample', [])
  .controller('NaomiController', ['$scope', function($scope) {
    $scope.customer = {
      name: 'Naomi',
      address: '1600 Amphitheatre'
    };
  }])
  .controller('IgorController', ['$scope', function($scope) {
    $scope.customer = {
      name: 'Igor',
      address: '123 Somewhere'
    };
  }])
  // this directive is part of docsScopeProblemExample module.
  // when used inside of a controller, this directive has access to
  // the controller's scope
  // it can refer to the scope as {{customer.name}} and {{customer.address}}
  .directive('myCustomer', function() {
    return {
      restrict: 'E',
      templateUrl: 'my-customer.html'
    };
  });
})(window.angular);

/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
