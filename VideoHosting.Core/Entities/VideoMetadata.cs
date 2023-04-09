namespace VideoHosting.Core.Entities
{
    public class VideoMetadata : VideoBase
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public List<string> Tags { get; set; }

        //public string FilePath { get; set; }
    }
}