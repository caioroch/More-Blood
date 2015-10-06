"use strict";
/* https://developer.mozilla.org/en-US/docs/Web/API/Document/cookie
:: cookies.js ::

A complete cookies reader/writer framework with full unicode support.

Revision #1 - September 4, 2014

https://developer.mozilla.org/en-US/docs/Web/API/document.cookie
https://developer.mozilla.org/User:fusionchess

This framework is released under the GNU Public License, version 3 or later.
http://www.gnu.org/licenses/gpl-3.0-standalone.html

Modified by Carlos Rafael Gimenes das Neves
*/
window.Cookies = {
    create: function (name, value, expires, path, domain, secure) {
        if (!name || /^(?:expires|max\-age|path|domain|secure)$/i.test(name)) return false;
        var exp = "";
        if (expires) {
            switch (expires.constructor) {
                case Number:
                    if (expires === Infinity) {
                        exp = "; expires=Fri, 31 Dec 9999 23:59:59 GMT";
                    } else {
                        exp = new Date();
                        exp.setTime(exp.getTime() + (expires * 60 * 60 * 1000));
                        exp = "; expires=" + exp.toUTCString();
                    }
                    break;
                case Date:
                    exp = "; expires=" + expires.toUTCString();
                    break;
                case String:
                    exp = "; expires=" + expires;
                    break;
            }
        }
        document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + exp + (path ? "; path=" + path : "") + (domain ? "; domain=" + domain : "") + (secure ? "; secure" : "");
        return true;
    },
    get: function (name) {
        return (!name ? null : (decodeURIComponent(document.cookie.replace(new RegExp("(?:(?:^|.*;)\\s*" + encodeURIComponent(name).replace(/[\-\.\+\*]/g, "\\$&") + "\\s*\\=\\s*([^;]*).*$)|^.*$"), "$1")) || null));
    },
    remove: function (name, path, domain) {
        if (!Cookies.exists(name)) return false;
        document.cookie = encodeURIComponent(name) + "=; expires=Thu, 01 Jan 1970 00:00:00 GMT" + (path ? "; path=" + path : "") + (domain ? "; domain=" + domain : "");
        return true;
    },
    exists: function (name) {
        if (!name) return false;
        return (new RegExp("(?:^|;\\s*)" + encodeURIComponent(name).replace(/[\-\.\+\*]/g, "\\$&") + "\\s*\\=")).test(document.cookie);
    },
    names: function () {
        var ns = document.cookie.replace(/((?:^|\s*;)[^\=]+)(?=;|$)|^\s*|\s*(?:\=[^;]*)?(?:\1|$)/g, "").split(/\s*(?:\=[^;]*)?;\s*/);
        for (var len = ns.length, idx = 0; idx < len; idx++) ns[idx] = decodeURIComponent(ns[idx]);
        return ns;
    }
};
