// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//import swal from 'sweetalert'

/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function openMenu() {
    document.getElementById("menuDropdown").classList.toggle("show");
}
function openNotifications() {
    document.getElementById("notificationsDropdown").classList.toggle("show");
}
function openNotificationsMobile() {
    document.getElementById("notificationsDropdown").classList.toggle("show");
}
// Close the dropdown menu if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtnmenu')) {
        var dropdowns = document.getElementsByClassName("dropdown-contentmenu");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
    if (!event.target.matches('.dropbtnnot')) {
        var dropdowns = document.getElementsByClassName("dropdown-contentnot");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
    if (!event.target.matches('.dropbtnnotMobile')) {
        var dropdowns = document.getElementsByClassName("dropdown-contentnotMobile");
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
        icon: "warning",
        buttons: ["Annuleer", "Log uit"],
        dangerMode: true,
        showConfirmButton: false,
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
        buttons: ["Annuleer", "Verwijder lid"],
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
function confirmDeleteComment(id) {
    swal({
        title: "Ben je zeker dat je dit commentaar wilt verwijderen?",
        text: "Dit commentaar wordt permanent verwijderd",
        icon: "warning",
        buttons: ["Annuleer", "Verwijder commentaar"],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Het commentaar is succesvol verwijderd!", {
                    icon: "success",
                });
                DeleteComment(id, name);
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
function DeleteComment(id) {
    $.ajax({
        url: '/CourseMaterial/RemoveComment',
        type: 'POST',
        data: { id: id },
        success: function () {
            var comment = document.getElementById(id);
            comment.parentNode.removeChild(comment);
        }
    });
}
function logout() {
    document.logoutForm.submit();
}

init();
function init() {
    if (!$.fn.dataTable.isDataTable('#summaryTable')) {
        $(document).ready(function () {
            $('#summaryTable').DataTable({
                "responsive":true,
                "language": {
                    "sProcessing": "Bezig...",
                    "sLengthMenu": "_MENU_ resultaten weergeven",
                    "sZeroRecords": "Geen resultaten gevonden",
                    "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
                    "sInfoEmpty": "Geen resultaten om weer te geven",
                    "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
                    "sInfoPostFix": "",
                    "sSearch": "Zoeken:",
                    "sEmptyTable": "Geen resultaten aanwezig in de tabel",
                    "sInfoThousands": ".",
                    "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
                    "oPaginate": {
                        "sFirst": "Eerste",
                        "sLast": "Laatste",
                        "sNext": "Volgende",
                        "sPrevious": "Vorige"
                    },
                    "oAria": {
                        "sSortAscending": ": activeer om kolom oplopend te sorteren",
                        "sSortDescending": ": activeer om kolom aflopend te sorteren"
                    }
                },
                dom: 'Bflrtip',
                buttons: [
                    'excel', 'print'
                ]
            });
        });
    }
    if (!$.fn.dataTable.isDataTable('#userSummaryTable')) {
        $(document).ready(function () {
            $('#userSummaryTable').DataTable({
                "responsive": true,
                "language": {
                    "sProcessing": "Bezig...",
                    "sLengthMenu": "_MENU_ resultaten weergeven",
                    "sZeroRecords": "Geen resultaten gevonden",
                    "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
                    "sInfoEmpty": "Geen resultaten om weer te geven",
                    "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
                    "sInfoPostFix": "",
                    "sSearch": "Zoeken:",
                    "sEmptyTable": "Geen resultaten aanwezig in de tabel",
                    "sInfoThousands": ".",
                    "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
                    "oPaginate": {
                        "sFirst": "Eerste",
                        "sLast": "Laatste",
                        "sNext": "Volgende",
                        "sPrevious": "Vorige"
                    },
                    "oAria": {
                        "sSortAscending": ": activeer om kolom oplopend te sorteren",
                        "sSortDescending": ": activeer om kolom aflopend te sorteren"
                    }
                },
                "columns": [
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    { "orderable": false, "width": "6%" }
                ]
            });
        });
    }
    if (!$.fn.dataTable.isDataTable('#commentSummaryTable')) {
        $(document).ready(function () {
            $('#commentSummaryTable').DataTable({
                "responsive": true,
                "language": {
                    "sProcessing": "Bezig...",
                    "sLengthMenu": "_MENU_ resultaten weergeven",
                    "sZeroRecords": "Geen resultaten gevonden",
                    "sInfo": "_START_ tot _END_ van _TOTAL_ resultaten",
                    "sInfoEmpty": "Geen resultaten om weer te geven",
                    "sInfoFiltered": " (gefilterd uit _MAX_ resultaten)",
                    "sInfoPostFix": "",
                    "sSearch": "Zoeken:",
                    "sEmptyTable": "Geen resultaten aanwezig in de tabel",
                    "sInfoThousands": ".",
                    "sLoadingRecords": "Een moment geduld aub - bezig met laden...",
                    "oPaginate": {
                        "sFirst": "Eerste",
                        "sLast": "Laatste",
                        "sNext": "Volgende",
                        "sPrevious": "Vorige"
                    },
                    "oAria": {
                        "sSortAscending": ": activeer om kolom oplopend te sorteren",
                        "sSortDescending": ": activeer om kolom aflopend te sorteren"
                    }
                },
                dom: 'Bflrtip',
                buttons: [{
                    extend: 'print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 5]
                    }
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 5]
                    }
                },

                    //,
                    //{
                    //    extend: 'pdfHtml5',
                    //    exportOptions: {
                    //        columns: [0, 1, 2,3, 5]
                    //    }
                    //}
                ], "columns": [
                    null,
                    null,
                    null,
                    null,
                    { "orderable": false, "width": "6%" },
                    null,
                    { "orderable": false, "width": "6%" }
                ]


            });
        });
    }
    
}