/**
 * Send API Request
 * @param {any} url
 * @param {any} method
 * @returns {jqXHR}
 */
function sendRequest(url, method = 'GET', data = undefined) {
    return $.ajax({
        url: $.appendRootToPath(url),
        method: method,
        data: data,
        dataType: "json"
    });
}

/**
 * Show json in a pretty print fashion
 */
var PrettyPrint = (function () {

    // Constructor
    function PrettyPrint(jsonObject, withCopyButton = false) {
        this._jsonObject = jsonObject;
        this._withCopyButton = withCopyButton;
    }

    function replacer(match, pIndent, pKey, pVal, pEnd) {
        var key = '<span class=json-key>';
        var val = '<span class=json-value>';
        var str = '<span class=json-string>';
        var r = pIndent || '';

        if (pKey)
            r = r + key + pKey.replace(/[": ]/g, '') + '</span>: ';
        if (pVal) {
            console.log(`this._withCopyButton => ${this._withCopyButton}`)
            if (this._withCopyButton) {
                r = r + (pVal[0] == '"' ? str : val) + pVal + '<button type="button" class="btn-copy btn btn-xs btn-success ml-1"><i class="fas fa-copy"></i></button></span>';
            } else {
                r = r + (pVal[0] == '"' ? str : val) + pVal;
            }
        }

        return r + (pEnd || '');
    };

    PrettyPrint.prototype.print = function () {
        var jsonLine = /^( *)("[\w]+": )?("[^"]*"|[\w.+-]*)?([,[{])?$/mg;
        return JSON.stringify(this._jsonObject, null, 3)
            .replace(/&/g, '&amp;').replace(/\\"/g, '&quot;')
            .replace(/</g, '&lt;').replace(/>/g, '&gt;')
            .replace(jsonLine, replacer);
    };
}());