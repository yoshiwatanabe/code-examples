(function(angular) {
  'use strict';
angular.module('docsTransclusionExample', [])
  .controller('Controller', ['$scope', function($scope) {
    $scope.name = 'Tobiasssssss';
  }])
  .directive('myDialog', function() {
    return {
      restrict: 'E',
      transclude: true,
      //scope: {},  <<<<< this is significant! by commenting out, we are saying "We do NOT need an isolate scope for my directive, and we are going to use the scope outside(which is that of Controller.)"
      //            <<<<< this mean...
      templateUrl: 'my-dialog.html',
      link: function (scope) {
        console.log('used to be ' + scope.name);
        scope.name = 'Jeff'; // <<<<< this line will modify the value of the outside scope, which was originally set to 'Tobiasssssss'
        console.log('now, set to be ' + scope.name);
      }
    };
  });
})(window.angular);

/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/
