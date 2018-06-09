{
    function printJsonOfPage(elementId) {
        var printContent = document.getElementById(elementId);
        var printWindow = window.open('', '', 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    }
}