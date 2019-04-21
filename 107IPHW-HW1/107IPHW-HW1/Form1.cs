using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace _107IPHW_HW1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		Image<Bgr, Byte> inputImage;
        Image<Bgr, Byte> grayImage;
		byte b, g, r;

		private void binarization_btn_Click(object sender, EventArgs e)
		{
            
            gray_btn_Click(this, null);
            Image<Bgr, Byte> _Image = grayImage.Clone();
            int minGrayValue = 0, maxGrayValue = 255;
            int threshold = (minGrayValue + maxGrayValue) / 2; ;
            for (int x = 0; x < _Image.Rows; x++)
            {
                for (int y = 0; y < _Image.Cols; y++)
                {
                    b = _Image.Data[x, y, 0];
                    g = _Image.Data[x, y, 1];
                    r = _Image.Data[x, y, 2];

                    if (_Image.Data[x, y, 0] > threshold) _Image.Data[x, y, 0] = 255;
                    else _Image.Data[x, y, 0] = 0;
                    if (_Image.Data[x, y, 1] > threshold) _Image.Data[x, y, 1] = 255;
                    else _Image.Data[x, y, 1] = 0;
                    if (_Image.Data[x, y, 2] > threshold) _Image.Data[x, y, 2] = 255;
                    else _Image.Data[x, y, 2] = 0;
                }
            }
            _outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _outputPictureBox.Image = _Image.ToBitmap();
            

        }

		private void invert_btn_Click(object sender, EventArgs e)
		{
            Image<Bgr, Byte> _Image = inputImage.Clone();
            for (int x = 0; x < _Image.Rows; x++)
            {
                for (int y = 0; y < _Image.Cols; y++)
                {

                    b = _Image.Data[x, y, 0];
                    g = _Image.Data[x, y, 1];
                    r = _Image.Data[x, y, 2];


                    _Image.Data[x, y, 0] = (byte)(255 - _Image.Data[x, y, 0]);
                    _Image.Data[x, y, 1] = (byte)(255 - _Image.Data[x, y, 1]);
                    _Image.Data[x, y, 2] = (byte)(255 - _Image.Data[x, y, 2]);


                }

            }
            _outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _outputPictureBox.Image = _Image.ToBitmap();
        }

		private void relief_btn_Click(object sender, EventArgs e)
		{
            Image<Bgr, Byte> _Image = inputImage.Clone();
            Byte b1, g1, r1, b2, g2, r2;

            for (int x = 0; x < _Image.Rows - 1; x++)
            {
                for (int y = 0; y < _Image.Cols - 1; y++)
                {

                    b = _Image.Data[x, y, 0];
                    g = _Image.Data[x, y, 1];
                    r = _Image.Data[x, y, 2];
                    b1 = _Image.Data[x + 1, y + 1, 0];
                    g1 = _Image.Data[x + 1, y + 1, 1];
                    r1 = _Image.Data[x + 1, y + 1, 2];
                    b2 = (byte)Math.Abs(b - b1 + 128);
                    g2 = (byte)Math.Abs(g - g1 + 128);
                    r2 = (byte)Math.Abs(r - r1 + 128);
                    if (b2 > 255) b2 = 255;
                    if (b2 < 0) b2 = 0;
                    if (g2 > 255) g2 = 255;
                    if (g2 < 0) g2 = 0;
                    if (r2 > 255) r2 = 255;
                    if (r2 < 0) r2 = 0;
                    _Image.Data[x, y, 0] = b2;
                    _Image.Data[x, y, 1] = g2;
                    _Image.Data[x, y, 2] = r2;
                }

            }
            _outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _outputPictureBox.Image = _Image.ToBitmap();
        }

		private void sharpen_btn_Click(object sender, EventArgs e)
		{
            Image<Bgr, Byte> _Image = inputImage.Clone();
            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int x = 1; x < _Image.Rows - 1; x++)
            {
                for (int y = 1; y < _Image.Cols - 1; y++)
                {
                    int index = 0;
                    int b1 = 0, g1 = 0, r1 = 0;
                    b = _Image.Data[x, y, 0];
                    g = _Image.Data[x, y, 1];
                    r = _Image.Data[x, y, 2];
                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            b = _Image.Data[x + row, y + col, 0];
                            g = _Image.Data[x + row, y + col, 1];
                            r = _Image.Data[x + row, y + col, 2];
                            b1 += b * Laplacian[index];
                            g1 += g * Laplacian[index];
                            r1 += r * Laplacian[index];
                            index++;
                        }
                    }
                    if (b1 > 255) b1 = 255;
                    if (b1 < 0) b1 = 0;
                    if (g1 > 255) g1 = 255;
                    if (g1 < 0) g1 = 0;
                    if (r1 > 255) r1 = 255;
                    if (r1 < 0) r1 = 0;
                    _Image.Data[x - 1, y - 1, 0] = (byte)b1;
                    _Image.Data[x - 1, y - 1, 1] = (byte)g1;
                    _Image.Data[x - 1, y - 1, 2] = (byte)r1;

                }

            }
            _outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _outputPictureBox.Image = _Image.ToBitmap();
        }

		private void mirror_btn_Click(object sender, EventArgs e)
		{
            Image<Bgr, Byte> _Image = inputImage.Clone();
            byte b1, g1, r1;
            for (int x = 0; x < _Image.Rows; x++)
            {
                for (int y = 0, i = _Image.Cols - 1; y < _Image.Cols / 2; y++, i--)
                {
                    b = _Image.Data[x, _Image.Cols - y - 1, 0];
                    g = _Image.Data[x, _Image.Cols - y - 1, 1];
                    r = _Image.Data[x, _Image.Cols - y - 1, 2];
                    b1 = _Image.Data[x, y, 0];
                    g1 = _Image.Data[x, y, 1];
                    r1 = _Image.Data[x, y, 2];
                    _Image.Data[x, y, 0] = b;
                    _Image.Data[x, y, 1] = g;
                    _Image.Data[x, y, 2] = r;
                    _Image.Data[x, i, 0] = b1;
                    _Image.Data[x, i, 1] = g1;
                    _Image.Data[x, i, 2] = r1;
                }

            }
            _outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            _outputPictureBox.Image = _Image.ToBitmap();

        }

		private void open_image_btn_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = "Select file";
			dialog.InitialDirectory = ".\\";
			dialog.Filter = "所有合適文件(*.bmp, *.jpg *.png) | *.bmp; *.jpg; *.png";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				inputImage = new Image<Bgr, byte>(dialog.FileName);

				_sourcePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				_sourcePictureBox.Image = inputImage.ToBitmap();

			}
		}

		private void gray_btn_Click(object sender, EventArgs e)
		{
			Image<Bgr, Byte> _Image = inputImage.Clone();


			for (int x = 0; x < _Image.Rows; x++)
			{
				for (int y = 0; y < _Image.Cols; y++)
				{

					b = _Image.Data[x, y, 0];
					g = _Image.Data[x, y, 1];
					r = _Image.Data[x, y, 2];


					byte grayScale = (byte)((b * 0.3) + (g * 0.59) + (r * 0.11));

					_Image.Data[x, y, 0] = grayScale;
					_Image.Data[x, y, 1] = grayScale;
					_Image.Data[x, y, 2] = grayScale;


				}

			}
			_outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
			_outputPictureBox.Image = _Image.ToBitmap();
            grayImage = _Image;
		}
	}
}
