
var app = angular.module('mainApp', []);

app.controller('GuidGeneratorController', function ($scope, $http) {
    $scope.GUID = "";

    $scope.addNewGuid = function () {
        $scope.GUID = guid();
    }
});

app.controller('DateCalculatorController', function ($scope, $http) {
    $scope.date = new Date();
    $scope.year = 0;
    $scope.month = 0;
    $scope.day = 0;
    $scope.result = new Date();
    $scope.calculate = function () {
        var years = $scope.date.getFullYear() + $scope.year;
        var months = $scope.date.getMonth() + $scope.month;
        var dates = $scope.date.getDate() + $scope.day;
        $scope.result.setFullYear(years);
        $scope.result.setMonth(months);
        $scope.result.setDate(dates);
    };
});

app.filter('isleapyear', function () {
    return function (input) {
        if (input) {
            return input.getFullYear() % 4 == 0 ? "Leap Year" : "Not A Leap Year";
        }
    }
});

app.filter('guidlower', function () {
    return function (input) {
        if (input) {
            return input.toLowerCase();
        }
    }
});

app.filter('guidupper', function () {
    return function (input) {
        if (input) {
            return input.toUpperCase();
        }
    }
});

app.filter('jguidlower', function () {
    return function (input) {
        if (input) {
            return input.replace(/-/g, "").toLowerCase();
        }
    }
});

app.filter('jguidupper', function () {
    return function (input) {
        if (input) {
            return input.replace(/-/g, "").toUpperCase();
        }
    }
});

function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
}