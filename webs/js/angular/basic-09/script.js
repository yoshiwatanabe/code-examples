(function(angular) {
  'use strict';
angular.module('docsIsoFnBindExample', [])
  .controller('Controller', ['$scope', '$timeout', function($scope, $timeout) {

    // There will be {{message}} and {{name}} available.
    $scope.name = 'Tobias';
    $scope.message = '';

    // Use example: see the on-close  <my-dialog ng-hide="dialogIsHidden" on-close="hideDialog(message)">
    $scope.hideDialog = function (message) {
      $scope.message = message; // this cauess {{message}} to show the value passed as a method param
      $scope.dialogIsHidden = true; // this boolean variable is used with ngHide as in:  <my-dialog ng-hide="dialogIsHidden" on-close="hideDialog(message)">

      // just scheduling a timer event to re-show the now hidden dialog.
      $timeout(function () {
        $scope.message = '';
        $scope.dialogIsHidden = false;
      }, 2000);
    };
  }])
  .directive('myDialog', function() {
    return {
      restrict: 'E',
      transclude: true,
      // Directive's own scope is dfined here.
      // 'close' is the directive's scope's property
      //
      scope: {
        // this is creating a function 'close'
        // & cause "evaluation" of an expression
        // the expression in this case is a function call at the original context (index.html)
        'close': '&onClose'
        // what does this allow?
        // directive exposes a way to listens to an event that occurred in the directive (e.g. click of an element in the directive)
        // the html that uses the instance of this directive and specify on-close="whatever" to handle an event that occurred in a directive.
        // Example: we write a directive that wraps filter control. the filter control can have "apply" button. that button can have ng-click to
        // call a directive's isolated scope's property (similar to 'close' above), passing any data needed by the handler (e.g. filter criteria)
        // now the "whatever" part (in our case, a Controller's function), can take the filter criteria and do the apply/reload etc.
      },
      templateUrl: 'my-dialog-close.html'
    };
  });
})(window.angular);

/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
