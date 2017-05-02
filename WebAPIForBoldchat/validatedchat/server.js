var bc = {};

function sendXHRReq(type) {
    var resultElem = document.getElementById("result" + type);
    resultElem.value = "";

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "../api/" + type.toLowerCase());
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.response);
                console.log("success - statusText=" + xhr.statusText, response);
                resultElem.value = response;
            } else {
                console.log("error - statusText=" + xhr.statusText, xhr.response);
                alert("Oops... an error occurred. Please check your console.");
            }
        }
    };

    xhr.timeout = 300000;
    xhr.onerror = function (e) {
        console.log("error occurred: " + e);
    };
    xhr.ontimeout = function () {
        console.log("timeout reached");
    };

    var vc = {};
    if (type === 'Pgp') {
        vc.secureParameters = bc.elemPgpSecureParm.value;
        vc.encryptionKey = bc.elemPEK.value;
        vc.signingKey = bc.elemPSK.value;
        vc.password = bc.elemPW.value;

        localStorage.setItem(bc.prefix + bc.name.secureParametersPgp, vc.secureParameters);
        localStorage.setItem(bc.prefix + bc.name.publicEncryptionKey, vc.encryptionKey);
        localStorage.setItem(bc.prefix + bc.name.privateSigningKey, vc.signingKey);
        localStorage.setItem(bc.prefix + bc.name.password, vc.password);
    } else {
        vc.secureParameters = bc.elemShaSecureParm.value;
        vc.hashKey = bc.elemHK.value;

        localStorage.setItem(bc.prefix + bc.name.secureParametersSha, vc.secureParameters);
        localStorage.setItem(bc.prefix + bc.name.hashKey, vc.hashKey);
    }

    xhr.send(JSON.stringify(vc));

    return false;
}

(function () {
    bc.prefix = "bc_vc_temp_";
    bc.name = {
        publicEncryptionKey: "publicEncryptionKey",
        privateSigningKey: "privateSigningKey",
        password: "password",
        secureParametersPgp: "secureParametersPgp",
        secureParametersSha: "secureParametersSha",
        hashKey: "hashKey"
    }

    // PGP
    bc.selectPgpVersion = document.getElementById('pgpVersion');
    bc.elemPgpSecureParm = document.getElementById(bc.name.secureParametersPgp);
    bc.elemPEK = document.getElementById(bc.name.publicEncryptionKey);
    bc.elemPSK = document.getElementById(bc.name.privateSigningKey);
    bc.elemPW = document.getElementById(bc.name.password);

    bc.elemPgpSecureParm.value = localStorage.getItem(bc.prefix + bc.name.secureParametersPgp);
    bc.elemPEK.value = localStorage.getItem(bc.prefix + bc.name.publicEncryptionKey);
    bc.elemPSK.value = localStorage.getItem(bc.prefix + bc.name.privateSigningKey);
    bc.elemPW.value = localStorage.getItem(bc.prefix + bc.name.password);

    //SHA512
    bc.selectShaVersion = document.getElementById('shaVersion');
    bc.elemShaSecureParm = document.getElementById(bc.name.secureParametersSha);
    bc.elemHK = document.getElementById(bc.name.hashKey);

    bc.elemShaSecureParm.value = localStorage.getItem(bc.prefix + bc.name.secureParametersSha);
    bc.elemHK.value = localStorage.getItem(bc.prefix + bc.name.hashKey);
})();
