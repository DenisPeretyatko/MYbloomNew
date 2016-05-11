﻿namespace BloomService.Domain.Entities.Concrete.MessageResponse
{
    using System.Xml.Serialization;

    [XmlType(AnonymousType = true)]
    public class ResponseReturnParams
    {
        public ResponseReturnParamsReturnParam ReturnParam { get; set; }

        [XmlText]
        public string[] Text { get; set; }
    }
}