console.log("Hwerwer");
window.showModal = function showModal(modalId) {
    $(`#${modalId}`).modal('show');
}

window.hideModal = function hideModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

window.setModalTitle = function setModalTitle(modalId, title) {
    $(`#${modalId} .modal-title`).text(`${title}`);
}

window.setModalBody = function setModalBody(modalId, bodyId) {
    $(`#${modalId} .modal-body`).html(`${document.getElementById(bodyId).innerHTML}`);
}