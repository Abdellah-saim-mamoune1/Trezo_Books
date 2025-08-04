namespace EcommerceBackend.WebAPI
{
    public class GetGoogleApiDto
    {
        public List<GoogleBookItem>? Items { get; set; }
    }

 
    public class GoogleBookItem
    {
        public DGetGoogleApiItems? VolumeInfo { get; set; }
    }

    public class DGetGoogleApiItems
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ImageLinks? ImageLinks { get; set; }
        public List<string>? Authors { get; set; }
        public string? PublishedDate { get; set; }
        public int PageCount { get; set; }
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public string ? Language { get; set; }
    }
    public class ImageLinks
    {
        public string? SmallThumbnail { get; set; }

    }



}
