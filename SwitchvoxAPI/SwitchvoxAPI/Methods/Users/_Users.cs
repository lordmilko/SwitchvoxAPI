namespace SwitchvoxAPI.Methods
{
    public partial class Users
    {
        private readonly SwitchvoxClient client;

        public CallQueuesUsers CallQueues;

        internal Users(SwitchvoxClient client)
        {
            this.client = client;

            CallQueues = new CallQueuesUsers(client);
        }
    }
}
