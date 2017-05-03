using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class ExtensionGroupDependent
    {
        [XmlAttribute("type")]
        public object Type { get; set; } //could this be currentcallstate? an extension type? and separately from this,
        //dont we need to add an extensiontype for all the different types of extensions we have. what if we make a bunch of feature extensions
        //and then do extensions.getlist()

        //we should update our readme file on github to list all the methods we currently support

        //we should implement systemclock.getinfo and find a way to convert the current timezone to some sort of object like timezoneinfo?

        [XmlAttribute("display_name")]
        public string DisplayName { get; set; }
    }
}
