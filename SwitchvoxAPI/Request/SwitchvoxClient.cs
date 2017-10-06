using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
using SwitchvoxAPI.Deserialization;
using SwitchvoxAPI.Methods;
using Extensions = SwitchvoxAPI.Methods.Extensions;

namespace SwitchvoxAPI
{
    /// <summary>
    /// Makes API requests against a Digium Switchvox Phone System.
    /// </summary>
    public partial class SwitchvoxClient
    {
        //Methods

        /// <summary>
        /// Methods contained in the Switchvox.Extensions namespace.
        /// </summary>
        public Extensions Extensions;

        /// <summary>
        /// Methods contained in the Switchvox.ExtensionGroups namespace.
        /// </summary>
        public ExtensionGroups ExtensionGroups;

        /// <summary>
        /// Methods contained in the Switchvox.Users namespace.
        /// </summary>
        public Users Users;

        /// <summary>
        /// Methods contained in the Switchvox.CallLogs namespace.
        /// </summary>
        public CallLogs CallLogs;

        /// <summary>
        /// Methods contained in the Switchvox.CallQueueLogs namespace.
        /// </summary>
        public CallQueueLogs CallQueueLogs;

        /// <summary>
        /// Methods contained in the Switchvox.CurrentCalls namespace.
        /// </summary>
        public CurrentCalls CurrentCalls;

        /// <summary>
        /// Methods contained in the Switchvox.CallQueues namespace.
        /// </summary>
        public CallQueues CallQueues;

        /// <summary>
        /// Methods contained in the Switchvox.IVR namespace.
        /// </summary>
        public IVR IVR;

        /// <summary>
        /// The address of the phone server API requests will be made against.
        /// </summary>
        public string Server
        {
            get { return server.AbsoluteUri; }
            private set
            {
                if (value.StartsWith("http://") || value.StartsWith("https://"))
                {
                    server = new UriBuilder(value).Uri;
                }
                else
                {
                    server = new UriBuilder("https", value).Uri;
                }
            }
        } 
        private Uri server;

        /// <summary>
        /// UserName that will be used to make API requests.
        /// </summary>
        public string UserName { get; }

        public string Password { get; }

