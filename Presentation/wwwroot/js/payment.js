document.addEventListener('DOMContentLoaded', function () {
    let timeLeft = 15 * 60; 
    const countdownElement = document.getElementById('countdown');
    let timerInterval;

    function updateCountdown() {
        if (!countdownElement) return;

        const minutes = Math.floor(timeLeft / 60);
        const seconds = timeLeft % 60;

        countdownElement.textContent =
            `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;

        if (timeLeft <= 0) {
            clearInterval(timerInterval);
            alert('Thời gian thanh toán đã hết. Vui lòng thực hiện lại giao dịch.');
            window.location.href = '/'; 
            return;
        }

        timeLeft--;
    }

    timerInterval = setInterval(updateCountdown, 1000);
    updateCountdown(); 

    function cancelPayment() {
        if (confirm('Bạn có chắc chắn muốn hủy thanh toán?')) {
            alert('Thanh toán đã được hủy.');
            window.history.back();
        }
    }

    const cancelButton = document.getElementById('cancelButton');
    if (cancelButton) {
        cancelButton.addEventListener('click', cancelPayment);
    }



    function generateQRPlaceholder() {
        const qrPlaceholder = document.querySelector('.qr-placeholder');
        if (!qrPlaceholder) return;

        qrPlaceholder.innerHTML = `
        <div style="text-align: center;">
            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVSI6bYOpnK7OBoSg8yuoodPKXlPLlovjmUg&s"
                 alt="" 
                 style="width: 150px; height: 150px; display: block; margin: 0 auto 0.5rem;" />
            <div style="font-size: 1rem; font-weight: 600;"></div>
        </div>`;
    }
    generateQRPlaceholder();


});