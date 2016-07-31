namespace SwitchvoxAPI
{
    public partial class CallQueues
    {
        private readonly SwitchvoxClient client;

        internal CallQueues(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
