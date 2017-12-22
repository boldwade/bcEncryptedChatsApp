interface IEncryptionBase {
    name?: any;
    secureParameters?: any;
}

interface IElementBase {
    resultElement?: any;
    versionSelectElement?: any;
    submitButton?: any;
    deleteButton?: any;
}

interface IPgp extends IEncryptionBase, IElementBase {
    encryptionKey?: any;
    signingKey?: any;
    password?: any;
}

interface ISha extends IEncryptionBase, IElementBase {
    hashKey?: any;
}

interface IBcEncryptionTypes {
    pgp?: IPgp;
    sha?: ISha;
}

let bcElements: IBcEncryptionTypes = {
    pgp: {},
    sha: {}
};

enum EncryptionTypes {
    Pgp = "pgp",
    Sha = "sha"
}

const ids = {
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

function sendXhrReq(type: EncryptionTypes) {
    bcElements[type].resultElement.value = "";

    const xhr = new XMLHttpRequest();
    xhr.open("POST", `../api/${type}`);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                const response = JSON.parse(xhr.response);
                bcElements[type].resultElement.value = response;
            } else {
                console.log(`error - statusText=${xhr.statusText}`, xhr);
                alert("Oops... an error occurred. Please check your console.");
            }
        }
    };

    xhr.timeout = 300000;
    xhr.onerror = e => {
        console.log(`error occurred: ${e}`);
    };
    xhr.ontimeout = () => {
        console.log("timeout reached");
    };

    let storageItems = bcStorageItems(type);
    let foundIndex = -1;
    if (storageItems) {
        foundIndex = storageItems.findIndex(item => item.name === bcElements[type].name.value);
    }
    let currentItem: IEncryptionBase;
    if (type === EncryptionTypes.Pgp) {
        const pgp: IPgp = {
            secureParameters: bcElements.pgp.secureParameters.value,
            encryptionKey: bcElements.pgp.encryptionKey.value,
            signingKey: bcElements.pgp.signingKey.value,
            password: bcElements.pgp.password.value
        };
        currentItem = pgp;
    } else {
        const sha: ISha = {
            secureParameters: bcElements.sha.secureParameters.value,
            hashKey: bcElements.sha.hashKey.value
        };
        currentItem = sha;
    }
    currentItem.name = bcElements[type].name.value;

    if (foundIndex > -1) {
        storageItems[foundIndex] = currentItem;
    } else {
        if (storageItems) {
            storageItems.push(currentItem);
        } else {
            storageItems = [currentItem];
        }
    }

    localStorage.setItem(ids.Prefix + type, JSON.stringify(storageItems));

    xhr.send(JSON.stringify(currentItem));

    return false;
}


function bcStorageItems(type: EncryptionTypes): [IPgp] {
    const ls: any = localStorage.getItem(ids.Prefix + type) || [];
    if (ls.length > 0) {
        return (JSON.parse(ls));
        // } else {
        //     return [{}];
    }
    return null;
}

