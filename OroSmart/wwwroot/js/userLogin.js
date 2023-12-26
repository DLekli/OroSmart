$(document).ready(function () {
    const getSearch = (userName, ipAddress, lastLoginTime, lastlogoutTimeSearc) => {
        var urlParams = new URLSearchParams(window.location.search);
        //var pages = urlParams.get('pageNumber');
        //const pageNumber = pages
        urlParams.set("userNameSearch", userName);
        urlParams.set("ipAddressSearch", ipAddress);
        urlParams.set("loginTimeSearch", lastLoginTime);
        urlParams.set("logoutTimeSearch", lastlogoutTimeSearc);
        urlParams.set("pageNumber", 1);
        history.replaceState(null, null, "?" + urlParams.toString());
        var params = `?userNameSearch=${userName}&ipAddressSearch=${ipAddress}&loginTimeSearch=${lastLoginTime}&logoutTimeSearch=${lastlogoutTimeSearc}`
        var settings = {
            "url": `/Account/UserLoginHistoryOther${params}`,
            "method": "GET",
            "headers": {
                "Content-Type": "application/json"
            },
        };
        $.ajax(settings).done(function (response) {
            searchContainer.html(response)
        });
    }

    console.log("asdfadsfadsfdsafdsafdsaafdsafdsafdsafdsafdsafsadf");
    var userNameSearch = $("#userNameSearch_query")
    var ipAddressSearch = $("#ipAddressSearch_query")
    var loginTimeSearch = $("#loginTimeSearch_query")
    var logoutTimeSearch = $("#logoutTimeSearch_query")
    var searchForm = $("#searchForm")
    var searchContainer = $("#searchContainer")


    var query_userNameSearch = $('input[name="userNameSearch"]')
    var query_ipAddressSearch = $('input[name="ipAddressSearch"]')
    var query_lastLoginTimeSearch = $('input[name="loginTimeSearch"]')
    var query_logoutTimeSearch = $('input[name="logoutTimeSearch"]')

    searchForm.on("submit", function (e) {
        console.log("adfsdsfds");
        e.preventDefault()

        var userName = userNameSearch.val()
        var ipAddress = ipAddressSearch.val()
        var lastLoginTime = loginTimeSearch.val()
        var lastlogoutTimeSearc = logoutTimeSearch.val()

        query_userNameSearch.val(userName);
        query_ipAddressSearch.val(ipAddress);
        query_lastLoginTimeSearch.val(lastLoginTime);
        query_logoutTimeSearch.val(lastlogoutTimeSearc);

        query_userNameSearch.trigger('change')
        query_ipAddressSearch.trigger('change')
        query_lastLoginTimeSearch.trigger('change')
        query_logoutTimeSearch.trigger('change')


        searchPosts(userName, ipAddress, lastLoginTime, lastlogoutTimeSearc)
    });
    function searchPosts(userName, ipAddress, lastLoginTime, lastlogoutTimeSearc) {
        getSearch(userName, ipAddress, lastLoginTime, lastlogoutTimeSearc)
    }






});


