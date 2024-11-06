(function(app){
    app.controller('roleController', roleController)
        .component('role', {
            templateUrl: '/app/admin/authorization/role/role.component.html',
            controller: 'roleController',
            controllerAs: 'vm'
        });

        roleController.$inject = ['serviceProxies'];

        function roleController(serviceProxies){
            var vm = this;
            vm.roles = [];
            vm.selectedId = null;

            vm.columns = [
                { headerName: "Id", field: "Id" },
                { headerName: "Name", field: "Name" },
            ];

            vm.getRoles = function(){
                serviceProxies.roleService.getAll().then(function (response) {
                    vm.roles = response;
                }).catch(function(error){
                    console.error('Error fetching roles:', error);
                });
            };

            vm.reload = function(){
                vm.getRoles();
            };

            vm.add = function(){
                angular.element(document.querySelector('create-or-edit-role-modal')).controller('createOrEditRoleModal').show();
            };

            vm.edit = function(){
                angular.element(document.querySelector('create-or-edit-role-modal')).controller('createOrEditRoleModal').show(vm.selectedId);
            };

            vm.delete = function(id){
                if(confirm('Are you sure you want to delete?')){
                    serviceProxies.roleService.delete(id).then(function(){
                        vm.getRoles();
                    }).catch(function(error){
                        console.error('Error deleting role:', error);
                    });
                }
            };

            vm.handleRowClick = function(row){
                vm.selectedId = row.Id;
            };

            vm.getRoles();
        }

})(angular.module('app.admin.authorization.role'));