
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

    var data = {};
    data.name = document.getElementById("hotelAdder-name").value;
    data.hotelregionid = document.getElementById("hotelAdder-region").value;
    data.roomsavailable = document.getElementById("hotelAdder-roomsAvailable").value;

    if (!data.name.includes(chain))
        data.name = chain + " " + data.name;

    //console.log(JSON.stringify(data));
    //return;

    fetch("/api/hotels/",
            {
                body: JSON.stringify(data),
                method: "POST"
            })
        .then(result => result.json())
        .catch(result => console.error("Error: ", result));

}

fillHotelRegions();


