(function(angular) {
  'use strict';
  angular.module('docsScopeProblemExample', [])
    // .controller('NaomiController', ['$scope', function($scope) {
    //   $scope.customer = {
    //     name: 'Naomi',
    //     address: '1600 Amphitheatre'
    //   };
    // }])
    // .controller('IgorController', ['$scope', function($scope) {
    //   $scope.customer = {
    //     name: 'Igor',
    //     address: '123 Somewhere'
    //   };
    // }])
    .controller('Controller', ['$scope', function($scope) {
      $scope.naomi = {
        name: 'Naomi',
        address: '1600 Amphitheatre'
      };
      $scope.igor = {
        name: 'Igor',
        address: '123 Somewhere'
      };
    }])

  .directive('myCustomer', function() {
    return {
      restrict: 'E',

      // NEW thing.
      // this allows me to refer to a state as {{customerInfo.name}}  inside of the template
      // below, we expect DOM markup to specify an HTML attribute called info, as in
      // <my-customer info="naomi"></my-customer>
      scope: {
        customerInfo: '=info'
      },

      templateUrl: 'my-customer.html'
    };
  });
})(window.angular);

/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
