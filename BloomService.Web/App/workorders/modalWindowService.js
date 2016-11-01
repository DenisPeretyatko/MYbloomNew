var modalWindowService = function ($interpolate, commonDataService) {
    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Id}}. {{Image}}</h1><div>{{Description}}</div></div>");
    var basePath = global.BasePath;
    var markersToRemove = [];
    
    this.openLargeMap = function (mapOptions, wo) {
        var staticMapUrl = "https://maps.googleapis.com/maps/api/staticmap";
        staticMapUrl += "?center=" + mapOptions.center.lat() + "," + mapOptions.center.lng();
        staticMapUrl += "&size=640x640";
        staticMapUrl += "&zoom=" + mapOptions.zoom;
        staticMapUrl += "&maptype=satellite";
        for (var i = 0; i < markersToRemove.length; i++) {
            staticMapUrl += "&markers=color:red|label:" + markersToRemove[i].labelContent + "|" + markersToRemove[i].position.lat() + "," + markersToRemove[i].position.lng();
        }
        var a = document.createElement('a');
        a.href = staticMapUrl;
        a.download = wo+"-map.png";
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    };

    this.setContent = function (wo, picture, markers, map) {
        $('#myModal').on('shown.bs.modal', function () {
            google.maps.event.trigger(map, 'resize');
            $("#modalImg").attr('src', basePath + '/images/' + wo + '/' + picture.BigImage);
            $("#modalComment").text(picture.Description);
        });
        $('#myModal').on('hidden.bs.modal', function () {
            angular.forEach(markersToRemove, function (marker, key) {
                marker.setMap(null);
            });
        });
    };

    var changeImage = function (image, description, wo) {
        $("#modalImg").attr('src', basePath + '/images/' + wo + '/' + image);
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
                icon: basePath + "/images/mTemplate.png"
            });
            markersToRemove.push(marker);
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
            marker.addListener('mouseup', function () {
                var model = {
                    WorkOrderId: wo,
                    PictureId: value.Id,
                    Latitude: marker.position.lat(),
                    Longitude: marker.position.lng()
                };
                commonDataService.changeImageLocation(model);
            });
        });
    };
}
modalWindowService.$inject = ["$interpolate", "commonDataService"];