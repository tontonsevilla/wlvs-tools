/**
 * Send API Request
 * @param {any} url
 * @param {any} method
 * @returns {jqXHR}
 */
function sendRequest(url, method = 'GET', data = undefined) {
    $.customLoader.loadImage();

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
function PrettyPrint(jsonObject, withCopyButton = false) {
    var _jsonObject = jsonObject;
    var _withCopyButton = withCopyButton;

    var replacer = function (match, pIndent, pKey, pVal, pEnd) {
        var key = '<span class=json-key>';
        var val = '<span class=json-value>';
        var str = '<span class=json-string>';
        var r = pIndent || '';

        if (pKey)
            r = r + key + pKey.replace(/[": ]/g, '') + '</span>: ';
        if (pVal) {
            if (_withCopyButton) {
                r = r + (pVal[0] == '"' ? str : val) + pVal + '<button type="button" class="btn-copy btn btn-xs btn-success ml-1"><i class="fas fa-copy"></i></button></span>';
            } else {
                r = r + (pVal[0] == '"' ? str : val) + pVal;
            }
        }

        return r + (pEnd || '');
    };

    this.print = function () {
        var jsonLine = /^( *)("[\w]+": )?("[^"]*"|[\w.+-]*)?([,[{])?$/mg;
        return JSON.stringify(_jsonObject, null, 3)
            .replace(/&/g, '&amp;').replace(/\\"/g, '&quot;')
            .replace(/</g, '&lt;').replace(/>/g, '&gt;')
            .replace(jsonLine, replacer);
    };
}