function createNewSetupItem(type: EncryptionTypes): string {
    const name = prompt("What do you want to name this setup?");

    if (name) {
        const storageItems = bcStorageItems(type);
        if (storageItems) {
            if (storageItems.findIndex(item => item.name === name) > -1) {
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

function sortSelect(selElem: any) {
    const tmpAry = [];
    let i: 0;
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
        const op = new Option(tmpAry[i][0], tmpAry[i][1]);
        selElem.options[i] = op;
        if (op.value === "0") {
            selElem.options[i].hidden = true;
            selElem.options[i].disabled = true;
        }
    }
    return;
}

function clearSection(type: EncryptionTypes) {
    Object.keys(bcElements[type])
        .filter(key => key !== "versionSelectElement" && key !== "submitButton" && key !== "deleteButton")
        .forEach(key => {
            bcElements[type][key].value = "";
        });
}

function onVersionSelectChange(type: EncryptionTypes, value: string) {
    bcElements[type].deleteButton.className = "delete";
    let name: string = "";
    if (value === "1") {
        name = createNewSetupItem(type);
        if (name) {
            bcElements[type].name.value = name;
            if (type === EncryptionTypes.Pgp) {
                bcElements.pgp.encryptionKey.focus();
            } else {
                bcElements.sha.hashKey.focus();
            }
        } else {
            bcElements[type].versionSelectElement.value = "0";
            bcElements[type].deleteButton.className = "delete disabled";
        }
    } else {
        name = bcElements[type].versionSelectElement.value;
        const storageItems = bcStorageItems(type);
        if (storageItems) {
            if (type === EncryptionTypes.Pgp) {
                assignPgpValues(bcStorageItems(type).find(item => item.name === name) as IPgp);
            } else {
                assignShaValues(bcStorageItems(type).find(item => item.name === name) as ISha);
            }
        }
        bcElements[type].submitButton.focus();
    }

    return false;
}

function onDeleteItem(type: EncryptionTypes) {
    bcElements[type].deleteButton.className = "delete disabled";

    const items = bcStorageItems(type);
    if (items) {
        items.splice(items.findIndex(item => item.name === bcElements[type].versionSelectElement.value), 1);
        (bcElements[type].versionSelectElement.options as HTMLOptionsCollection).remove(bcElements[type].versionSelectElement.options.selectedIndex)
    }

    localStorage.setItem(ids.Prefix + type, JSON.stringify(items));

    bcElements[type].versionSelectElement.value = "0";
    clearSection(type);
}

function assignPgpValues(pgp: IPgp) {
    if (pgp) {
        bcElements.pgp.versionSelectElement.value = pgp.name;
        bcElements.pgp.name.value = pgp.name;
        bcElements.pgp.secureParameters.value = pgp.secureParameters;

        bcElements.pgp.encryptionKey.value = pgp.encryptionKey;
        bcElements.pgp.signingKey.value = pgp.signingKey;
        bcElements.pgp.password.value = pgp.password;

    } else {
        throw new Error("No Pgp Values found");
    }
}

function assignShaValues(sha: ISha) {
    if (sha) {
        bcElements.sha.versionSelectElement.value = sha.name;
        bcElements.sha.name.value = sha.name;
        bcElements.sha.secureParameters.value = sha.secureParameters;

        bcElements.sha.hashKey.value = sha.hashKey;

    } else {
        throw new Error("No Sha Values found");
    }
}

function assignAndSortValues(type: EncryptionTypes) {
    const storageItems = bcStorageItems(type);
    if (storageItems && storageItems.length > 0 && storageItems[0].name) {
        storageItems
            .sort((a: IEncryptionBase, b: IEncryptionBase) => {
                return a.name < b.name ? -1 : 1;
            })
            .forEach(data => {
                bcElements[type].versionSelectElement.options[bcElements[type].versionSelectElement.options.length] = new Option(data.name);
            });

        if (type === EncryptionTypes.Pgp) {
            const firstPgpItem = storageItems[0] as IPgp;
            assignPgpValues(firstPgpItem);
        } else {
            const firstShaItem = storageItems[0] as ISha;
            assignShaValues(firstShaItem);
        }
    }
}

(() => {
    // PGP
    bcElements.pgp.deleteButton = document.getElementById("pgp_delete")
    bcElements.pgp.resultElement = document.getElementById("pgp_result");
    bcElements.pgp.versionSelectElement = document.getElementById("pgp_version");
    bcElements.pgp.name = document.getElementById("pgp_name");
    bcElements.pgp.submitButton = document.getElementById("pgp_submit");
    bcElements.pgp.secureParameters = document.getElementById(ids.SecureParameters.Pgp);

    bcElements.pgp.encryptionKey = document.getElementById(ids.PublicEncryptionKey);
    bcElements.pgp.signingKey = document.getElementById(ids.PrivateSigningKey);
    bcElements.pgp.password = document.getElementById(ids.Password);

    // SHA512
    bcElements.sha.deleteButton = document.getElementById("sha_delete")
    bcElements.sha.resultElement = document.getElementById("sha_result");
    bcElements.sha.versionSelectElement = document.getElementById("sha_version");
    bcElements.sha.name = document.getElementById("sha_name");
    bcElements.sha.submitButton = document.getElementById("sha_submit");
    bcElements.sha.secureParameters = document.getElementById(ids.SecureParameters.Sha);

    bcElements.sha.hashKey = document.getElementById(ids.HashKey);

    assignAndSortValues(EncryptionTypes.Pgp);
    assignAndSortValues(EncryptionTypes.Sha);
})();
