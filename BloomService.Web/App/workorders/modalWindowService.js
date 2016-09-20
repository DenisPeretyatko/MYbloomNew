var modalWindowService = function ($interpolate) {
    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Id}}. {{Image}}</h1><div>{{Description}}</div></div>");

    this.setContent = function (wo, picture, markers, map) {
        $('#myModal').on('shown.bs.modal', function () {
            google.maps.event.trigger(map, 'resize');
            $("#modalImg").attr('src', 'public/images/' + wo + '/' + picture.BigImage);
            $("#modalComment").text(picture.Description);
        });
        $('#myModal').on('hidden.bs.modal', function () {
            angular.forEach(markers, function (marker, key) {
                marker.setMap(null);
            });
        });
    };
    var changeImage = function (image, description, wo) {
        $("#modalImg").attr('src', 'public/images/' + wo + '/' + image);
        $("#modalComment").text(description);
    };
    this.setMarkers = function (images, map, wo) {
        angular.forEach(images, function (value, key) {
            var content = tooltip(value);
            var pos = {
                lat: parseFloat(value.Latitude),
                lng: parseFloat(value.Longitude)
            }

            var marker = new MarkerWithLabel({
                position: pos,
                map: map,
                draggable: true,
                raiseOnDrag: true,
                labelContent: value.Id.toString(),
                labelClass: "map-labels",
                labelInBackground: false,
                icon: "/public/images/mTemplate.png"
            });
            var infowindow = new google.maps.InfoWindow({
                content: content
            });
            marker.addListener('mouseover', function () {
                infowindow.open(map, marker);
            });
            marker.addListener('mouseout', function () {
                infowindow.close();
            });
            marker.addListener('click', function () {
                changeImage(value.BigImage, value.Description, wo);
            });
        });
    };
}
modalWindowService.$inject = ["$interpolate"];