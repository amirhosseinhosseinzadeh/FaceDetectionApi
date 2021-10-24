using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace TestFaceApi
{
    public class ImageUtility
    {
        public async Task<byte[]> ConvertToByteArrayAsync(string imagePath)
        {
           using(MemoryStream ms = new())
           {
              using (FileStream stream = new(imagePath, FileMode.Open))
              {
                  await stream.CopyToAsync(ms);
              }
              var bytes = ms.ToArray();
              return bytes;
           }
        }

        public void FromBytesToImage(byte[] imageBytes,string fileName)
        {
            using (MemoryStream ms = new(imageBytes))
            {
                Image image = Image.FromStream(ms);
                image.Save(fileName + ".jpg", ImageFormat.Jpeg); 
            }
        }
    }
}
