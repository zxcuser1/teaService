namespace GateWayApi.Contracts.Images
{
    public class ImageRequestForm
    {
        public IFormFile Image {get; set;}
        public ImageCategory ImageCategory {get; set;}
    }
}