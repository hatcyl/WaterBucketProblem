//http://stackoverflow.com/questions/12286332/twitter-bootstrap-remote-modal-shows-same-content-everytime
//This fix is needed so that the bootstrap modal refreshes every time you open it.
$('body').on('hidden.bs.modal', '.modal', function () {
    $(this).removeData('bs.modal');
});
