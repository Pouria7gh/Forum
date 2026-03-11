
// sets up sites color theme
$(document).ready(function () {
    const darkModeMql = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)');

    if (darkModeMql && darkModeMql.matches) {
        $("#html").attr("data-bs-theme", "dark");
    } else {
        $("#html").attr("data-bs-theme", "light");
    }
});

// admin navbar
$(document).ready(() => {
    $("#sidebar-toggle").on("click", () => {
        const $sidebarWrapper = $("#sidebar-wrapper");
        if (!$sidebarWrapper) {
            return;
        }

        if ($sidebarWrapper.hasClass("hide")) {
            $sidebarWrapper.removeClass("hide");
        } else {
            $sidebarWrapper.addClass("hide");
        }
    });
});

class AppConfirm {
    modalElement;
    titleElement;
    subtitleElement;
    btnConfirm;
    btnCancel;
    currentResolve;
    bsModal;
    constructor() {
        this.modalElement = document.getElementById("app-confirm-modal");
        this.titleElement = document.getElementById("confirm-modal-title");
        this.subtitleElement = document.getElementById("confirm-modal-subtitle");
        this.btnConfirm = document.getElementById("confirm-modal-confirm");
        this.btnCancel = document.getElementById("confirm-modal-cancel");
    }

    static show(title, subTitle) {
        if (!AppConfirm.instance) {
            AppConfirm.instance = new AppConfirm();
        }
        return AppConfirm.instance._showInstance(title, subTitle);
    }

    _showInstance(title, subTitle) {
        return new Promise((resolve) => {
            this.currentResolve = resolve;
            this.attachEventListenerToCancelBtn();
            this.attachEventListenerToConfirmBtn();
            this.configureModal(title, subTitle);
            this.showModal();
        });
    }

    attachEventListenerToCancelBtn() {
        this.btnCancel.addEventListener("click", () => this.handleClick(false))
    }

    attachEventListenerToConfirmBtn() {
        this.btnConfirm.addEventListener("click", () => this.handleClick(true))
    }

    handleClick(value) {
        this.resolve(value);
        this.bsModal.hide();
    }

    resolve(value) {
        if (this.currentResolve) {
            this.currentResolve(value);
            this.currentResolve = null;
        }
    }

    configureModal(title, subTitle) {
        this.titleElement.innerText = title;
        this.subtitleElement.innerText = subTitle;
    }

    showModal() {
        if (!this.bsModal) {
            this.bsModal = new bootstrap.Modal(this.modalElement);
        }
        this.bsModal.show();
    }
}