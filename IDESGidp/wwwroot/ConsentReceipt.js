var toSave = document.getElementById("SaveReceipt").innerText;
var toSaveBlob = new Blob([JSON.stringify(toSave, null, 2)], { type: "application/json" });
var a = document.getElementById("jsonDL");
a.href = URL.createObjectURL(toSaveBlob);
function crPrint() {
    window.print();
}
function crBack() {
    window.history.back();
}