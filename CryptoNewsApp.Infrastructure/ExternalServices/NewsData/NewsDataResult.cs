namespace CryptoNewsApp.Infrastructure.ExternalServices.NewsData
{
    public class NewsDataResult
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string SourceId { get; set; }
        public string PubDate { get; set; }
        public string Image_Url { get; set; }
    }
}
