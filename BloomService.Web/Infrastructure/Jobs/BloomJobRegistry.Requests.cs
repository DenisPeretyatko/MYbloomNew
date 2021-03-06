﻿using System;
using System.Net;

namespace BloomService.Web.Infrastructure.Jobs
{
    public partial class BloomJobRegistry
    {
        public void SendRequest()
        {
            //Send request
            Schedule(() =>
            {
                lock (_keepAlive)
                {
                    try
                    {
                        WebRequest req = WebRequest.Create(_settings.SiteUrl);
                        WebResponse result = req.GetResponse();
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Keep alive job failed {0}", ex);
                    }

                }
            }).ToRunNow().AndEvery(10).Minutes();
        }
    }
}