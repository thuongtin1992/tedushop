using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public IEnumerable<ApplicationRoleViewModel> Roles { get; set; }
    }
}