using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Encapsulates properties pertaining to an extension on a phone system.
    /// </summary>
    public class Extension
    {
        [XmlAttribute("server_uuid")]
        public string ServerUUID { get; set; }

        /// <summary>
        /// The number of the extension.
        /// </summary>
        [XmlAttribute("number")]
        public string Number { get; set; }

        [XmlAttribute("status")]
        public int Status { get; set; }

        /// <summary>
        /// The extension's Account ID.
        /// </summary>
        [XmlAttribute("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// The Display Name that has been set for the extension (if the extension belongs to a user, this is typically their name)
        /// </summary>
        [XmlAttribute("display")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Date the extension was created.
        /// </summary>
        public DateTime DateCreated => DateTime.Parse(_RawDateCreated);

        [XmlAttribute("type")]
        public ExtensionType Type { get; set; }

        [XmlAttribute("type_display")]
        public string TypeDisplay { get; set; }

        [XmlAttribute("first_name")]
        public string FirstName { get; set; }

        [XmlAttribute("last_name")]
        public string LastName { get; set; }

        [XmlAttribute("email_address")]
        public string EmailAddress { get; set; }

        [XmlAttribute("template_id")]
        public int TemplateId { get; set; }

        private string templateName;

        [XmlAttribute("template_name")]
        public string TemplateName
        {
            get { return templateName; }
            set { templateName = value == string.Empty ? null : value; }
        }

        [XmlAttribute("phone_password_score")]
        public int PhonePasswordScore { get; set; }

        [XmlAttribute("voicemail_password_score")]
        public int VoicemailPasswordScore { get; set; }

        [XmlAttribute("lang_locale")]
        public string Locale { get; set; }

        private string title;

        [XmlAttribute("title")]
        public string Title
        {
            get { return title; }
            set { title = value == string.Empty ? null : value; }
        }

        private string location;

        [XmlAttribute("location")]
        public string Location
        {
            get { return location; }
            set { location = value == string.Empty ? null : value; }
        }

        [XmlAttribute("profile_image_id")]
        public int ProfileImageId { get; set; }

        [XmlAttribute("profile_image_link")]
        public string ProfileImageUrl { get; set; }

        [XmlAttribute("date_created")]
        public string _RawDateCreated { get; set; }
    }
}
