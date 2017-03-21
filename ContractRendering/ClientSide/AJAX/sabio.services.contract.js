sabio.services.contract = sabio.services.contract || {};

sabio.services.contract.saveContract = function (data, onSuccess, onError) {

    var url = "/api/contract/insert";

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        dataType: "JSON",
        success: onSuccess,
        error: onError,
        type: "POST"
    };

    $.ajax(url, settings);

};


sabio.services.contract.updateContractURL = function (id, data, onSuccess, onError) {

    var url = "/api/contract/" + id;

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: data,
        dataType: "JSON",
        success: onSuccess,
        error: onError,
        type: "PUT"
    };

    $.ajax(url, settings);

};

sabio.services.contract.getContract = function (id, onSuccess, onError) {

    var url = "/api/contract/get/" + id;

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "JSON",
        success: onSuccess,
        error: onError,
        type: "POST"
    };

    $.ajax(url, settings);

};
