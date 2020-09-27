using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PhotoThumb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void GenerateThumb()
        {

            string pathPerc = @"C:\Users\Victor\Desktop\新建文件夹 (5)\压缩装配式装修流程.png";
            string source = @"C:\Users\Victor\Desktop\装配式装修流程.png";
            if (!File.Exists(pathPerc))
            {
                File.Create(pathPerc).Close();
            }
            else
            {
                File.Delete(pathPerc);
                File.Create(pathPerc).Close();
            }
            getThumImage(source, 50, 2, pathPerc);
        }


         /// <summary>/// 生成缩略图/// </summary> 
         /// <param name="sourceFile">原始图片文件</param>    
         /// <param name="quality">质量压缩比</param>      
         /// <param name="multiple">收缩倍数</param>     
         /// <param name="outputFile">输出文件名</param>    
         /// <returns>成功返回true,失败则返回false</returns>

        public bool getThumImage(String sourceFile, long quality, int multiple, String outputFile)
        {
            try
            {
                long imageQuality = quality;
                Bitmap sourceImage = new Bitmap(sourceFile);
                ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, imageQuality);
                myEncoderParameters.Param[0] = myEncoderParameter; float xWidth = sourceImage.Width;
                float yWidth = sourceImage.Height;
                Bitmap newImage = new Bitmap((int)(xWidth / multiple), (int)(yWidth / multiple));
                Graphics g = Graphics.FromImage(newImage); g.DrawImage(sourceImage, 0, 0, xWidth / multiple, yWidth / multiple); g.Dispose();
                newImage.Save(outputFile, myImageCodecInfo, myEncoderParameters);
                return true;
            }
            catch { return false; }
        }


        /// <summary>
        /// 获取图片编码信息
        /// </summary>
        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // 打开文件
            FileStream fileStream = new FileStream(@"C:\Users\Victor\Desktop\新建文件夹 (5)\aa", FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);

            StreamToImage(stream, @"C:\Users\Victor\Desktop\新建文件夹 (5)\b.png");




            label1.Text = "存储完成";

            //GenerateThumb();
        }



        private void StreamToImage(Stream buffer,string path)
        {
            byte[] bytes = new byte[buffer.Length];
            buffer.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            buffer.Seek(0, SeekOrigin.Begin);

            MemoryStream ms = new MemoryStream(bytes);
            Image img = Image.FromStream(ms);


            img.Save(path);







        }








    }
}
