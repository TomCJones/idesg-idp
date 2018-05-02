var errorMap = {
    1: 'Unknown error, try again',
    2: "Bad request error, try again",
    3: "This key isn't supported, please try another one",
    4: 'The device is already registered, please login',
    5: 'Authentication timed out. Please reload to try again.'
};
btn = document.getElementById("BindU2F");
btn.onclick = function () {
    setTimeout(function () {
        var jsonObj = null;
        $('#promptModal').modal('show');
        $.post("/Manage/GetChallenge").
            done(function (data) {
                console.log("Binding input", data);
                jsonObj = JSON.parse(data)
                window.u2f.register(jsonObj.AppId, [{
                    challenge: jsonObj.Challenge,
                    version: jsonObj.Version
                }], [], function (data) {
                    console.log("Binding callback", data);
                    if (data.errorCode) {
                        $('#promptModal').modal('hide');
                        return alert(errorMap[data.errorCode]);
                    }
                    $('#promptModal').modal('hide');
                    $('#TokenResponse').val(JSON.stringify(data));
                    $('#bindForm').submit();

                    return false;
                });
            });
    }, 1000);
}
