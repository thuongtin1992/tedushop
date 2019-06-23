(function (app) {
    app.controller('unauthorizeController', unauthorizeController);
    unauthorizeController.$inject = ['$scope'];

    function unauthorizeController($scope) {
        $scope.message = "Bạn không có quyền truy cập vào đây. Vui lòng liên hệ quản trị viên.";
    }
})(angular.module('tedushop'));