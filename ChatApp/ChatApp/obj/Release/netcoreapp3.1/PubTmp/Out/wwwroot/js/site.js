var createRoomBtn = document.getElementById('create-room');
var createRoomModal = document.getElementById('create-room-modal');

createRoomBtn.addEventListener('click', function () {

    createRoomModal.classList.add('active');

});
var closeModal = function () {
    createRoomModal.classList.remove('active')
}