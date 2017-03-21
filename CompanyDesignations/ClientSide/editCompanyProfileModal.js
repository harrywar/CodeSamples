// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Edit Company Profile Modal Controller
(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('companyModalController', CompanyModalController);

    // $uibModalInstance is coming from the UI Bootstrap library and is a reference to the
    //    modal window itself so we can work with it.
    // companyDetails is the array passed in from the parent controller above through the resolve property.
    CompanyModalController.$inject = ['$scope', '$baseController', '$uibModalInstance', '$alertService', '$companyProfileService', 'companyDetails']

    function CompanyModalController(
        $scope
        , $baseController
        , $uibModalInstance
        , $alertService
        , $companyProfileService
        , companyDetails) {

        var vm = this;

        $baseController.merge(vm, $baseController);

        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;
        vm.alertService = $alertService;
        vm.$companyProfileService = $companyProfileService;
        vm.modalItems = companyDetails.payload;
        vm.companyDesignations = companyDetails.designations;

        vm.ok = _ok;
        vm.cancel = _cancel;
        vm.submitForm = _submitForm;
        vm.companyId = sabio.p.companyId;

        vm.onDropzoneSending = _onDropzoneSending;
        vm.onDropzoneSuccess = _onDropzoneSuccess;
        vm.submitCompanyEdit = _submitCompanyEdit;
        vm.editCompany = _editCompany;
        vm.receiveEditedItem = _receiveEditedItem;

        vm.smallBusiness = vm.companyDesignations.smallBusiness;
        vm.veteranOwned = vm.companyDesignations.veteranOwned;
        vm.minorityOwned = vm.companyDesignations.minorityOwned;
        vm.womenOwned = vm.companyDesignations.womenOwned;

        vm.designationsTotal = 0;
        vm.changeBadge = _changeBadge;

        vm.smallBusinessOwnedURL = (vm.smallBusiness) ? "/Content/Theme/img/companyType/Small-Business-48.png" : "/Content/Theme/img/companyType/Small-Business-48-grey.png";
        vm.veteranOwnedURL = (vm.veteranOwned) ? "/Content/Theme/img/companyType/Medal-48.png" : "/Content/Theme/img/companyType/Medal-48-grey.png";
        vm.minorityOwnedURL = (vm.minorityOwned) ? "/Content/Theme/img/companyType/Racism-48.png" : "/Content/Theme/img/companyType/Racism-48-grey.png";
        vm.womenOwnedURL = (vm.womenOwned) ? "/Content/Theme/img/companyType/Female-48.png" : "/Content/Theme/img/companyType/Female-48-grey.png";

        vm.notify = vm.$companyProfileService.getNotifier($scope);

        vm.payload = {
            "companyId": sabio.p.companyId,
            "ownerId": sabio.p.companyOwnerId,
            "name": "",
            "typeId": sabio.p.companyRole,
            "phone": '',
            "fax": '',
            "email": '',
            "mediaId": 0,
            "url": "",
            "designations": 0
        };

        vm.designations = {
            SmallBusiness: 1,
            VeteranOwned: 2,
            MinorityOwned: 4,
            WomenOwned: 8
        }

        function _changeBadge($event, control, activeURL, inactiveURL) {

            //console.log('changeBadge fired');

            var checkbox = $event.target;

            if (checkbox.checked) {

                vm[control] = activeURL;

            }

            else {

                vm[control] = inactiveURL;
            };

        };

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //console.log('vm.designations', vm.designations);
       
        function _submitForm(isValid) {
            if (isValid) {
                //console.log("data is valid! go save this object -> ");


            } else {
                console.log("form submitted with invalid data :(")
            }
        };

        //  $uibModalInstance is used to communicate and send data back to parent controller
        function _ok() {

            vm.$uibModalInstance.close();

        };

        function _cancel() {
            //console.log("editProfile.cancel");
            vm.$uibModalInstance.dismiss('cancel');
        };

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _onDropzoneSending(file, xhr, formData) {
            console.log("DZ Sending");

            var mediaType = sabio.mediaType; // <-- Set this value in the upload modal. 

            // Rest value to null before closing modal
            var userId = sabio.p.currentUserId; // <-- global variable for userid
            var companyId = Number(sabio.p.companyId);  // <-- global variable for company id

            formData.append("MediaType", mediaType);
            formData.append("UserId", userId);
            formData.append("CompanyId", companyId);

        }; // End _onDropzoneSending

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _onDropzoneSuccess(file, response) {
            console.log("DZ Success");

            var id = Number(sabio.p.companyId);

            var payload = {
                'CompanyId': id,
                'MediaId': response.item
            };

            var onSuccess = function (data) {
                console.log("update success! ", data);
            };
            var onError = function (data) {
                console.log("An error occured: ", data);
            };

            sabio.services.companyProfile.editMediaId(id, payload, updateSuccess, modalError);

        }; // End _onDropzoneSuccess

        function _submitCompanyEdit() {

            var confirmAnswer = confirm("By selecting the type of company, you are confirming that you acknowledge your company profile will be shown with the selections you have made. It is your responsibility as the company owner to be able to show proof of your selections.");

            if (confirmAnswer == true) {

                vm.designationsTotal = vm.smallBusiness + vm.womenOwned + vm.veteranOwned + vm.minorityOwned;

                //console.log('vm.designationsTotal', vm.designationsTotal);

                _editCompany(vm.modalItems)

                _ok();

            }

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _editCompany(modalItems) {

            //console.log('designationsTotal:', vm.designationsTotal);

            vm.payload = {
                "companyId": sabio.p.companyId,
                "ownerId": sabio.p.companyOwnerId,
                "typeId": sabio.p.companyRole,
                "name": modalItems.name,
                "phone": modalItems.phone,
                "fax": modalItems.fax,
                "email": modalItems.email,
                "url": modalItems.url,
                "designations": vm.designationsTotal,
                "mediaId": modalItems.mediaId

            };

            console.log('vm.payload', vm.payload);

            vm.$companyProfileService.edit(vm.payload.companyId, vm.payload, vm.receiveEditedItem, vm.onEmpError);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _receiveEditedItem() {
            //console.log("Recieve edited item.");

            //vm.renderThisCompany();
        };

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _onEmpError(jqXhr, error) {
            console.error(error);
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function updateSuccess() {
            console.log('click save');
            vm.alertService.success('Updated Profile', 'Success!');
            //vm.$uibModalInstance.close();
        };


        //// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

         function modalError (data) {
            console.log('data', data);
            vm.alertService.error('There was an error!', 'Uh-Oh');
        //    vm.$uibModalInstance.dismiss('cancel');
        };



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    }
})();