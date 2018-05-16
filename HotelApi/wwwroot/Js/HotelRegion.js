document.getElementById("SeeAllRegion-Submit").addEventListener("click",
    function () {
        fetch("/api/hotelregion/")
            .then(result => {
                console.log(result);
                return result.json();
            })
            .catch(error => console.error('Error:', error))
            .then(appendHotelRegionToContent);
    });

document.getElementById("reseedDatabase-submit").addEventListener("click", function() {
    fetch("/api/hotelregion/reseeddatabase",
            {
                method: "DELETE"
            })
        .then(result => alert(result));
});

function appendHotelRegionToContent(result) {

    if (result !== undefined) {


        var content = document.getElementById("content");
        content.innerHTML = "";
        console.log(result);
        for (var hotelregion of result) {
            var div = document.createElement("div");

            var element = document.createElement("h1");
            element.innerText = hotelregion.name;
            div.appendChild(element);

            element = document.createElement("p");
            element.innerText = "Regionsid: " + hotelregion.id;
            div.appendChild(element);

            if (hotelregion.hotels.length > 0) {
                appendHotelTableToElement(hotelregion.hotels, div);

            }

            content.appendChild(div);
        }
    }
}

function appendHotelTableToElement(listOfHotels, div) {
    var table = document.createElement("table");

    var header = document.createElement("thead");

    var element = document.createElement("th");
    element.innerText = "Id";
    header.appendChild(element);

    element = document.createElement("th");
    element.innerText = "Namn";
    header.appendChild(element);

    element = document.createElement("th");
    element.innerText = "Regionsid";
    header.appendChild(element);

    element = document.createElement("th");
    element.innerText = "Lediga rum";
    header.appendChild(element);

    table.appendChild(header);

    var content = document.createElement("tbody");

    for (var hotel of listOfHotels) {
        var tr = document.createElement("tr");

        element = document.createElement("td");
        if (hotel.id)
            element.innerText = hotel.id;
        tr.appendChild(element);

        element = document.createElement("td");
        element.innerText = hotel.name;
        tr.appendChild(element);

        element = document.createElement("td");
        element.innerText = hotel.hotelRegionId;
        tr.appendChild(element);

        element = document.createElement("td");
        element.innerText = hotel.roomsAvailable;
        tr.appendChild(element);

        content.append(tr);
    }

    table.appendChild(content);

    div.appendChild(table);

}

function parseJson(input) {
    input.Json();
}