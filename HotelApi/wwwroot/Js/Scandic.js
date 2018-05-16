
function errorLogger(result) {
    console.error("Error: ", result);
    console.log(result.status);
    console.log(result.statusText);
    console.log(result.error);
}

document.getElementById("hotelAdder-submit").addEventListener("click", sendToDatabase);

function fillHotelRegions() {

    fetch("/api/hotelregion/")
        .then(result => {
            console.log(result);
            return result.json();
        })
        .catch(result => console.error("Error: ", result))
        .then(function (result) {

            var context = document.getElementById("hotelRegions");
            console.log(context);
            for (var region of result) {
                var tr = document.createElement("tr");

                var element = document.createElement("td");
                element.innerText = region.name;
                tr.appendChild(element);
                element = document.createElement("td");
                element.innerText = region.id;
                tr.appendChild(element);

                context.appendChild(tr);

            }
        });

}

function sendToDatabase() {
    var chain = document.getElementById("hotelChain").value;

    var hotel = {
        name: document.getElementById("hotelAdder-name").value,
        hotelRegionId: document.getElementById("hotelAdder-region").value,
        roomsAvailable: document.getElementById("hotelAdder-roomsAvailable").value,
    };

    if (!hotel.name.includes(chain)) {
        hotel.name = chain + " " + hotel.name;
    }

    console.log(JSON.stringify(hotel));

    var data = new FormData();
    data.append("json", JSON.stringify(hotel));


    //console.log(data);
    //return;

    fetch("/api/hotels/",
        {
            body: JSON.stringify(hotel),
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(result => {
            console.log(result);
            if (result.status !== 200)
                errorLogger(result);
            return result.json();
        })
        .catch(result => console.error("Error: ", result))
        .then(result => {
            alert(result);
        });

}

fillHotelRegions();


