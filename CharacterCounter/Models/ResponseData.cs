namespace CharacterCounter.Models
{
    public class ResponseData
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public List<LetterCountDetails> Data { get; set; }
    }

    public class LetterCountDetails
    {
        public int Count { get; set; }
        public string Letter { get; set; }
    }

    public enum Status
    {
        Success = 1,
        Error = 2
    }
}
