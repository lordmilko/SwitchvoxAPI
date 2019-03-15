using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    /// <summary>
    /// Generate a call between two numbers using the phone system.
    /// </summary>
    public partial class Users
    {
        /// <summary>
        /// Generate a call between two numbers using the phone system.
        /// </summary>
        /// <param name="dialFirst">The number to ring first; this number will then appear to "dial" the second number</param>
        /// <param name="dialSecond">The second number to ring. When the call to the first number is answered, the PBX will dial the second number (then connect both parties together)</param>
        /// <param name="accountId">The Account ID of the account whose Call Rules and API settings should be used to place the calls. When dialFirst is an extension, this is typically that extension's Account ID.</param>
        /// <param name="ignoreUserApiSettings">Whether the Call API Settings (digits to prepend, etc) of accountId should be ignored</param>
        /// <param name="callerIdName">The Caller ID to display on the phone called by dialFirst.</param>
        /// <param name="ignoreUserCallRules">Whether the call rules defined under the extension belonging to accountId should be ignored when generating the call.</param>
        /// <param name="timeout">How long, in seconds, the phone system should attempt to ring dialFirst without being answered before giving up</param>
        /// <param name="variables">A dictionary of variables to update in your IVR. The key is the variable name and the value is the value to set it to.</param>
        public void Call(string dialFirst, string dialSecond, string accountId = null, bool ignoreUserApiSettings = false, string callerIdName = "PBX", bool ignoreUserCallRules = false, int timeout = 30, Dictionary<string, string> variables = null)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                ExtensionInfo info = GetMyInfo();

                if (info == null || info.AccountId == string.Empty)
                    throw new ArgumentException("Account ID must be specified.");

                accountId = info.AccountId;
            }

            List<XElement> vars = new List<XElement>
            {
                new XElement("dial_first", dialFirst),
                new XElement("dial_second", dialSecond),
                new XElement("account_id", accountId),
                new XElement("caller_id_name", callerIdName),
                new XElement("timeout", timeout),
                new XElement("ignore_user_api_settings", Convert.ToInt32(ignoreUserApiSettings)),
                new XElement("ignore_user_call_rules", Convert.ToInt32(ignoreUserCallRules))
            };

            if (variables != null)
            {
                List<XElement> variableList = variables.Select(var => new XElement("variable", var.Key + " = " + var.Value)).ToList();

                vars.Add(new XElement("variables", variableList));
            }

            var response = client.Execute(new RequestMethod("switchvox.users.call", vars));
        }
    }
}
