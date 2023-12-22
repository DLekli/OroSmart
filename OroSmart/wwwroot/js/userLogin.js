$(document).ready(function () {
    const getSearch = (userName, ipAddress, lastLoginTime) => {
        var urlParams = new URLSearchParams(window.location.search);
        var pages = urlParams.get('pageNumber');
        const pageNumber = pages
    var params = `?userNameSearch=${userName}&ipAddressSearch=${ipAddress}&lastLoginTimeSearch=${lastLoginTime}&pageNumber=${pages}`
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
    var userNameSearch = $("#userNameSearch_hi")
    var ipAddressSearch = $("#ipAddressSearch_hi")
    var lastLoginTimeSearch = $("#lastLoginTimeSearch_hi")
var searchForm = $("#searchForm")
var searchContainer = $("#tables")


    var query_userNameSearch = $('input[name="userNameSearch"]')
    var query_ipAddressSearch = $('input[name="ipAddressSearch"]')
    var query_lastLoginTimeSearch = $('input[name="lastLoginTimeSearch"]')

searchForm.on("submit", function (e) {
    console.log("adfsdsfds");
    e.preventDefault()

    var userName = userNameSearch.val()
    var ipAddress = ipAddressSearch.val()
    var lastLoginTime = lastLoginTimeSearch.val()

    query_userNameSearch.val(userName);
    query_ipAddressSearch.val(ipAddress);
    query_lastLoginTimeSearch.val(lastLoginTime);

    query_userNameSearch.trigger('change')
    query_ipAddressSearch.trigger('change')
    query_lastLoginTimeSearch.trigger('change')


    searchPosts(userName, ipAddress, lastLoginTime)
});
function searchPosts(userName, ipAddress, lastLoginTime) {
    getSearch(userName, ipAddress, lastLoginTime)
}






});

