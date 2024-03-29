﻿(function () {
    window.addEventListener('load', function () {
        var canvas = this.__canvas || this.canvas,
            canvases = this.__canvases || this.canvases;

        canvas && canvas.calcOffset && canvas.calcOffset();

        if (canvases && canvases.length) {
            for (var i = 0, len = canvases.length; i < len; i++) {
                canvases[i].calcOffset();
            }
        }
    });
})();