        private static bool ignoreInvalidSSLSet = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchvoxClient"/> class, using the "ServerUrl", "Username" and "Password" fields of a web.config file's AppSettings.
        /// </summary>
        public SwitchvoxClient()
        {
            var url = WebConfigurationManager.AppSettings["ServerUrl"];
            var name = WebConfigurationManager.AppSettings["UserName"];
            var pass = WebConfigurationManager.AppSettings["Password"];

            if(url == null)
                throw new InvalidOperationException("Your web.config file does not have a \"ServerUrl\" property in its AppSettings");

            if (name == null)
                throw new InvalidOperationException("Your web.config file does not have a \"UserName\" property in its AppSettings");

            if(pass == null)
                throw new InvalidOperationException("Your web.config file does not have a \"Password\" property in its AppSettings");

            Server = url;
            UserName = name;
            Password = pass;

            InitializeMethodMembers();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchvoxClient"/> class.
        /// </summary>
        /// <param name="serverUrl">URL of the Phone Server</param>
        /// <param name="username">Case sensitive username of an account with sufficient permissions to make API Requests. Username can be of an Admin (to access the Admin API) or a User (Phone Extension) (to access the User API).</param>
        /// <param name="password">Password for the username.</param>
        public SwitchvoxClient(string serverUrl, string username, string password)
        {
            if (serverUrl == null)
                throw new ArgumentNullException(nameof(serverUrl));

            if (username == null)
                throw new ArgumentNullException(nameof(username));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            Server = serverUrl;
            UserName = username;
            Password = password;

            InitializeMethodMembers();
        }

        private void InitializeMethodMembers()
        {
            Extensions = new Extensions(this);
            ExtensionGroups = new ExtensionGroups(this);
            Users = new Users(this);
            CallLogs = new CallLogs(this);
            CallQueueLogs = new CallQueueLogs(this);
            CurrentCalls = new CurrentCalls(this);
            CallQueues = new CallQueues(this);
            IVR = new IVR(this);
        }

        /// <summary>
        /// Execute and deserialize a request against the phone system.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the XML response from the phone system to.</typeparam>
        /// <param name="method">The Switchvox XML API Method to execute. For more information, please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods</param>
        /// <returns>An object representing the data returned from the phone system.</returns>
        internal T Execute<T>(RequestMethod method) where T : new()
        {
            var response = Execute(method);

            return Deserializer.Deserialize<T>(response);
        }

        internal IEnumerable<T2> Stream<T1, T2>(RequestMethod method, Func<XDocument, string> getTotalItems, Action<RequestMethod, int> setNextPage, int itemsPerPage, Func<T1, List<T2>> getItems) where T1 : new()
        {
            int? totalItems = null;

            var count = itemsPerPage;
            var page = 1;

            for (int i = 0; i < totalItems || totalItems == null;)
            {
                var response = Execute(method);

                var result = Deserializer.Deserialize<T1>(response);

                foreach (var item in getItems(result))
                    yield return item;

                if (totalItems == null)
                    totalItems = Convert.ToInt32(getTotalItems(response));

                i = i + count;
                page++;

                if (totalItems - i < count)
                    count = totalItems.Value - i;

                setNextPage(method, page);
            }
        }

        /// <summary>
        /// Execute a request against the phone system.
        /// </summary>
        /// <param name="method">The Switchvox XML API Method to execute. For more information, please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods</param>
        /// <returns>A <see cref="XDocument"/> containing the XML returned by the phone system.</returns>
        internal XDocument Execute(RequestMethod method)
        {
            var xmlRequestBytes = method.ToBytes();

            return ExecuteRequest(xmlRequestBytes);
        }

        /// <summary>
        /// Execute a custom request against the phone system.
        /// </summary>
        /// <param name="xml">Custom generated XML containing a The Switchvox XML API Method to execute. For more information, please see http://developers.digium.com/switchvox/wiki/index.php/WebService_methods</param>
        /// <returns>A <see cref="XDocument"/> containing the XML returned by the phone system.</returns>
        public XDocument Execute(XDocument xml)
        {
            byte[] xmlRequestBytes = Encoding.ASCII.GetBytes(xml.ToString());

            return ExecuteRequest(xmlRequestBytes);
        }

        private XDocument ExecuteRequest(byte[] xmlRequestBytes)
        {
            IgnoreSSLCertificateProblems();

            var request = CreateHttpRequest(xmlRequestBytes);

            SendRequest(request, xmlRequestBytes);

            //var response = new SwitchvoxResponse(GetResponse(request));
            var response = GetResponse(request);

            ValidateResponse(response);

            return response;
        }

        private void IgnoreSSLCertificateProblems()
        {
            if (!ignoreInvalidSSLSet)
            {
                ignoreInvalidSSLSet = true;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
        }

        private HttpWebRequest CreateHttpRequest(byte[] xmlRequestBytes)
        {
            var request = (HttpWebRequest)WebRequest.Create(server + "xml");
            request.Proxy = null;
            request.Credentials = new NetworkCredential(UserName, Password);
            request.ContentType = "application/xml; encoding='utf8'";
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

        private async void SendRequestAsync(HttpWebRequest request, byte[] xmlRequestBytes)
        {
            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(xmlRequestBytes, 0, xmlRequestBytes.Length);
            }
        }

        private XDocument GetResponse(HttpWebRequest request)
        {
            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var tempDoc = new XmlDocument();
                    tempDoc.Load(response.GetResponseStream());

                    var doc = XDocument.Parse(tempDoc.InnerXml, LoadOptions.SetLineInfo);

                    return doc;
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
        }

        /*private async XDocument GetResponseAsync(HttpWebRequest request)
        {
            try
            {
                using (var response = (HttpWebResponse) await request.GetResponseAsync())
                {
                    return GetResponseXml(response);

                    //i have a unit test for testing aggregate exceptions are handled properly with prtgapi
                    //we should implement the same thing for normal and aggregate exceptions here

                    var tempDoc = new XmlDocument();
                    tempDoc.Load(await response.GetResponseStream())
                }
            }
        }*/

        private void ValidateResponse(XDocument response)
        {
            if (response.Descendants("errors").Any())
                throw new SwitchvoxRequestException(response.Descendants("error").Single().Attribute("message").Value);
        }
    }
}