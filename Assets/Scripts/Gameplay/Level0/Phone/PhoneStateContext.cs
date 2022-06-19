namespace ProjectWendigo
{
    public class PhoneStateContext : AStateContext
    {
        private void Start()
        {
            this.SetState(new PhoneStates.Idle());
        }

        public void GoToRing()
        {
            this.SetState(new PhoneStates.Ringing());
        }

        public void GoToCalling()
        {
            this.SetState(new PhoneStates.Calling());
        }
    }
}