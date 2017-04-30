// define a namespace
(function(angular) {
  'use strict';

// create a module
angular.module('docsSimpleDirective', [])
  // also create a controller
  .controller('Controller', ['$scope', function($scope) {
    // add a property to the scope
    $scope.customer = {
      name: 'Naomi',
      address: '1600 Amphitheatre'
    };
  }])
  // also create a directive
  .directive('myCustomer', function() {
    // just create a directive that defines a template
    return {
      template: 'Name: {{customer.name}} Address: {{customer.address}}'
    };
  });
})(window.angular);

/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
