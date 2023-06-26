const accentСolor = "#141414";

function loginSwitcher() {
    let loginSwitchBlock = document.getElementById("login-switch");
    let registerSwitchBlock = document.getElementById("register-switch");
    let extraPasswordLabel = document.getElementById("password-again-label");
    let extraPasswordInput= document.getElementById("password-again-input");
    let submitButton = document.getElementById("submit");
    loginSwitchBlock.style.backgroundColor = accentСolor;
    loginSwitchBlock.style.color = "white";
    registerSwitchBlock.style.backgroundColor = "white";
    registerSwitchBlock.style.color = "black";
    extraPasswordInput.style.display = "none";
    extraPasswordLabel.style.display = "none";
    submitButton.innerHTML = loginSwitchBlock.childNodes[0].innerHTML;
}
function registerSwitcher() {
    let loginSwitchBlock = document.getElementById("login-switch");
    let registerSwitchBlock = document.getElementById("register-switch");
    let extraPasswordLabel = document.getElementById("password-again-label");
    let extraPasswordInput= document.getElementById("password-again-input");
    let submitButton = document.getElementById("submit");
    registerSwitchBlock.style.backgroundColor = accentСolor;
    registerSwitchBlock.style.color = "white";
    loginSwitchBlock.style.backgroundColor = "white";
    loginSwitchBlock.style.color = "black";
    extraPasswordInput.style.display = "block";
    extraPasswordLabel.style.display = "inline";
    submitButton.innerHTML = registerSwitchBlock.childNodes[0].innerHTML;
}