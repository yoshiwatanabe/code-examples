(function(angular) {
  'use strict';
angular.module('docsTransclusionDirective', [])
  .controller('Controller', ['$scope', function($scope) {
    $scope.name = 'Tobias';
  }])
  .directive('myDialog', function() {
    return {
      restrict: 'E',
      transclude: true,
      scope: {},
      templateUrl: 'my-dialog.html'
    };
  });
})(window.angular);

/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
