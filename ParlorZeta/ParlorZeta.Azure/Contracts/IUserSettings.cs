namespace ParlorZeta.Azure.Contracts
{
    public interface IUserSettings
    {
        string GetSelectedSubscriptionId();
        void SetSelectedSubscriptionId(string value);
    }
}