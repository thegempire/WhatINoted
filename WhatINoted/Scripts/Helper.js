function ToggleElementHidden(className) {
    document.getElementsByClassName(className)[0].classList.toggle("hidden");
}

function callCSMethod(methodPath, paramsJson, successFunction) {
    $.ajax({
        type: "POST",
        url: methodPath,
        data: paramsJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunction
    });
}