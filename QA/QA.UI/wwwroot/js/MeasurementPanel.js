window.setFocus = function (element) {
    if (element) {
        element.focus();
    }
};

window.clearInput = function (element) {
    if (element) {
        element.value = "";
    }
};

function openInNewTab(url) {
    window.open(url, '_blank');
}

//window.confirmclose = {
//    initialize: function () {
//        window.addeventlistener('beforeunload', function (e) {
//            var confirmationmessage = 'Zapisz zmiany przed opuszczenie strony?';
//            e.returnvalue = confirmationmessage;
//            return confirmationmessage;
//        });
//    }
//};

