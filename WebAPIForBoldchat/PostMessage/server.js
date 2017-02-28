function sendXHRReq(data) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', "../api/util/sha512/" + data);
    //xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                console.log('success - statusText=' + xhr.statusText + ' : responseText=' + xhr.responseText);
            } else {
                console.log('error - statusText=' + xhr.statusText + ' : responseText=' + xhr.responseText);
            }

            if (parent && parent.postMessage) {
                parent.postMessage(xhr.responseText, '*');
            }
        }
    };
    xhr.timeout = 30000;
    xhr.onerror = function (e) {
        console.log('onError: ' + e);
    };
    xhr.ontimeout = function () {
        console.log('onTimeout');
    };

    debugger;
    xhr.send('bodyData=' + data);
}

function listener(event) {
    //if (event.origin !== "http://javascript.info")
    //    return;
    debugger;
    sendXHRReq(event.data);
}

//if (window.addEventListener) {
//    addEventListener("message", listener, false);
//} else {
//    attachEvent("onmessage", listener);
//}

var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
var eventer = window[eventMethod];
var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";
eventer(messageEvent, listener, false);
