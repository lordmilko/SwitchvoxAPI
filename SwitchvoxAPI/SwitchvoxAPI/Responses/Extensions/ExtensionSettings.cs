using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    public class ExtensionSettings
    {
        [XmlAttribute("extension_length_constraint")]
        public int ExtensionLength { get; set; }

        [XmlAttribute("beep_after_assisted_transfer")]
        public bool BeepAfterAssistedTransfer { get; set; }

        [XmlAttribute("require_strong_user_password")]
        public bool RequireStrongUserPassword { get; set; }

        [XmlAttribute("require_strong_phone_password")]
        public bool RequireStrongPhonePassword { get; set; }

        [XmlAttribute("force_weak_user_password_change")]
        public bool ForceWeakPasswordChange { get; set; }
    }
}
