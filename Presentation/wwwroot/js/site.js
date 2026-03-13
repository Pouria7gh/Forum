
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

class AppToast {
    toastElement;
    toastCloseBtn;
    titleElement;
    subtitleElement;
    bsToast;
    constructor() {
        this.toastElement = document.getElementById("app-toast");
        this.toastCloseBtn = document.getElementById("app-toast-close-btn");
        this.titleElement = document.getElementById("app-toast-title");
        this.subtitleElement = document.getElementById("app-toast-subtitle");
    }

    static show(title, subtitle) {
        if (!AppToast.instance) {
            AppToast.instance = new AppToast();
        }
        AppToast.instance.showToast(title, subtitle);
    }

    showToast(title, subtitle) {
        this.setToastStyle(title);
        this.setToastContent(title, subtitle);    
        this.createBsToast();
        this.bsToast.show();
        this.addEventListenerToCloseBtn();
    }

    setToastStyle(title) {
        if (title === "Error") {
            this.toastElement.classList.remove("bg-success", "text-light");
            this.toastElement.classList.add("bg-warning", "text-dark");
        } else if (title === "Success") {
            this.toastElement.classList.remove("bg-warning", "text-dark");
            this.toastElement.classList.add("bg-success", "text-light");
        }
    }

    setToastContent(title, subtitle) {
        this.titleElement.innerText = title;
        this.subtitleElement.innerText = subtitle;
    }

    createBsToast() {
        if (this.bsToast) {
            return;
        }
        this.bsToast = new bootstrap.Toast(this.toastElement, {
            autohide: true,
            animation: true,
            delay: 4000,
        });
    }

    addEventListenerToCloseBtn() {
        this.toastCloseBtn.addEventListener("click", () => this.hideToast(this.bsToast))
    }

    hideToast(bsToast) {
        bsToast.hide();
    }
}

// Like & Dislike buttons
$(document).ready(() => {
    $(".btn-like-post").on("click", () => handleInteraction(event, true));
    $(".btn-dislike-post").on("click", () => handleInteraction(event, false));

    function handleInteraction(event, predicate) {
        const button = event.currentTarget;
        const data = {
            forumPostId: button.dataset.forumPostId,
            isLiked: predicate ? true : false,
            isDisliked: predicate ? false : true,
        };

        const antiforgeryToken = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "/ForumPost/AddInteraction",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            headers: {
                "RequestVerificationToken" : antiforgeryToken,
            },
            success: function () {
                if ($(button).hasClass("active-interaction")) {
                    deactivateButton($(button), predicate);
                }
                else {
                    let otherButton = $(`.active-interaction[data-forum-post-id="${data.forumPostId}"]`);
                    deactivateButton(otherButton, !predicate);
                    activateButton($(button), predicate);
                }
            },
            error: function (error) {
                if (error.status === 401) {
                    AppToast.show("Error", `You should sign in to ${predicate ? "like" : "dislike"} the post.`)
                } else {
                    AppToast.show("Error", error.responseText);
                }
            }
        })
    }

    function deactivateButton(element, predicate) {
        const iconClassName = predicate ? "bi-hand-thumbs-up" : "bi-hand-thumbs-down";
        element.removeClass("active-interaction");
        element.children("i").removeClass(iconClassName + "-fill");
        element.children("i").addClass(iconClassName);
        const count = $(element).siblings(".interaction-count").text();
        element.siblings(".interaction-count").text(Number(count) - 1)
    }

    function activateButton(element, predicate) {
        const iconClassName = predicate ? "bi-hand-thumbs-up" : "bi-hand-thumbs-down";
        element.addClass("active-interaction");
        element.children("i").addClass(iconClassName + "-fill");
        element.children("i").removeClass(iconClassName);
        const count = $(element).siblings(".interaction-count").text();
        element.siblings(".interaction-count").text(Number(count) + 1)
    }
});