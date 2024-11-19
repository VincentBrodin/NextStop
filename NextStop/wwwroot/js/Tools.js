window.ForceFocus = (id) => {
    const element = document.getElementById(id);
    if (element == null) {
        return;
    }
    element.focus();
    console.log("Forced focus to " + element)
}