var app = angular.module('app', []);

app.controller('sportListController', [
    '$scope', '$http', function($scope, $http) {

        $scope.newSport = 'Fotbal';
    $scope.sportList = [];

    var success = function(sport) {
        alert(sport.Id);
        loadSports();
    };

    var error = function(error) {
        alert(error.Message);
    };

    $scope.createSport = function() {
        $http.post('/odata/Sports', { Caption: $scope.newSport }).success(success).error(error);
    };

    var sportsLoaded = function(data) {
        $scope.sportList = data.value;
    };
    var loadSports = function() {
        $http.get('/odata/Sports').success(sportsLoaded);
    }
    loadSports();
}]);