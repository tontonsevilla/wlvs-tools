(function ($) {
    "use strict";

    $.getRootPath = function () {
        var path = "/"
        try {
            if (typeof global_approot !== 'undefined')
                path = global_approot;
        } catch (e) {
        }
        return path;
    }
})(jQuery);


(function ($) {
    "use strict";
    $.appendRootToPath = function (path) {
        return $.getRootPath() + path;
    };

})(jQuery);