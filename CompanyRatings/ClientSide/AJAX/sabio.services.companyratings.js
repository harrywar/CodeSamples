sabio.services.companyratings = sabio.services.companyratings || {};

sabio.services.companyratings.insert = function (data, onSuccess, onError) {

    var url = "/api/ratings/insert";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);

};

sabio.services.companyProfile.getQuotesByBuyerCompanyId = function (companyId, onSuccess, onError) {
    var url = "/api/companies/" + companyId + "/buyerquotes";

    var settings = {
        cache: false,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        type: "GET",
        success: onxSuccess,
        error: onError
    };

    $.ajax(url, settings);
}





sabio.services.companyratings.getByCompanyId = function (id, onSuccess, onError) {

    var url = "/api/ratings/get/" + id;

    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , dataType: "json"
    , success: onSuccess
    , error: onError
    , type: "GET"
    };

    $.ajax(url, settings);

};



sabio.services.companyratings.getAverageByCompanyId = function (id, onSuccess, onError) {

    var url = "/api/ratings/average/" + id;

    var settings = {
        cache: false
    , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
    , dataType: "json"
    , success: onSuccess
    , error: onError
    , type: "GET"
    };

    $.ajax(url, settings);

};
