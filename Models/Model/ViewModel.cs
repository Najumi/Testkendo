using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testkendo.Models.Model
{
    public class ViewModel
    {
        public spGetItems_Result objgetitem  = new spGetItems_Result();
        public getCategory_Result objgetcat = new getCategory_Result();

        public List<getCategory_Result> getcategory = new List<getCategory_Result>();
        public List<spGetItems_Result> getItem = new List<spGetItems_Result>();
    }
}