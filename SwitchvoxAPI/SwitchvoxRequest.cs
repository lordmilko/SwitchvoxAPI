using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Net;
using System.Net.Security;
using System.Web.Configuration;

using Switchvox;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Specifies the type of identifier being used to represent an Extension.
    /// </summary>
    public enum ExtensionIdentifier
    {
        /// <summary>
        /// An Extension Number.
        /// </summary>
        Extension,

        /// <summary>
        /// The Account ID associated with an Extension Number.
        /// </summary>
        AccountID
    }

    /// <summary>
    /// The order in which the returned elements should be sorted.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Sort in ascending order.
        /// </summary>
        Asc,

        /// <summary>
        /// Sort in descending order.
        /// </summary>
        Desc
    }

    /// <summary>
    /// Perform XML Requests against a Digium Switchvox Phone System
    /// </summary>
    public class SwitchvoxRequest
    {
        readonly string serverUrl;
        readonly string username;
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

            serverUrl = url;
            username = name;
            password = pass;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequest"/> class.
        /// </summary>
        /// <param name="serverUrl">URL of the Phone Server</param>
        /// <param name="username">Username of an account with sufficient permissions to make API Requests. Username can be of an Admin (to access the Admin API) or a User (Phone Extension) (to access the User API)</param>
        /// <param name="password">Password for the username.</param>
        public SwitchvoxRequest(string serverUrl, string username, string password)
        {
            this.serverUrl = serverUrl;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Execute a request against the Phone System
        /// </summary>
        /// <param name="method">The Switchvox XML API Method to execute. For more information, please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods </param>
        /// <param name="validateResponse">Validate the phone system did not return an error in the SwitchvoxResponse</param>
        /// <returns>A <see cref="T:SwitchvoxAPI.SwitchvoxResponse"/> encapsulating the XML returned by the phone system.</returns>
        public SwitchvoxResponse Execute(RequestMethod method, bool validateResponse = true)
        {
            IgnoreSSLCertificateProblems();

            var xmlRequestBytes = method.ToBytes();

            var request = CreateHttpRequest(xmlRequestBytes);

            SendRequest(request, xmlRequestBytes);

            var response = new SwitchvoxResponse(GetResponse(request));

            if (validateResponse)
                ValidateResponse(response);

            return response;
        }

        private void IgnoreSSLCertificateProblems()
        {
            var defaultValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
            var customValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, errors) => { return true; }); //Completely ignore the bad certificate on the PBX

            ServicePointManager.ServerCertificateValidationCallback = Delegate.Combine(defaultValidationCallback, customValidationCallback) as RemoteCertificateValidationCallback;
        }

        private HttpWebRequest CreateHttpRequest(byte[] xmlRequestBytes)
        {
            var request = (HttpWebRequest)WebRequest.Create(serverUrl + "/xml");
            request.Proxy = null;
            request.Credentials = new NetworkCredential(username, password);
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
            
            using(var response = (HttpWebResponse)request.GetResponse())
            {
                var tempDoc = new System.Xml.XmlDocument();
                tempDoc.Load(response.GetResponseStream());

                doc = XDocument.Parse(tempDoc.InnerXml);
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