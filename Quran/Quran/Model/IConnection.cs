namespace Quran
{
    public interface IConnection
    {
        bool IsConnection { get; set; }
        void CheckConnection();
    }
}
