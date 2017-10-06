using System.Xml.Linq;

namespace SwitchvoxAPI.Methods
{
    public partial class CurrentCalls
    {
        public void HangUp(string callId)
        {
            var xml = new XElement("current_call_id", callId);

            client.Execute(new RequestMethod("switchvox.currentCalls.hangUp", xml));
        }
    }
}
