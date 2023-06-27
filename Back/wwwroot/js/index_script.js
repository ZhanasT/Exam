const timeTableRequestURL = 'https://localhost:7274/api/Cinema/GetTimeTable';

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
        document.getElementById("login").innerHTML = sessionStorage.getItem("login");
        document.getElementById("login").href = "./account.html"
    }
}


async function getResource(url) {
    const res = await fetch(`${url}`);

    if (!res.ok) {
    throw new Error(`Could not fetch ${url}, status: ${res.status}`);
    }

     return await res.json();
}

function showTimeTable(day) {
    getData();
    getResource(`${timeTableRequestURL}?day=${day}`)
        .then(data => buildTimeTables(data));
    let dayHeaders = ['Сеансы на сегодня','Сеансы на завтра','Сеансы на послезавтра'];
    document.getElementById("sessions-header").innerHTML = dayHeaders[day - 1];
}

function buildTimeTables(data) {
    if (document.getElementById("time-table-by-films") != null && document.getElementById("time-table-by-halls") != null) {
        document.getElementById("time-table-by-films").remove();
        document.getElementById("time-table-by-halls").remove();
    }
    //By films
    let tableElementByFilms = document.createElement('table');
    let containerByFilms = document.getElementById("sessions-by-films");
    tableElementByFilms.id = `time-table-by-films`;
    tableElementByFilms.style.cssText = `width: 60%;
                                        margin: 0 auto;`;
    let tableHeaderArr = ['Название фильма','Начало сеанса','Номер зала','Цена'];
    let headerRow = document.createElement('tr');
    tableHeaderArr.forEach(value => {
        let headerCell = document.createElement('th');
        if (value != 'Название фильма')
            headerCell.style.textAlign = 'left';
        headerCell.innerHTML = value;
        headerRow.appendChild(headerCell);
    });
    tableElementByFilms.appendChild(headerRow);
    
    customSort(data, ["movieId"]);
    for (let sId = 0; sId < data.length; sId++) {
        let row = document.createElement('tr');
        for (let cellIndex = 0; cellIndex < 4; cellIndex++) {
            let cell = document.createElement('td');
            row.appendChild(cell);
        }
        let linkElement = document.createElement('a');
        linkElement.href = `./movie.html?id=${data[sId].movieId}`;
        linkElement.innerHTML = data[sId].movieTitle;
        linkElement.style.color = "black";
        row.childNodes[0].appendChild(linkElement);
        row.childNodes[1].innerHTML = `${data[sId].startTime[0]}${data[sId].startTime[1]}:${data[sId].startTime[3]}${data[sId].startTime[4]}`;
        row.childNodes[2].innerHTML = data[sId].hallNumber;
        row.childNodes[3].innerHTML = data[sId].price;
        tableElementByFilms.appendChild(row);
    }
    containerByFilms.appendChild(tableElementByFilms);

    //By Halls
    
    customSort(data, ["hallNumber"]);
    let tableElementByHalls = document.createElement('table');
    let containerByHalls = document.getElementById("sessions-by-halls");
    tableElementByHalls.id = `time-table-by-halls`;
    tableElementByHalls.style.cssText = `width: 60%;
                                        margin: 0 auto;`;
    tableElementByHalls.appendChild(headerRow.cloneNode(true));
    

    for (let sId = 0; sId < data.length; sId++) {
        let row = document.createElement('tr');
        for (let cellIndex = 0; cellIndex < 4; cellIndex++) {
            let cell = document.createElement('td');
            row.appendChild(cell);
        }
        let linkElement = document.createElement('a');
        linkElement.href = `./movie.html?id=${data[sId].movieId}`;
        linkElement.innerHTML = data[sId].movieTitle;
        linkElement.style.color = "black";
        row.childNodes[0].appendChild(linkElement);
        row.childNodes[1].innerHTML = `${data[sId].startTime[0]}${data[sId].startTime[1]}:${data[sId].startTime[3]}${data[sId].startTime[4]}`;
        row.childNodes[2].innerHTML = data[sId].hallNumber;
        row.childNodes[3].innerHTML = data[sId].price;
        tableElementByHalls.appendChild(row);
    }
    containerByHalls.appendChild(tableElementByHalls);


}

function customSort(arr, prop) {
    arr.sort(function(x, y) {
        if (x[prop] == y[prop]) return 0;
        else if (x[prop] > y[prop]) return 1;
        else return -1;
    }) 
}