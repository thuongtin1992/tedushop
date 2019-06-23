(function (app) {
    'use strict';

    app.controller('applicationUserEditController', applicationUserEditController);

    applicationUserEditController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

    function applicationUserEditController($scope, apiService, notificationService, $location, $stateParams) {
        $scope.account = {}

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2030, 12, 31),
            minDate: new Date(1920, 1, 1),
            startingDay: 1,
            showWeeks: true
        };

        $scope.popup = {
            opened: false
        };

        $scope.open = function () {
            $scope.popup.opened = true;
        };

        $scope.format = 'dd/MM/yyyy';

        $scope.updateAccount = updateAccount;

        function updateAccount() {
            apiService.put('/api/applicationUser/update', $scope.account, addSuccessed, addFailed);
        }

        function loadDetail() {
            apiService.get('/api/applicationUser/detail/' + $stateParams.id, null,
            function (result) {
                $scope.account = result.data;
                $scope.account.BirthDay = new Date($scope.account.BirthDay);
            },
            function (result) {
                notificationService.displayError(result.data);
            });
        }

        function addSuccessed() {
            notificationService.displaySuccess('Tài khoản ' + $scope.account.FullName + ' đã được cập nhật thành công.');
            $location.url('application_users');
        }

        function addFailed(response) {
            notificationService.displayError(response.data.Message);
            notificationService.displayErrorValidation(response);
        }

        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notificationService.displayError('Không tải được danh sách nhóm.');
                });
        }

        loadGroups();
        loadDetail();
    }
})(angular.module('tedushop.application_users'));