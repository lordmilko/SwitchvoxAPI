using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Switchvox
{
    /// <summary>
    /// Generate a call between two numbers using the phone system.
    /// </summary>
    public class Call : RequestMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Switchvox.Call"/> class.
        /// </summary>
        /// <param name="dialFirst">The number to ring first; this number will then appear "dial" the second number</param>
        /// <param name="dialSecond">The second number to ring. When the call to the first number is answered, the PBX will dial the second number (then connect both parties together)</param>
        /// <param name="dialAsAccountId">The Account ID of the account whose Call Rules and API settings should be used to place the calls. When dialFirst is an extension, this is typically that extension's Account ID</param>
        /// <param name="ignoreUserApiSettings">Whether the Call API Settings (digits to prepend, etc) of dialAsAccountId should be ignored</param>
        /// <param name="callerIdName">The Caller ID to display on the phone called by dialFirst.</param>
        /// <param name="ignoreUserCallRules">Whether the call rules defined under the extension belonging to dialAsAccountId should be ignored when generating the call.</param>
        /// <param name="timeout">How long, in seconds, the phone system should attempt to ring dialFirst without being answered before giving up</param>
        /// <param name="timeoutSecondCall">How long, in seconds, the phone system should attempt to ring dialSecond without being answered before giving up</param>
        /// <param name="autoAnswerFirstCall">Whether dialFirst should automatically answer the call</param>
        /// <param name="variables">A dictionary of variables to update in your IVR. The key is the variable name and the value is the value to set it to.</param>
        public Call(string dialFirst, string dialSecond, string dialAsAccountId, bool ignoreUserApiSettings = false, string callerIdName = "PBX", bool ignoreUserCallRules = false, int timeout = 30, int timeoutSecondCall = 30, bool autoAnswerFirstCall = false, Dictionary<string, string> variables = null) : base("switchvox.call")
        {
            List<XElement> vars = new List<XElement>
            {
                new XElement("dial_first", dialFirst),
                new XElement("dial_second", dialSecond),
                new XElement("dial_as_account_id", dialAsAccountId),
                new XElement("ignore_user_api_settings", Convert.ToInt32(ignoreUserApiSettings)),
                new XElement("caller_id_name", callerIdName),
                new XElement("ignore_user_call_rules", Convert.ToInt32(ignoreUserCallRules)),
                new XElement("timeout", timeout),
                new XElement("timeout_second_call", timeoutSecondCall),
                new XElement("auto_answer", Convert.ToInt32(autoAnswerFirstCall))
            };

            if (variables != null)
            {
                List<XElement> variableList = variables.Select(var => new XElement("variable", var.Key + " = " + var.Value)).ToList();

                vars.Add(new XElement("variables", variableList));
            }

            SetXml(vars);
        }
    }
}
