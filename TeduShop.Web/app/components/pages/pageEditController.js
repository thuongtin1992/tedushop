﻿(function (app) {
    app.controller('pageEditController', pageEditController);

    pageEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function pageEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.page = {};

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '400px'
        }

        $scope.UpdatePage = UpdatePage;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }

        function loadPageDetail() {
            apiService.get('api/page/getbyid/' + $stateParams.id, null, function (result) {
                $scope.page = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePage() {
            apiService.put('api/page/update', $scope.page,
                function (result) {
                    notificationService.displaySuccess('Trang ' + result.data.Name + ' đã được cập nhật.');
                    $state.go('pages');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadPageDetail();
    }

})(angular.module('tedushop.pages'));