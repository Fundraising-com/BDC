(function () {
   "use strict";
   var module = angular.module("lisa.reports");

   function ReportsController($log, $scope, ReportsFactory, $localStorage, $q, AddressFactory, SalesFactory, LeadFactory, ProductFactory, ToastFactory) {
      var vm = this;
      vm.scope = $scope;
      vm.scope.$storage = $localStorage;
      vm.executing = false;
      vm.filter = "";
      vm.reports = [];
      vm.countries = AddressFactory.Country.query({}, { isArray: true, cache: true });
      vm.regions = [];
      vm.organizationTypes = LeadFactory.OrganizationType.query({}, { isArray: true });
      vm.groupTypes = LeadFactory.GroupType.query({}, { isArray: true });
      vm.productClasses = ProductFactory.ProductClass.query({}, { isArray: true });
      vm.parameters = {};
      vm.results = undefined;
      vm.repeatedBusinessConsultants = undefined;
      vm.reportBarChartLabels = [];
      vm.reportBarChartSeries = [];
      vm.reportBarChartData = [[], [], [], []];
      vm.CsvData = undefined;
      vm.Totals = {};
      vm.reportBarChartColors = ['#83ACC2', '#C0C0C0', '#335C72', '#606060'];
      vm.reportBarChartDatasetOverride = [
         {
            borderWidth: 2,
            type: 'bar'
         },
         {
            borderWidth: 2,
            type: 'bar'
         },
         {
            borderWidth: 3,
            type: 'line',
            backgroundColor: "rgba(255, 255, 255, 0.0)",
            borderColor: "rgba(83, 124, 146, 1)",
         },
         {
            borderWidth: 3,
            type: 'line',
            backgroundColor: "rgba(255, 255, 255, 0.0)",
            borderColor: "rgba(128, 128, 128, 1)",
         }
      ];
      vm.reportBarChartOptions = {
         legend: {
            display: true
         }
      };
      vm.GetRegions = function () {
         if (vm.parameters.Country !== undefined && vm.parameters.Country !== null) {
            vm.regions = AddressFactory.Region.query({ countryCode: vm.parameters.Country.Code },
               { isArray: true, cache: true });
         } else {
            vm.parameters.Region = undefined;
         }
      };
      vm.GetCountryMatches = function (query) {
         var found = [];
         for (var i = 0; i < vm.countries.length; i++) {
            if (vm.countries[i].Name.toLowerCase().indexOf(query.toLowerCase()) > -1 ||
               vm.countries[i].Code.toLowerCase().indexOf(query.toLowerCase()) > -1) {
               found.push(vm.countries[i]);
            }
         }
         return found;
      };
      vm.GetRegionMatches = function (query) {
         var found = [];
         for (var i = 0; i < vm.regions.length; i++) {
            if (vm.regions[i].Name.toLowerCase().indexOf(query.toLowerCase()) > -1 ||
               vm.regions[i].Code.toLowerCase().indexOf(query.toLowerCase()) > -1) {
               found.push(vm.regions[i]);
            }
         }
         return found;
      };
      vm.GetConsultantMatches = function (query) {
         return SalesFactory.Consultant.query({ name: query }, { isArray: true });
      };
      vm.GetProductClassesMatches = function (query) {
         var found = [];
         for (var i = 0; i < vm.productClasses.length; i++) {
            if (vm.productClasses[i].Name.toLowerCase().indexOf(query.toLowerCase()) > -1) {
               found.push(vm.productClasses[i]);
            }
         }
         return found;
      };
      vm.GetOrganizationTypesMatches = function (query) {
         var found = [];
         for (var i = 0; i < vm.organizationTypes.length; i++) {
            if (vm.organizationTypes[i].Name.toLowerCase().indexOf(query.toLowerCase()) > -1) {
               found.push(vm.organizationTypes[i]);
            }
         }
         return found;
      };
      vm.GetGroupTypesMatches = function (query) {
         var found = [];
         for (var i = 0; i < vm.groupTypes.length; i++) {
            if (vm.groupTypes[i].Name.toLowerCase().indexOf(query.toLowerCase()) > -1) {
               found.push(vm.groupTypes[i]);
            }
         }

         return found;
      };
      vm.GetReports = function () {
         vm.executing = true;
         //For now, this is hardcoded but one day we'll move it to DB
         vm.reports = [{ Id: 1, Name: 'Spider', Url: '/Reports/Report/Spider', Icon: 'bug_report' },
         { Id: 2, Name: 'Repeated Business', Url: '/Reports/Report/RepeatedBusiness', Icon: 'compare_arrows' },
             { Id: 3, Name: 'Gross Profit', Url: '/Reports/Report/GrossProfit', Icon: 'trending_up' },
             { Id: 4, Name: 'Sales To Process', Url: '/Reports/Report/SalesToProcess', Icon: 'trending_up' },
             { Id: 5, Name: 'Sales Transformer', Url: '/Reports/Report/SalesToProcessNEW', Icon: 'trending_up' },
             { Id: 6, Name: 'Products List', Url: '/Reports/Report/ProductList', Icon: '' },
             { Id: 7, Name: 'Customer List', Url: '/Reports/Report/CustomerList', Icon: '' },
             { Id: 8, Name: 'Traditional Confirmed Sales By Product Class', Url: '/Reports/Report/TraditionalConfirmedSalesByProductClass', Icon: 'info' }];
         vm.executing = false;
       };


       vm.GetTraditionalConfirmedSalesByProductClassReportHeader = function () {
           return [
               "Year", "Month", "Sales Id", "Consultant", "Partner_name", "Product Class", "Currency", "$Total Confirmed"];
       };


       vm.ExecuteReportTraditionalConfirmedSalesByProductClass = function () {
           vm.executing = true;
           var startedReportExecution = new Date();
           ReportsFactory.TraditionalConfirmedSalesByProductClass(vm.parameters)
               .then(function (result) {
                   var endedReportExecution = new Date();
                   vm.executedOn = new Date();
                   vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) / 1000);
                   vm.fileName = "TraditionalConfirmedSalesByProductClass (" + startedReportExecution.toLocaleString() + ").csv";
                   vm.results = result.data;
               }, function (error) {
                   $log.error(error);
                   ToastFactory.Error(error);
               }).finally(function () {
                   vm.executing = false;
               });
       };




       vm.ExecuteReportCustomerList = function () {
           vm.executing = true;
           var startedReportExecution = new Date();
           ReportsFactory.CustomerList(vm.parameters)
               .then(function (result) {
                   var endedReportExecution = new Date();
                   vm.executedOn = new Date();
                   vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) / 1000);
                   vm.fileName = "CustomerList (" + startedReportExecution.toLocaleString() + ").csv";
                   vm.results = result.data;
               }, function (error) {
                   $log.error(error);
                   ToastFactory.Error(error);
               }).finally(function () {
                   vm.executing = false;
               });
       };

       vm.GetCustomerListReportHeader = function () {
           return [
               "Name", "Customer Type","Company", "Email", "Phone", "Mobile", "Fax", "Website", "Street",
               "City", "State", "ZIP", "Country", "Opening Balance", "Date", "Resale Number"];};


       vm.ExecuteReportProductList = function () {
           vm.executing = true;
           var startedReportExecution = new Date();
           ReportsFactory.ProductList(vm.parameters)
               .then(function (result) {
                   var endedReportExecution = new Date();
                   vm.executedOn = new Date();
                   vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) / 1000);
                   vm.fileName = "ProductList (" + startedReportExecution.toLocaleString() + ").csv";
                   vm.results = result.data;
               }, function (error) {
                   $log.error(error);
                   ToastFactory.Error(error);
               }).finally(function () {
                   vm.executing = false;
               });
       };

       vm.GetProductListReportHeader = function () {
           return [
               "SCID", "Product / Service Name", "Sales Description", "SKU", "Type", "Sales Price / Rate", "Taxable", "Income Account",
               "Purchase Description", "Purchase Cost", "Expense Account", "Quantity on Hand", "Reorder Point", "Inventory Asset Account", "Quantity as-of Date"];
       };


       vm.ExecuteReportSalesToProcess = function () {
           vm.executing = true;
           var startedReportExecution = new Date();
           ReportsFactory.SalesToProcess(vm.parameters)
               .then(function (result) {
                   var endedReportExecution = new Date();
                   vm.executedOn = new Date();
                   vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) / 1000);
                   vm.fileName = "SalesToProcess (" + startedReportExecution.toLocaleString() + ").csv";
                   vm.results = result.data;
               }, function (error) {
                   $log.error(error);
                   ToastFactory.Error(error);
               }).finally(function () {
                   vm.executing = false;
               });
       };

       vm.GetSalesToProcessReportHeader = function () {
           return [
               "InvoiceNo", "Customer", "InvoiceDate", "DueDate", "Terms", "Location", "Memo",
               "Item(Product/Service)", "ItemDescription", "ItemQuantity", "ItemRate", "ItemAmount","ItemTaxCode", "TaxRate", "Currency"];
       };


       vm.ExecuteReportSalesToProcessNEW = function () {
           vm.executing = true;
           var startedReportExecution = new Date();
           ReportsFactory.SalesToProcessNEW(vm.parameters)
               .then(function (result) {
                   var endedReportExecution = new Date();
                   vm.executedOn = new Date();
                   vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) / 1000);
                   vm.fileName = "SalesTransformer (" + startedReportExecution.toLocaleString() + ").csv";
                   vm.results = result.data;
               }, function (error) {
                   $log.error(error);
                   ToastFactory.Error(error);
               }).finally(function () {
                   vm.executing = false;
               });
       };




       vm.GetSalesToProcessReportHeaderNEW = function () {
           return [
               "Billing First Name",
               "Billing Last Name",
               "Billing Organization",
               "Billing Phone",
               "Billing Mobile",
               "Billing Email",
               "Billing Street",
               "Billing City",
               "Billing State",
               "Billing Country",
               "Billing Zip Code",
               "Shipping First Name",
               "Shipping Last Name",
               "Shipping Attention of",
               "Shipping Organization",
               "Shipping Location",
               "Shipping Street",
               "Shipping City",
               "Shipping State",
               "Shipping Country",
               "Shipping Zip Code",
               "Sale ID",
               "Invoice date",
               "Terms",
               "Due Date",
               "Division",
               "Consultant",
               "Product Description",
               "Item Description",
               "Item Quantity",
               "Item Selling Rate",
               "Item Amount",
               "Shipping Amount",
               "Sale Total Amount",
               "Price Adjustment",
               "Billing ItemTax Code",
               "Billing Tax Rate"];
       };




      vm.ExecuteReportSpider = function () {
         vm.executing = true;
         var startedReportExecution = new Date();
         ReportsFactory.Spider(vm.parameters)
            .then(function (result) {
               var endedReportExecution = new Date();
               vm.executedOn = new Date();
               vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) / 1000);
               vm.fileName = "Spider (" + startedReportExecution.toLocaleString() + ").csv";
               vm.results = result.data;
            }, function (error) {
               $log.error(error);
               ToastFactory.Error(error);
            }).finally(function () {
               vm.executing = false;
            });
      };
      vm.GetSpiderReportHeader = function () {
         return [
            "Consultant", "First Name", "Last Name", "Day Phone", "Evening Phone", "Fax", "Organization", "Street",
            "ZIP Code", "State", "Country", "City", "Email", "Member Count", "Participant Count", "Fundraiser Start Date",
            "Promotion Id", "Organization Type Description", "Sale Total Amount", "Sale Id", "Lead Id",
            "Sale Confirmed Date", "Sale Actual Ship Date", "Lead Entry Date", "Product Description",
            "Promotion Type Code", "Promotion Description", "Partner Id",
            "Partner Name", "Partner Email", "Partner Phone Number", "Channel Code", "Group Description", "Total Adjustments"
         ];
      };
      vm.GetRepeatedBusinessReportHeader = function () {
         return [
            "LeadIdT1", "Country1", "State1", "City1", "ConsultantIdT1", "ConsultantNameT1", "SaleID1", "ActualShipDate1", "SaleItemNumber1", "Quantity1", "Product1", "Amount1", "LeadIdT2", "SaleID2", "ActualShipDate2", "SaleItemNumber2", "Quantity2", "Product2", "Amount2"
         ];
      };
      vm.GetGrossProfitReportHeader = function () {
         return [
            "T", "Name", "Product Class", "Sales", "Amount Sold", "GP%", "GP Amount"
         ];
      };
      vm.ExecuteReportRepeatedBusiness = function () {
         var promises = [];
         vm.executing = true;
         var startedReportExecution = new Date();
         vm.repeatedBusinessReportExecutedOn = new Date();
         vm.fileName = "RepeatedBusiness (" + startedReportExecution.toLocaleString() + ").csv";
         var a = new Date(vm.parameters.Date1Start).getFullYear() + "-" + (new Date(vm.parameters.Date1Start).getMonth() + 1) + "-" + new Date(vm.parameters.Date1Start).getDate();
         var b = new Date(vm.parameters.Date1End).getFullYear() + "-" + (new Date(vm.parameters.Date1End).getMonth() + 1) + "-" + new Date(vm.parameters.Date1End).getDate();
         var c = new Date(vm.parameters.Date2Start).getFullYear() + "-" + (new Date(vm.parameters.Date2Start).getMonth() + 1) + "-" + new Date(vm.parameters.Date2Start).getDate();
         var d = new Date(vm.parameters.Date2End).getFullYear() + "-" + (new Date(vm.parameters.Date2End).getMonth() + 1) + "-" + new Date(vm.parameters.Date2End).getDate();
         promises.push(ReportsFactory.RepeatedBusiness.query({
            date1Start: a,
            date1End: b,
            date2Start: c,
            date2End: d,
            showFCs: vm.parameters.ShowFCs,
            countryCode: vm.parameters.Country !== null ? vm.parameters.Country.Code : "",
            regionCode: vm.parameters.Region !== null ? vm.parameters.Region.Code : ""
         },
            { isArray: true, cache: false })
            .$promise);
         promises.push(ReportsFactory.RepeatedBusiness.query({
            date1Start: a,
            date1End: b,
            date2Start: c,
            date2End: d,
            showFCs: vm.parameters.ShowFCs,
            isDetail: true,
            countryCode: vm.parameters.Country !== null ? vm.parameters.Country.Code : "",
            regionCode: vm.parameters.Region !== null ? vm.parameters.Region.Code : ""
         },
            { isArray: true, cache: false })
            .$promise);
         $q.all(promises).then(
            function (results) {
               vm.InitReportChartData();
               vm.results = results[0];
               vm.CsvData = results[1];
               var endedReportExecution = new Date();
               vm.executedOn = new Date();
               vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) /
                  1000);

               vm.repeatedBusinessConsultants = {};
               var round = function (value, decimals) {
                  return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
               }
               angular.forEach(vm.results,
                  function (value, key) {
                     var rowValues = Object.values(value);
                     if (vm.repeatedBusinessConsultants[rowValues[1]] !== undefined) {
                        vm.repeatedBusinessConsultants[rowValues[1]].leadsT1 += 1;
                        vm.repeatedBusinessConsultants[rowValues[1]].leadsT2 += rowValues[4] !== null ? 1 : 0;
                        vm.repeatedBusinessConsultants[rowValues[1]].totalAmountT1 += rowValues[3];
                        vm.repeatedBusinessConsultants[rowValues[1]]
                           .totalAmountT2 += rowValues[5] !== null ? rowValues[5] : 0;
                     } else {
                        vm.repeatedBusinessConsultants[rowValues[1]] =
                           {
                              name: rowValues[2],
                              leadsT1: 1,
                              leadsT2: rowValues[4] !== null ? 1 : 0,
                              leadsDifference: "",
                              totalAmountT1: rowValues[3],
                              totalAmountT2: rowValues[5] !== null ? rowValues[5] : 0,
                              amountDifference: ""
                           };
                     }
                  });
               vm.Totals.LeadsT1 = 0;
               vm.Totals.LeadsT2 = 0;
               vm.Totals.AmountT1 = 0;
               vm.Totals.AmountT2 = 0;
               for (var key in vm.repeatedBusinessConsultants) {
                  var consultant = vm.repeatedBusinessConsultants[key];
                  consultant.leadsDifference = consultant.leadsT1 > 0 && consultant.leadsT2 > 0 ? consultant.leadsT2 / consultant.leadsT1 : 0;
                  consultant.amountDifference = consultant.totalAmountT1 > 0 && consultant.totalAmountT2 > 0 ? consultant.totalAmountT2 / consultant.totalAmountT1 : 0;

                  vm.Totals.LeadsT1 += consultant.leadsT1;
                  vm.Totals.LeadsT2 += consultant.leadsT2;
                  vm.Totals.AmountT1 += consultant.totalAmountT1;
                  vm.Totals.AmountT2 += consultant.totalAmountT2;

                  vm.reportBarChartLabels.push(consultant.name);
                  var leadsDifference = round(consultant.leadsDifference * 100, 2);
                  vm.reportBarChartData[0].push(leadsDifference);
                  var amountDifference = round(consultant.amountDifference * 100, 2);
                  vm.reportBarChartData[1].push(amountDifference);
               }
               vm.Totals.LeadsDifference = vm.Totals.LeadsT1 > 0 && vm.Totals.LeadsT2 > 0 ? vm.Totals.LeadsT2 / vm.Totals.LeadsT1 : 0;
               vm.Totals.AmountDifference = vm.Totals.AmountT1 > 0 && vm.Totals.AmountT2 > 0 ? vm.Totals.AmountT2 / vm.Totals.AmountT1 : 0;
               for (var i = 0; i < vm.reportBarChartData[0].length; i++) {
                  var totalleadsDifference = round(vm.Totals.LeadsDifference * 100, 2);
                  vm.reportBarChartData[2].push(totalleadsDifference);
                  var totalAmountDifference = round(vm.Totals.AmountDifference * 100, 2);
                  vm.reportBarChartData[3].push(totalAmountDifference);
               }
               vm.reportBarChartSeries.push('Repeated Leads');
               vm.reportBarChartSeries.push('Repeated Amount Sold');
               vm.reportBarChartSeries.push('Average Leads');
               vm.reportBarChartSeries.push('Average Amount Sold');
               vm.executing = false;
            },
            function (errors) {
               $log.error(errors);
            });
      };
      vm.ExecuteGrossProfit = function () {
         var promises = [];
         vm.executing = true;
         var startedReportExecution = new Date();
         vm.grossProfitReportExecutedOn = new Date();
         vm.fileName = "GrossProfit (" + startedReportExecution.toLocaleString() + ").csv";
         var a = new Date(vm.parameters.Date1Start).getFullYear() + "-" + (new Date(vm.parameters.Date1Start).getMonth() + 1) + "-" + new Date(vm.parameters.Date1Start).getDate();
         var b = new Date(vm.parameters.Date1End).getFullYear() + "-" + (new Date(vm.parameters.Date1End).getMonth() + 1) + "-" + new Date(vm.parameters.Date1End).getDate();
         var c = new Date(vm.parameters.Date2Start).getFullYear() + "-" + (new Date(vm.parameters.Date2Start).getMonth() + 1) + "-" + new Date(vm.parameters.Date2Start).getDate();
         var d = new Date(vm.parameters.Date2End).getFullYear() + "-" + (new Date(vm.parameters.Date2End).getMonth() + 1) + "-" + new Date(vm.parameters.Date2End).getDate();
         promises.push(ReportsFactory.GrossProfit.query({
            date1Start: a,
            date1End: b,
            date2Start: c,
            date2End: d            
         },
            { isArray: true, cache: false })
            .$promise);         
         $q.all(promises).then(
            function (results) {
               vm.CsvData = results[0];
               var endedReportExecution = new Date();
               vm.executedOn = new Date();
               vm.executionTime = Math.abs((endedReportExecution.getTime() - startedReportExecution.getTime()) /
                  1000);               
               vm.executing = false;
            },
            function (errors) {
               $log.error(errors);
            });
      };
      vm.ResetReportParameters = function () {
         vm.parameters = {};
         vm.results = undefined;
         vm.executionTime = undefined;
         vm.executedOn = undefined;
         vm.InitReportChartData();
      }
      vm.InitReportChartData = function () {
         vm.reportBarChartLabels = [];
         vm.reportBarChartData = [[], [], [], []];
         vm.reportBarChartSeries = [];
         vm.repeatedBusinessConsultants = undefined;
         vm.CsvData = [];
         vm.Totals = {};
      }
   }
   ReportsController.$inject = ["$log", "$scope", "ReportsFactory", "$localStorage", "$q", "AddressFactory", "SalesFactory", "LeadFactory", "ProductFactory", "ToastFactory"];
   module.controller("ReportsController", ReportsController);
})();