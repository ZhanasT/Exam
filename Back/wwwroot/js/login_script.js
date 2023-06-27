const accentСolor = "#141414";
const loginRequestURL = 'https://localhost:7274/Login';

var tokenKey = "accesToken";

function onLoad() {
    document.getElementById("password-again-input").style.display = "none";
}

async function loginOrRegister() {
    if (document.getElementById("password-again-input").style.display != "none")
        await register();
    else
        await login();
}

async function register()
{
    const response = await fetch("https://localhost:7274/security/register", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            username: document.getElementById("username-input").value,
            password: document.getElementById("password-input").value
        })
    });
    if (response.ok == true) {
        alert("Вы зарегестрированы, теперь можете войти в аккаунт");
    } else {
        alert("Не введены обязательные значения");
    } 

}

async function login()
{
    const response = await fetch("https://localhost:7274/security/createToken", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            username: document.getElementById("username-input").value,
            password: document.getElementById("password-input").value
        })
    });
    if (response.ok == true) {
        const data = await response.json();

        sessionStorage.setItem("accessToken", data.access_token);
        sessionStorage.setItem("login", document.getElementById("username-input").value);
        window.location.href = "account.html";
    } else {
        alert("Неправильный логин или пароль");
    } 
}

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