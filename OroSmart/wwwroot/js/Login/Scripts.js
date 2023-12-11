    
        $('body').show();
        $('.version').text(NProgress.version);
        NProgress.start();
        setTimeout(function () { NProgress.done(); $('.fade').removeClass('out'); }, 1000);

        $("#b-0").click(function () { NProgress.start(); });
        $("#b-40").click(function () { NProgress.set(0.4); });
        $("#b-inc").click(function () { NProgress.inc(); });
        $("#b-100").click(function () { NProgress.done(); });

        var HN = [];
        HN.factory = function (e) {
            return function () {
                HN.push([e].concat(Array.prototype.slice.call(arguments, 0)));
            };
        };
        HN.on = HN.factory("on");
        HN.once = HN.factory("once");
        HN.off = HN.factory("off");
        HN.emit = HN.factory("emit");
        HN.load = function () {
            var e = "hn-button.js";
            if (document.getElementById(e)) return;
            var t = document.createElement("script");
            t.id = e;
            t.src = "//hn-button.herokuapp.com/hn-button.js";
            var n = document.getElementsByTagName("script")[0];
            n.parentNode.insertBefore(t, n);
        };
        HN.load();


        $('body').show();
        NProgress.start();
        setTimeout(function () { NProgress.done(); }, 20);
