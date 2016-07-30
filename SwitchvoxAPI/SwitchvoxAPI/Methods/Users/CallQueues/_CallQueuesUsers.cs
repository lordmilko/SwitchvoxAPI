namespace SwitchvoxAPI
{
    public partial class CallQueuesUsers
    {
        private readonly SwitchvoxClient client;

        internal CallQueuesUsers(SwitchvoxClient client)
        {
            this.client = client;
        }
    }
}
