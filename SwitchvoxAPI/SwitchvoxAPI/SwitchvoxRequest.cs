using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
using Switchvox;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Makes API requests against a Digium Switchvox Phone System.
    /// </summary>
    public class SwitchvoxRequest
    {
        //Methods

        public Extensions Extensions;

        /// <summary>
        /// The address of the phone server API requests will be made against.
        /// </summary>
        public string Server
        {
            get { return server.AbsoluteUri; }
            private set { server = new UriBuilder("https", value).Uri; }
        } 
        private Uri server;

        /// <summary>
        /// Username that will be used to make API requests.
        /// </summary>
        public string Username { get; }

        readonly string password;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequest"/> class, using the "ServerUrl", "Username" and "Password" fields of a web.config file's AppSettings.
        /// </summary>
        public SwitchvoxRequest()
        {
            var url = WebConfigurationManager.AppSettings["ServerUrl"];
            var name = WebConfigurationManager.AppSettings["Username"];
            var pass = WebConfigurationManager.AppSettings["Password"];

            if(url == null)
                throw new InvalidOperationException("Your web.config file does not have a \"ServerUrl\" property in its AppSettings");

            if (name == null)
                throw new InvalidOperationException("Your web.config file does not have a \"Username\" property in its AppSettings");

            if(pass == null)
                throw new InvalidOperationException("Your web.config file does not have a \"Password\" property in its AppSettings");

            Server = url;
            Username = name;
            password = pass;

            Extensions = new Extensions(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequest"/> class.
        /// </summary>
        /// <param name="serverUrl">URL of the Phone Server</param>
        /// <param name="username">Case sensitive username of an account with sufficient permissions to make API Requests. Username can be of an Admin (to access the Admin API) or a User (Phone Extension) (to access the User API).</param>
        /// <param name="password">Password for the username.</param>
        public SwitchvoxRequest(string serverUrl, string username, string password)
        {
            Server = serverUrl;
            Username = username;
            this.password = password;

            Extensions = new Extensions(this);
        }

        /// <summary>
        /// Execute a custom request against the phone system.
        /// </summary>
        /// <param name="xml">Custom generated XML containing a The Switchvox XML API Method to execute. For more information, please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods </param>
        /// <param name="validateResponse">Validate the phone system did not return an error in the SwitchvoxResponse</param>
        /// <returns>A <see cref="T:SwitchvoxAPI.SwitchvoxResponse"/> encapsulating the XML returned by the phone system.</returns>
        internal SwitchvoxResponse Execute(XDocument xml, bool validateResponse = true)
        {
            byte[] xmlRequestBytes = Encoding.ASCII.GetBytes(xml.ToString());

            return ExecuteRequest(xmlRequestBytes, validateResponse);
        }

        /// <summary>
        /// Execute a request against the phone system.
        /// </summary>
        /// <param name="method">The Switchvox XML API Method to execute. For more information, please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods </param>
        /// <param name="validateResponse">Validate the phone system did not return an error in the SwitchvoxResponse</param>
        /// <returns>A <see cref="T:SwitchvoxAPI.SwitchvoxResponse"/> encapsulating the XML returned by the phone system.</returns>
        internal SwitchvoxResponse Execute(RequestMethod method, bool validateResponse = true)
        {
            var xmlRequestBytes = method.ToBytes();

            return ExecuteRequest(xmlRequestBytes, validateResponse);
        }

        private SwitchvoxResponse ExecuteRequest(byte[] xmlRequestBytes, bool validateResponse)
        {
            IgnoreSSLCertificateProblems();

            var request = CreateHttpRequest(xmlRequestBytes);

            SendRequest(request, xmlRequestBytes);

            var response = new SwitchvoxResponse(GetResponse(request));

            if (validateResponse)
                ValidateResponse(response);

            return response;
        }

        private void IgnoreSSLCertificateProblems()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        private HttpWebRequest CreateHttpRequest(byte[] xmlRequestBytes)
        {
            var request = (HttpWebRequest)WebRequest.Create(server + "/xml");
            request.Proxy = null;
            request.Credentials = new NetworkCredential(Username, password);
            request.ContentType = "text/xml; encoding='utf8'";
            request.Method = "POST";
            request.ContentLength = (long)(xmlRequestBytes.Length);

            return request;
        }

        private void SendRequest(HttpWebRequest request, byte[] xmlRequestBytes)
        {
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(xmlRequestBytes, 0, xmlRequestBytes.Length);
            }
        }

        private XDocument GetResponse(HttpWebRequest request)
        {
            XDocument doc;

            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var tempDoc = new XmlDocument();
                    tempDoc.Load(response.GetResponseStream());

                    doc = XDocument.Parse(tempDoc.InnerXml);
                }

            }
            catch (WebException ex)
            {
                using (var response = (HttpWebResponse)ex.Response)
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                        throw new UnauthorizedRequestException("The Phone Server refused your request, either due to an ACL restriction or bad username and password.", ex);
                    throw;
                }
            }

            return doc;
        }

        private void ValidateResponse(SwitchvoxResponse response)
        {
            if (response.GetElements("errors").Any())
                throw new SwitchvoxRequestException(response.GetAttribute("error", "message"));
        }
    }
}