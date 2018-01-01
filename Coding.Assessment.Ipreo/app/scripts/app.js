(function() {
    'use strict';

    window.Ipreo = {};

    window.Ipreo.getNamespace = function(ns) {
        var namespaceParts = ns.split('.'),
            namespacePart,
            namespace = window,
            i = 0;

        for (i; i < namespaceParts.length; i++) {
            namespacePart = namespaceParts[i];
            if (namespace[namespacePart] === undefined || namespace[namespacePart] === null) {
                namespace[namespacePart] = {}
            }

            namespace = namespace[namespacePart];
        }

        return namespace;
    }

})();