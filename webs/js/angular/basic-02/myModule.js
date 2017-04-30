var myModule = angular.module('myModule', []);
myModule.directive('myDirective', function() {

    return {
      transclude: true,
      template: '<div > <div  style="color:red" ng-transclude></div> </div>'
    };
});
