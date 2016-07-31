namespace SwitchvoxAPI
{
    /// <summary>
    /// Methods contained in the Switchvox.CallLogs namespace.
    /// </summary>
    public partial class CallLogs
    {
        private readonly SwitchvoxClient client;

        internal CallLogs(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
