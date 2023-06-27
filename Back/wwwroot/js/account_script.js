async function getData() {
    const token = sessionStorage.getItem("accessToken");
    // отправляем запрос к "/data
    const response = await fetch("https://localhost:7274/security/data", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token  // передача токена в заголовке
        }
    });

    if (response.ok === true) {
        const data = await response.json();
        document.getElementById("account-name").innerHTML = sessionStorage.getItem("login");
    }
    else
        console.log("Status: ", response.status);

}

function logout() {
    sessionStorage.clear();
    window.location.href = "index.html";
}