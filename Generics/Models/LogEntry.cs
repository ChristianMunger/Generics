namespace Generics
{
    public class LogEntry
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public DateTime TimeOfEvent { get; set; } = DateTime.UtcNow;
    }
}