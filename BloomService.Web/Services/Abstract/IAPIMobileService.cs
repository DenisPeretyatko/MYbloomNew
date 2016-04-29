using BloomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloomService.Web.Services.Abstract
{
    public interface IAPIMobileService
    {
        IEnumerable<SageWorkOrder> GetWorkOreders();
        bool AddImage(IEnumerable<string> images, string idWorkOrder);
    }
}