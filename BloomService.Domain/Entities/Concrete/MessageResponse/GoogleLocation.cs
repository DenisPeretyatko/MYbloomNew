using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomService.Domain.Entities.Concrete.MessageResponse
{
        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class GeocodeResponse
        {

            private string statusField;

            private GeocodeResponseResult[] resultField;

            /// <remarks/>
            public string status
            {
                get
                {
                    return statusField;
                }
                set
                {
                statusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("result")]
            public GeocodeResponseResult[] result
            {
                get
                {
                    return resultField;
                }
                set
                {
                resultField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResult
        {

            private string[] typeField;

            private string formatted_addressField;

            private GeocodeResponseResultAddress_component[] address_componentField;

            private GeocodeResponseResultGeometry geometryField;

            private bool partial_matchField;

            private string place_idField;

            private string[] textField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("type")]
            public string[] type
            {
                get
                {
                    return typeField;
                }
                set
                {
                typeField = value;
                }
            }

            /// <remarks/>
            public string formatted_address
            {
                get
                {
                    return formatted_addressField;
                }
                set
                {
                formatted_addressField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("address_component")]
            public GeocodeResponseResultAddress_component[] address_component
            {
                get
                {
                    return address_componentField;
                }
                set
                {
                address_componentField = value;
                }
            }

            /// <remarks/>
            public GeocodeResponseResultGeometry geometry
            {
                get
                {
                    return geometryField;
                }
                set
                {
                geometryField = value;
                }
            }

            /// <remarks/>
            public bool partial_match
            {
                get
                {
                    return partial_matchField;
                }
                set
                {
                partial_matchField = value;
                }
            }

            /// <remarks/>
            public string place_id
            {
                get
                {
                    return place_idField;
                }
                set
                {
                place_idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return textField;
                }
                set
                {
                textField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultAddress_component
        {

            private string long_nameField;

            private string short_nameField;

            private string[] typeField;

            /// <remarks/>
            public string long_name
            {
                get
                {
                    return long_nameField;
                }
                set
                {
                long_nameField = value;
                }
            }

            /// <remarks/>
            public string short_name
            {
                get
                {
                    return short_nameField;
                }
                set
                {
                short_nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("type")]
            public string[] type
            {
                get
                {
                    return typeField;
                }
                set
                {
                typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometry
        {

            private GeocodeResponseResultGeometryLocation locationField;

            private string location_typeField;

            private GeocodeResponseResultGeometryViewport viewportField;

            private GeocodeResponseResultGeometryBounds boundsField;

            /// <remarks/>
            public GeocodeResponseResultGeometryLocation location
            {
                get
                {
                    return locationField;
                }
                set
                {
                locationField = value;
                }
            }

            /// <remarks/>
            public string location_type
            {
                get
                {
                    return location_typeField;
                }
                set
                {
                location_typeField = value;
                }
            }

            /// <remarks/>
            public GeocodeResponseResultGeometryViewport viewport
            {
                get
                {
                    return viewportField;
                }
                set
                {
                viewportField = value;
                }
            }

            /// <remarks/>
            public GeocodeResponseResultGeometryBounds bounds
            {
                get
                {
                    return boundsField;
                }
                set
                {
                boundsField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryLocation
        {

            private decimal latField;

            private decimal lngField;

            /// <remarks/>
            public decimal lat
            {
                get
                {
                    return latField;
                }
                set
                {
                latField = value;
                }
            }

            /// <remarks/>
            public decimal lng
            {
                get
                {
                    return lngField;
                }
                set
                {
                lngField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryViewport
        {

            private GeocodeResponseResultGeometryViewportSouthwest southwestField;

            private GeocodeResponseResultGeometryViewportNortheast northeastField;

            /// <remarks/>
            public GeocodeResponseResultGeometryViewportSouthwest southwest
            {
                get
                {
                    return southwestField;
                }
                set
                {
                southwestField = value;
                }
            }

            /// <remarks/>
            public GeocodeResponseResultGeometryViewportNortheast northeast
            {
                get
                {
                    return northeastField;
                }
                set
                {
                northeastField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryViewportSouthwest
        {

            private decimal latField;

            private decimal lngField;

            /// <remarks/>
            public decimal lat
            {
                get
                {
                    return latField;
                }
                set
                {
                latField = value;
                }
            }

            /// <remarks/>
            public decimal lng
            {
                get
                {
                    return lngField;
                }
                set
                {
                lngField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryViewportNortheast
        {

            private decimal latField;

            private decimal lngField;

            /// <remarks/>
            public decimal lat
            {
                get
                {
                    return latField;
                }
                set
                {
                latField = value;
                }
            }

            /// <remarks/>
            public decimal lng
            {
                get
                {
                    return lngField;
                }
                set
                {
                lngField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryBounds
        {

            private GeocodeResponseResultGeometryBoundsSouthwest southwestField;

            private GeocodeResponseResultGeometryBoundsNortheast northeastField;

            /// <remarks/>
            public GeocodeResponseResultGeometryBoundsSouthwest southwest
            {
                get
                {
                    return southwestField;
                }
                set
                {
                southwestField = value;
                }
            }

            /// <remarks/>
            public GeocodeResponseResultGeometryBoundsNortheast northeast
            {
                get
                {
                    return northeastField;
                }
                set
                {
                northeastField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryBoundsSouthwest
        {

            private decimal latField;

            private decimal lngField;

            /// <remarks/>
            public decimal lat
            {
                get
                {
                    return latField;
                }
                set
                {
                latField = value;
                }
            }

            /// <remarks/>
            public decimal lng
            {
                get
                {
                    return lngField;
                }
                set
                {
                lngField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class GeocodeResponseResultGeometryBoundsNortheast
        {

            private decimal latField;

            private decimal lngField;

            /// <remarks/>
            public decimal lat
            {
                get
                {
                    return latField;
                }
                set
                {
                latField = value;
                }
            }

            /// <remarks/>
            public decimal lng
            {
                get
                {
                    return lngField;
                }
                set
                {
                lngField = value;
                }
            }
        }
}
