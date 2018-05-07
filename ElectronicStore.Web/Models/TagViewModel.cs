using ElectronicStore.Fulcrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Web.Models
{
    public class TagViewModel
    {
        public int Id { set; get; }

        public string Alias { set; get; }

        public string Name { set; get; }

        public TagType Type { set; get; }
    }
}