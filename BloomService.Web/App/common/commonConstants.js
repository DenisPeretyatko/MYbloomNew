var flotChartOptions =
{
    series: {
        bars: {
            show: true,
            barWidth: 0.6,
            align: "center",
            fill: true,
            fillColor: {
                colors: [
                    {
                        opacity: 0.8
                    },
                    {
                        opacity: 0.8
                    }
                ]
            }
        }
    },
    xaxis: {
        mode: 'categories',
        tickLength: 0,
        tickDecimals: 0
    },
    colors: ["#1ab394"],
    grid: {
        color: "#999999",
        hoverable: true,
        clickable: true,
        tickColor: "#D4D4D4",
        borderWidth: 0
    },
    legend: {
        show: false
    }
};

var googleMapStyles = [
    {   
        "featureType":"water",
        "stylers": [{"saturation":43},{"lightness":-11},{"hue":"#0088ff"}]
    },
    {
        "featureType":"road",
        "elementType":"geometry.fill",
        "stylers":[{"hue":"#ff0000"},{"saturation":-100},{"lightness":99}]
    },
    {
        "featureType":"road",
        "elementType":"geometry.stroke",
        "stylers":[{"color":"#808080"},{"lightness":54}]
    },
    {
        "featureType":"landscape.man_made",
        "elementType":"geometry.fill",
        "stylers":[{"color":"#ece2d9"}]
    },
    {   
        "featureType":"poi.park",
        "elementType":"geometry.fill",
        "stylers":[{"color":"#ccdca1"}]
    },
    {   
        "featureType":"road",
        "elementType":"labels.text.fill",
        "stylers":[{"color":"#767676"}]
    },
    {
        "featureType":"road",
        "elementType":"labels.text.stroke",
        "stylers":[{"color":"#ffffff"}]
    },
    {
        "featureType":"poi",
        "stylers":[{"visibility":"off"}]
    },
    {
        "featureType":"landscape.natural",
        "elementType":"geometry.fill",
        "stylers":[{"visibility":"on"},{"color":"#b8cb93"}]
    },
    {
        "featureType":"poi.park",
        "stylers":[{"visibility":"on"}]
    },
    {   
        "featureType":"poi.sports_complex",
        "stylers":[{"visibility":"on"}]
    },
    {
        "featureType":"poi.medical",
        "stylers":[{"visibility":"on"}]
    },
    {
        "featureType":"poi.business",
        "stylers":[{"visibility":"simplified"}]
    }
];

var googleMapOptions = {
    zoom: 10,
    center: new google.maps.LatLng(42.499307, -83.699596),
    styles: googleMapStyles,
    mapTypeId: google.maps.MapTypeId.ROADMAP
};