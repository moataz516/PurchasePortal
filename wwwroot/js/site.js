document.addEventListener('DOMContentLoaded', function () {
//    setupAddProductModal();
//    setupUpdateProductModal();
    setupShowMsgHandler();
updateFavoriteCount();
updateCartCount();
//    setupFormResetHandlers();
});

//function setupAddProductModal() {
//    const addProductModal = document.getElementById('addProductModal');

//    if (addProductModal) {
//        addProductModal.addEventListener('show.bs.modal', function (event) {
//            fetch('/GetCreateProductForm')
//                .then(response => response.text())
//                .then(html => {
//                    const modalContent = document.querySelector('#addProductModal .modal-content');
//                    modalContent.innerHTML = html;
//                })
//                .catch(error => console.error('Error:', error));
//        });
//    } else {
//        console.error('Element with ID "addProductModal" not found.');
//    }
//}

//function setupUpdateProductModal() {
//    const updateProductModal = document.getElementById('updateProductModal');

//    if (updateProductModal) {
//        updateProductModal.addEventListener('show.bs.modal', function (event) {
//            const button = event.relatedTarget; // Button that triggered the modal
//            const productId = button.getAttribute('data-id');

//            fetch('/GetProductForUpdate/' + productId)
//                .then(response => response.text())
//                .then(html => {
//                    const modalContent = document.querySelector('#updateProductModal .modal-content');
//                    modalContent.innerHTML = html;
//                })
//                .catch(error => console.error('Error:', error));
//        });
//    } else {
//        console.error('Element with ID "updateProductModal" not found.');
//    }
//}

function setupShowMsgHandler() {
    const showMsgDiv = document.getElementById('ShowMsg');
    if (showMsgDiv && showMsgDiv.innerHTML.trim() !== "") {
        setTimeout(() => {
            showMsgDiv.style.display = 'none';
        }, 5000);
    } else if (showMsgDiv) {
        showMsgDiv.innerHTML = "";
    }
}

function updateFavoriteCount() {
    fetch('/GetTotalFavorite')
        .then(response => response.json())
        .then(favoriteCount => {
            document.getElementById('favorite-count').innerText = favoriteCount.count ?? "";
        })
        .catch(error => console.error('Error fetching favorite count:', error));
 // Initial load
}

function updateCartCount() {
    fetch('/GetTotalCart')
        .then(response => response.json())
        .then(cartCount => {
            console.log(cartCount.count)
            document.getElementById('cart-count').innerText = cartCount.count;
        })
        .catch(error => console.error('Error fetching favorite count:', error));
 // Initial load
}


//function setupFormResetHandlers() {
//    const addProductModal = document.getElementById('addProductModal');
//    const addProductForm = document.getElementById('addProductForm');

//    if (addProductModal && addProductForm) {
//        addProductModal.addEventListener('hidden.bs.modal', function () {
//            resetFormAndInfo(addProductForm);
//        });
//    } else {
//        if (!addProductModal) console.error('Element with ID "addProductModal" not found.');
//        if (!addProductForm) console.error('Element with ID "addProductForm" not found.');
//    }

//    function resetFormAndInfo(form) {
//        form.reset();
//        form.classList.remove('was-validated');

//        // Clear validation messages
//        const validationMessages = form.querySelectorAll('.text-danger');
//        validationMessages.forEach(function (message) {
//            message.innerText = '';
//        });

//        // Reset validation summary
//        const validationSummary = form.querySelector('[asp-validation-summary]');
//        if (validationSummary) {
//            validationSummary.innerText = '';
//        }
//    }
//}
