const timeTableRequestURL = 'https://localhost:7274/api/Cinema/GetTimeTable';
const movieInfoRequestURL = 'https://localhost:7274/api/Cinema/GetMovieInfo/'
let movieId = new URL(location.href).searchParams.get("id");

async function getResource(url) {
    const res = await fetch(`${url}`);

    if (!res.ok) {
        throw new Error(`Could not fetch ${url}, status: ${res.status}`);
    }

     return await res.json();
}

function onLoad(day) {
    getResource(`${movieInfoRequestURL}?id=${movieId}`)
        .then(data => fillMovieInfo(data));
    getResource(`${timeTableRequestURL}?day=${day}&id=${movieId}`)
        .then(data => buildTimeTables(data));
}

function showTimeTable(day) {
    getResource(`${timeTableRequestURL}?day=${day}&id=${movieId}`)
        .then(data => buildTimeTables(data));
}

function fillMovieInfo(data) {
    document.getElementById("film-poster").src = `../Images/posters/${data.posterSrc}`;
    document.getElementById("film-name").innerHTML = data.title;
    document.getElementById("film-description").innerHTML = data.description;
    data.genres.forEach(genre => {
        let item = document.createElement('li');
        item.innerHTML = genre;
        document.getElementById("genre-list").appendChild(item);
    });
    document.getElementById("age-limit").innerHTML = `Возрастное ограничение: ${data.ageLimit}+`;
    document.getElementById("duration").innerHTML = `Продолжительность: ${data.duration} минут`;
    document.getElementById("director").innerHTML = `Режиссер: ${data.director}`
    data.actors.forEach(actor => {
        let item = document.createElement('li');
        item.innerHTML = actor;
        document.getElementById("actors-list").appendChild(item);
    });
    document.getElementById("rating").innerHTML = `Оценка: ${data.rating}`;

}


function buildTimeTables(data) {
    if (document.getElementById("time-table-by-films") != null) 
        document.getElementById("time-table-by-films").remove();

    console.log(data);

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
    console.log(data);
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
        row.childNodes[1].innerHTML = data[sId].startTime;
        row.childNodes[2].innerHTML = data[sId].hallNumber;
        row.childNodes[3].innerHTML = data[sId].price;
        tableElementByFilms.appendChild(row);
    }
    containerByFilms.appendChild(tableElementByFilms);
}

function customSort(arr, prop) {
    arr.sort(function(x, y) {
        if (x[prop] == y[prop]) return 0;
        else if (x[prop] > y[prop]) return 1;
        else return -1;
    }) 
}