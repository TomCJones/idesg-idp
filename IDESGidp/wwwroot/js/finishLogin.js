var errorMap = {
    1: 'Unknown error, try again',
    2: "Bad request error, try again",
    3: "This key isn't supported, please try another one",
    4: 'The device is already registered, please login',
    5: 'Authentication timed out. Please reload to try again.'
};
$('#promptModal').modal('show');
    setTimeout(function () {
        window.u2f.sign(
            "@Model.AppId",
            "@Model.Challenge",
             @Html.Raw(@Model.Challenges),
            function (data) {
                console.log("call back data: ", data);
                alert("inside sign");
                if (data.errorCode) {
                    $('#promptModal').modal('hide');
                    return alert(errorMap[data.errorCode]);
                }
                $('#promptModal').modal('hide');
                $('#DeviceResponse').val(JSON.stringify(data));
                $('#loginForm').submit();

                return "";
             });
        }, 1000);
