window.scrollElementIntoView = (elementId) => {
    var element = document.getElementById(elementId);

    if (element) {
        var top;
        if (element.offsetTop < element.parentElement.scrollTop || element.clientHeight > element.parentElement.clientHeight) {
            top = element.offsetTop;
        } else if (element.offsetTop + element.offsetHeight > element.parentElement.scrollTop + element.parentElement.clientHeight) {
            top = element.offsetTop + element.offsetHeight - element.parentElement.clientHeight;
        }

        element.parentElement.scrollTo({ top: top, behavior: "smooth" });
    }
}