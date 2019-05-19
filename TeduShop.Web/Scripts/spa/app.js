/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module('myModule', []);

//myApp.controller("myController", function ($scope) {
//    $scope.message = "This is my message from Controller!";
//}); viết gộp

// viết tường minh
myApp.controller("schoolController", schoolController);
myApp.directive("teduShopDirective", teduShopDirective);

myApp.service('validatorService', validatorService);
schoolController.$inject = ['$scope', 'validatorService'];

//declare
//rootScope
//function studentController($rootScope, $scope) {
//    $rootScope.message = "This is a student name A!";
//}

function schoolController($scope, validatorService) {
    //Validator.checkNumber(1);
    $scope.checkNumber = function () {
        $scope.message = validatorService.checkNumber($scope.numb);
    }
    $scope.numb = 1;
}

function validatorService($window) {
    return {
        checkNumber : checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0) {
            //$window.alert('This is even.');
            return 'This is even';
        }
        else
            //$window.alert('This is odd.');
            return 'This is odd';
    }
}

function teduShopDirective() {
    return {
        restrict: "A",
        templateUrl: "/Scripts/spa/teduShopDirective.html"
    }
}