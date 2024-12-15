(function (app) {
    app.controller('invoiceController', invoiceController)
        .component('invoice', {
            templateUrl: '/app/admin/invoice/invoice.component.html',
            controller: 'invoiceController',
            controllerAs: 'vm'
        });

    invoiceController.$inject = ['serviceProxies', '$state', '$element', '$http', '$timeout'];

    function invoiceController(serviceProxies, $state, $element, $http, $timeout) {
        var vm = this;
        vm.invoices = [];
        vm.selectedId = null;

        vm.gridOptions = {
            columnDefs: [
                { headerName: "No", valueGetter: "node.rowIndex + 1" },
                { headerName: "Id", field: "Id" },
                { headerName: "CreationTime", field: "CreationTime" },
                { headerName: "CustomerName", field: "CustomerName" },
                { headerName: "CustomerTelephone", field: "CustomerTelephone" },
                { headerName: "PaymentStatusName", field: "PaymentStatusName" },
                { headerName: "PaymentMethodName", field: "PaymentMethodName" },
                { headerName: "Total", field: "Total", valueGetter: `data.Total.toLocaleString() + ' VNĐ'` },
            ],
            rowData: [],
            pagination: true,
            paginationPageSize: 10,
            defaultColDef: {
                sortable: true,
                filter: true,
                resizable: true,
                floatingFilter: true,
                flex: 1,
                cellStyle: { 
                    display: 'flex', 
                    alignItems: 'center', 
                } 
            },
            rowSelection: 'single',
            enableColResize: true,
            rowHeight: 35,
            frameworkComponents: {
            },
            onGridReady: function (params) {
                vm.gridApi = params.api
                window.addEventListener('resize', () => params.api.sizeColumnsToFit())
            },
            onFirstDataRendered: function (params) {
                params.api.sizeColumnsToFit();
            },
            onRowClicked: function (event) {
                $timeout(() => vm.selectedId = event.data.Id)
            }
        };

        vm.getInvoices = () => 
            serviceProxies.invoiceService.getAll()
                .then((response) => {
                    vm.invoices = response
                    if (vm.gridApi) {
                        vm.gridApi.setRowData(vm.invoices);
                    }
                })
                .catch((error) => console.error('Error fetching invoices:', error))  

        vm.reload = () => vm.getInvoices()

        vm.exportExcel = () => {
            if(confirm('Are you sure you want to export?')) {
                fetch('/api/invoice/exportExcel')
                    .then(response => response.blob())
                    .then(blob => {
                        const url = window.URL.createObjectURL(new Blob([blob]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'invoices.xlsx');
                        document.body.appendChild(link);
                        link.click();
                        link.remove();
                    });
            }
        }

        vm.exportPdf = () => {
            if(confirm('Are you sure you want to export?')) {
                fetch(`/api/invoice/exportPDF/${vm.selectedId}`)
                    .then(response => response.blob())
                    .then(blob => {
                        const url = window.URL.createObjectURL(new Blob([blob]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'invoice.pdf');
                        document.body.appendChild(link);
                        link.click();
                        link.remove();
                    });
            }
        }

        vm.add = () => angular.element(document.querySelector('create-or-edit-invoice-modal'))
            .controller('createOrEditInvoiceModal').show();

        vm.edit = () => angular.element(document.querySelector('create-or-edit-invoice-modal'))
            .controller('createOrEditInvoiceModal').show(vm.selectedId);

        vm.delete = () => {
            if (confirm('Are you sure you want to delete?')) {
                serviceProxies.invoiceService.delete(vm.selectedId)
                    .then(() => vm.getInvoices())
                    .catch((error) => console.error('Error deleting invoice:', error))
            }
        };

        vm.getInvoices();
    }

})(angular.module('app.admin.invoice'));