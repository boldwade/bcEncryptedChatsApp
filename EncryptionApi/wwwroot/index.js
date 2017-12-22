var bcElements = {
    pgp: {},
    sha: {}
};
var EncryptionTypes;
(function (EncryptionTypes) {
    EncryptionTypes["Pgp"] = "pgp";
    EncryptionTypes["Sha"] = "sha";
})(EncryptionTypes || (EncryptionTypes = {}));
var ids = {
    Prefix: "bc_ec_vars_",
    PublicEncryptionKey: "pgp_publicEncryptionKey",
    PrivateSigningKey: "pgp_privateSigningKey",
    Password: "pgp_password",
    SecureParameters: {
        Pgp: "pgp_secureParameters",
        Sha: "sha_secureParameters"
    },
    HashKey: "sha_hashKey"
};
function sendXhrReq(type) {
    bcElements[type].resultElement.value = "";
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "../api/" + type);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.response);
                bcElements[type].resultElement.value = response;
            }
            else {
                console.log("error - statusText=" + xhr.statusText, xhr);
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
    var storageItems = bcStorageItems(type);
    var foundIndex = -1;
    if (storageItems) {
        foundIndex = storageItems.findIndex(function (item) { return item.name === bcElements[type].name.value; });
    }
    var currentItem;
    if (type === EncryptionTypes.Pgp) {
        var pgp = {
            secureParameters: bcElements.pgp.secureParameters.value,
            encryptionKey: bcElements.pgp.encryptionKey.value,
            signingKey: bcElements.pgp.signingKey.value,
            password: bcElements.pgp.password.value
        };
        currentItem = pgp;
    }
    else {
        var sha = {
            secureParameters: bcElements.sha.secureParameters.value,
            hashKey: bcElements.sha.hashKey.value
        };
        currentItem = sha;
    }
    currentItem.name = bcElements[type].name.value;
    if (foundIndex > -1) {
        storageItems[foundIndex] = currentItem;
    }
    else {
        if (storageItems) {
            storageItems.push(currentItem);
        }
        else {
            storageItems = [currentItem];
        }
    }
    localStorage.setItem(ids.Prefix + type, JSON.stringify(storageItems));
    xhr.send(JSON.stringify(currentItem));
    return false;
}
function bcStorageItems(type) {
    var ls = localStorage.getItem(ids.Prefix + type) || [];
    if (ls.length > 0) {
        return (JSON.parse(ls));
        // } else {
        //     return [{}];
    }
    return null;
}
function createNewSetupItem(type) {
    var name = prompt("What do you want to name this setup?");
    if (name) {
        var storageItems = bcStorageItems(type);
        if (storageItems) {
            if (storageItems.findIndex(function (item) { return item.name === name; }) > -1) {
                alert("This nameElemnt already exists, please try again...");
                return createNewSetupItem(type);
            }
        }
        clearSection(type);
        bcElements[type].versionSelectElement.options[bcElements[type].versionSelectElement.options.length] = new Option(name);
        sortSelect(bcElements[type].versionSelectElement);
        bcElements[type].versionSelectElement.value = name;
    }
    return name;
}
function sortSelect(selElem) {
    var tmpAry = [];
    var i;
    for (i = 0; i < selElem.options.length; i++) {
        tmpAry[i] = [];
        tmpAry[i][0] = selElem.options[i].text;
        tmpAry[i][1] = selElem.options[i].value;
    }
    tmpAry.sort();
    while (selElem.options.length > 0) {
        selElem.options[0] = null;
    }
    for (i = 0; i < tmpAry.length; i++) {
        var op = new Option(tmpAry[i][0], tmpAry[i][1]);
        selElem.options[i] = op;
        if (op.value === "0") {
            selElem.options[i].hidden = true;
            selElem.options[i].disabled = true;
        }
    }
    return;
}
function clearSection(type) {
    Object.keys(bcElements[type])
        .filter(function (key) { return key !== "versionSelectElement" && key !== "submitButton" && key !== "deleteButton"; })
        .forEach(function (key) {
        bcElements[type][key].value = "";
    });
}
function onVersionSelectChange(type, value) {
    bcElements[type].deleteButton.className = "delete";
    var name = "";
    if (value === "1") {
        name = createNewSetupItem(type);
        if (name) {
            bcElements[type].name.value = name;
            if (type === EncryptionTypes.Pgp) {
                bcElements.pgp.encryptionKey.focus();
            }
            else {
                bcElements.sha.hashKey.focus();
            }
        }
        else {
            bcElements[type].versionSelectElement.value = "0";
            bcElements[type].deleteButton.className = "delete disabled";
        }
    }
    else {
        name = bcElements[type].versionSelectElement.value;
        var storageItems = bcStorageItems(type);
        if (storageItems) {
            if (type === EncryptionTypes.Pgp) {
                assignPgpValues(bcStorageItems(type).find(function (item) { return item.name === name; }));
            }
            else {
                assignShaValues(bcStorageItems(type).find(function (item) { return item.name === name; }));
            }
        }
        bcElements[type].submitButton.focus();
    }
    return false;
}
function onDeleteItem(type) {
    bcElements[type].deleteButton.className = "delete disabled";
    var items = bcStorageItems(type);
    if (items) {
        items.splice(items.findIndex(function (item) { return item.name === bcElements[type].versionSelectElement.value; }), 1);
        bcElements[type].versionSelectElement.options.remove(bcElements[type].versionSelectElement.options.selectedIndex);
    }
    localStorage.setItem(ids.Prefix + type, JSON.stringify(items));
    bcElements[type].versionSelectElement.value = "0";
    clearSection(type);
}
function assignPgpValues(pgp) {
    if (pgp) {
        bcElements.pgp.versionSelectElement.value = pgp.name;
        bcElements.pgp.name.value = pgp.name;
        bcElements.pgp.secureParameters.value = pgp.secureParameters;
        bcElements.pgp.encryptionKey.value = pgp.encryptionKey;
        bcElements.pgp.signingKey.value = pgp.signingKey;
        bcElements.pgp.password.value = pgp.password;
    }
    else {
        throw new Error("No Pgp Values found");
    }
}
function assignShaValues(sha) {
    if (sha) {
        bcElements.sha.versionSelectElement.value = sha.name;
        bcElements.sha.name.value = sha.name;
        bcElements.sha.secureParameters.value = sha.secureParameters;
        bcElements.sha.hashKey.value = sha.hashKey;
    }
    else {
        throw new Error("No Sha Values found");
    }
}
function assignAndSortValues(type) {
    var storageItems = bcStorageItems(type);
    if (storageItems && storageItems.length > 0 && storageItems[0].name) {
        storageItems
            .sort(function (a, b) {
            return a.name < b.name ? -1 : 1;
        })
            .forEach(function (data) {
            bcElements[type].versionSelectElement.options[bcElements[type].versionSelectElement.options.length] = new Option(data.name);
        });
        if (type === EncryptionTypes.Pgp) {
            var firstPgpItem = storageItems[0];
            assignPgpValues(firstPgpItem);
        }
        else {
            var firstShaItem = storageItems[0];
            assignShaValues(firstShaItem);
        }
    }
}
(function () {
    // PGP
    bcElements.pgp.deleteButton = document.getElementById("pgp_delete");
    bcElements.pgp.resultElement = document.getElementById("pgp_result");
    bcElements.pgp.versionSelectElement = document.getElementById("pgp_version");
    bcElements.pgp.name = document.getElementById("pgp_name");
    bcElements.pgp.submitButton = document.getElementById("pgp_submit");
    bcElements.pgp.secureParameters = document.getElementById(ids.SecureParameters.Pgp);
    bcElements.pgp.encryptionKey = document.getElementById(ids.PublicEncryptionKey);
    bcElements.pgp.signingKey = document.getElementById(ids.PrivateSigningKey);
    bcElements.pgp.password = document.getElementById(ids.Password);
    // SHA512
    bcElements.sha.deleteButton = document.getElementById("sha_delete");
    bcElements.sha.resultElement = document.getElementById("sha_result");
    bcElements.sha.versionSelectElement = document.getElementById("sha_version");
    bcElements.sha.name = document.getElementById("sha_name");
    bcElements.sha.submitButton = document.getElementById("sha_submit");
    bcElements.sha.secureParameters = document.getElementById(ids.SecureParameters.Sha);
    bcElements.sha.hashKey = document.getElementById(ids.HashKey);
    assignAndSortValues(EncryptionTypes.Pgp);
    assignAndSortValues(EncryptionTypes.Sha);
})();
