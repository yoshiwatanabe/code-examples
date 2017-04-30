var myModule = angular.module('myModule', []);
myModule.directive('firstDirective', function() {
  return {
    template: '<span>初めてのディレクティブ</span>'
  };
});
