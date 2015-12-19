
var DirectoriesApp = angular.module("BrawsingDirectoriesApp");

DirectoriesApp.controller("DirectoriesCtrl", function ($scope, $http) {

    $http.get('/api/values').success(function (ld) {
        $scope.directories = { path: ld.DirectoriesPath, name: ld.DirectoriesName };
        $scope.filesCountLess10Mb = ld.FilesCountLess10Mb;
        $scope.filesCount10_50Mb = ld.FilesCount10_50Mb;
        $scope.filesCountMore50Mb = ld.FilesCountMore50Mb;
        $scope.currentPath = "hard discs";
    });
    
    $scope.directoryEntries = function (path) {
     
        $http.post('/api/values?path=' + path).success(function (de) {
            if (de != null) {
                $scope.directories = { path: de.DirectoriesPath, name: de.DirectoriesName };
                $scope.files = de.FilesName;
                $scope.parentDirectory = de.ParentDirectory;
                $scope.filesCountLess10Mb = de.FilesCountLess10Mb;
                $scope.filesCount10_50Mb = de.FilesCount10_50Mb;
                $scope.filesCountMore50Mb = de.FilesCountMore50Mb;
                $scope.showLogicalDrives = de.ShowLogicalDrives;
                $scope.showParentDirectory = de.ShowParentDirectory;
                $scope.currentPath = path;
                $scope.exception = false;
            }
            else
            {
                $scope.exception = true;
            }
        });
    }


    $scope.logicalDrives = function () {

        $http.get('/api/values').success(function (ld) {
            $scope.directories = { path: ld.DirectoriesPath, name: ld.DirectoriesName };;
            $scope.files = ld.FilesName;
            $scope.parentDirectory = ld.ParentDirectory;
            $scope.showLogicalDrives = ld.ShowLogicalDrives;
            $scope.showParentDirectory = ld.ShowParentDirectory;
            $scope.filesCountLess10Mb = ld.FilesCountLess10Mb;
            $scope.filesCount10_50Mb = ld.FilesCount10_50Mb;
            $scope.filesCountMore50Mb = ld.FilesCountMore50Mb;
            $scope.currentPath = "hard discs";
        });

    };
});