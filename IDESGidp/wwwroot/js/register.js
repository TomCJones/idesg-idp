var errorMap = {
    1: 'Unknown error, try again',
    2: "Bad request error, try again",
    3: "This key isn't supported, please try another one",
    4: 'The device is already registered, please login',
    5: 'Authentication timed out. Please reload to try again.'
};

setTimeout(function () {
            $('#promptModal').modal('show');
            window.u2f.register(
                "@Model.AppId", [{
                    challenge: "@Model.Challenge",
                    version: "@Model.Version",
                    appId: "@Model.AppId"
                }],
                [{}],
                function (data) {
                    if (data.errorCode) {
                        $('#promptModal').modal('hide');
                        return alert(errorMap[data.errorCode]);
                    }
                    console.log("Register callback", data);
                    $('#promptModal').modal('hide');
                    $('#DeviceResponse').val(JSON.stringify(data));
                    $('#registerForm').submit();

                    return false;
                });
        }, 1000);
