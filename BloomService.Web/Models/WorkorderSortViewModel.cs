using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloomService.Domain.Entities.Concrete;

namespace BloomService.Web.Models
{
	public class WorkorderSortViewModel
	{
	    public int CountPage { get; set; }
	    public IEnumerable<SageWorkOrder> WorkordersList { get; set; }
	}
}