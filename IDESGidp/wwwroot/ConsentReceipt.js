
function crPrint() {
    window.print();
}
function crSave() {
    var toSave = document.getElementById("json").value;
    var toSaveBlob = new Blob(toSave, { type:"text/plain"});
    saveAs(toSaveBlob, "ConsentReceitp.json");
}
function crBack() {
    window.history.back();
}
