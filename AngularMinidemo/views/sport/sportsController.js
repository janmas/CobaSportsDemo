var app = angular.module('app', []);

app.controller('sportListController', [
    '$scope', '$http', function($scope, $http) {

        $scope.newSport = 'Fotbal';
        $scope.sportList = [];        

    var success = function (sport) {        
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
    
    var patchOk = function () {
        alert('Product successfully updated.')
    };

    var changeActionOk = function (data) {
        alert('Response: ' + data.value);
    }

    
    $scope.onBlur = function(sport) {
        console.log(sport);        
        // $http({ method: 'PATCH', url: '/odata/Sports/(' + sport.Id + ')', data: angular.toJson({ Caption: sport.Caption }) }).error(error).success(patchOk);

        // use Sport.ChangeCaption action...
        $http({ method: 'POST', url: '/odata/Sports/(' + sport.Id + ')/ChangeCaption', data: angular.toJson({ Caption: sport.Caption }) }).error(error).success(changeActionOk);
    }

    var deleteOk = function () {
        alert('Product successfully deleted.')
        loadSports();
    };

    $scope.deleteSport = function(sport) {
        $http({ method: 'DELETE', url: '/odata/Sports/(' + sport.Id + ')'}).error(error).success(deleteOk);
    }

    $scope.sportQuery = '';
    $scope.getSports = function () {
        var queryPart = $scope.sportQuery ? '?' + $scope.sportQuery : ''
        $http.get('/odata/Sports/' + queryPart).success(sportsLoaded).error(error);
    }

}]);