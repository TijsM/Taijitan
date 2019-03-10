// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//import swal from 'sweetalert'

/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown menu if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

function confirmLogout() {
    swal({
        title: "Ben je zeker dat je wilt uitloggen?",
        //text: "Once deleted, you will not be able to recover this imaginary file!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Je bent succesvol uitgelogd!", {
                    icon: "success",
                }).then((value) => {
                        document.logoutForm.submit();
                });

            }
        });
}

function confirmDelete(id) {
    swal({
        title: "Ben je zeker dat je dit lid wilt verwijderen?",
        text: "Dit lid wordt permanent verwijderd",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Het lid is succesvol verwijderd!", {
                    icon: "success",
                });
                Delete(id, name);
            }
        });
}

function Summary() {
    $.ajax({
        url: '/User/Summary'
    });
}
function Delete(id) {
    $.ajax({
        url: '/User/Delete',
        type: 'POST',
        data: { id: id },
        success: function () {
            var member = document.getElementById(id);
            member.parentNode.removeChild(member);
        }
    });
}
function logout() {
    document.logoutForm.submit();
}

function exportTableToExcel(tableID, filename = '') {

    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableID);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');
    // Specify file name
    filename = filename ? filename + '.xls' : 'excel_data.xls';

    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType
        });
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        // Setting the file name    
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }
}