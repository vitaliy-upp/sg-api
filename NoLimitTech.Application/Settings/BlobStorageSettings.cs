namespace NoLimitTech.Application.Settings
{
    public class BlobStorageSettings
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string ConnectionString { get; set; }
        public string ImageContainer { get; set; }
        public string SoundContainer { get; set; }
        public string RecordContainer { get; set; }
        public string ThumbnailContainer { get; set; }
    }
}
