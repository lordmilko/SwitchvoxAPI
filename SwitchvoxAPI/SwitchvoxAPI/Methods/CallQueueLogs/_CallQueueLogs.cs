namespace SwitchvoxAPI
{
    public partial class CallQueueLogs
    {
        private readonly SwitchvoxClient client;

        internal CallQueueLogs(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
