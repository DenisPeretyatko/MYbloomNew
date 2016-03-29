using System;
using System.Web.Mvc;
using Sage.Messaging;
using System.Xml.Serialization;
using System.IO;
using Sage.WebApi.Models.SerializeModels;
using System.Web.Script.Serialization;
using AttributeRouting.Web.Mvc;

namespace Sage.WebApi.Controllers
{
    public class HomeController : Controller
    {
        //IMessageBoard _mb = MessageBoardFactory.CreateMessageBoardFromDefaultCatalogFile();
        private MessageTypeDescriptor _mtd = new MessageTypeDescriptor();
        private IMessageBoard _mb;
        [GET("api/v1/sm/locations")]
        public ActionResult Index()
        {
            try
            {
                _mtd.Xml = "<MessageTypeDescriptor MessageId='SMCommCenterRequest' MustHaveTarget='true'><DataConnection URL='C:\\STOData\\Timberline\\Bloom Roofing Reconstructed'/><LibraryLicense Name='SMCommCenter' ID='C7FC4D2C-BBC3-4a0b-B58A-ED2A9CBDF07F'/><Security UID='kris' Password='sageDEV!!'/></MessageTypeDescriptor>";

                _mb = MessageBoardFactory.CreateMessageBoardFromDefaultCatalogFile();
                
                string catalogPath = "C:\\Documents and Settings\\All Users\\Application Data\\Sage\\Timberline Office\\9.5\\Shared\\Config\\RoutingCatalogs\\SM\\SMCommCenterCatalog.xml";
                _mb.RoutingCatalogPersist.LoadFrom(catalogPath);
                var xmlString = "<Message><Get Entity='Location'><AllProperties/></Get></Message>";
                string sResponse1 = _mb.SendMessage(_mtd.Xml, xmlString);
                var serializer = new XmlSerializer(typeof(MessageResponses));
                object result;
                using (TextReader reader = new StringReader(sResponse1))
                {
                    result = serializer.Deserialize(reader);
                }
                var json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize((result as MessageResponses).MessageResponse.ReturnParams.ReturnParam.Locations);
                Response.Write(json);


            }
            catch (Exception exp)
            {
               
            }
            return View();
        }



    }
}
