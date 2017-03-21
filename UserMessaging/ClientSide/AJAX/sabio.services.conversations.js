sabio.services.conversations = sabio.services.conversations || {};

sabio.services.conversations.insert = function (data, onSuccess, onError) {

    var url =   "/api/conversations/insert";

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



// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

sabio.services.conversations.getBySenderId = function (id, onSuccess, onError) {

    var url = "/api/conversations/get/" + id;

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



// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

sabio.services.conversations.checkConversationExists = function (data, onSuccess, onError) {

    var url =   "/api/conversations/check";

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



// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

sabio.services.conversations.getAllUserProfiles = function (onAjaxSuccess, onAjaxError) {

    var url = "/api/profile";

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onAjaxSuccess,
        error: onAjaxError,
        type: "GET"

    };

    $.ajax(url, settings);

};


sabio.services.conversations.getUnreadConversationsByUserId = function (id, onSuccess, onError) {

    var url = "/api/conversations/unread/?userId=".concat(id);

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);
};


