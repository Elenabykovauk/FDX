namespace FDX.Services.Interfaces
{
    public interface ISmsSendService
    {
        Task MessageProcceed(string smsId);
    }
}
