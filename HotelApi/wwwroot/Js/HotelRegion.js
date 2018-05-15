document.getElementById("SeeAllRegion-Submit").addEventListener("click", function() {
    fetch("http://localhost:30763/api/hotelregion/")
        .then(result => result.json()) 
    .catch(error => console.error('Error:', error))
        .then(function (result) {
            for (var hotelregion of result)
                console.log(hotelregion);
        })
});