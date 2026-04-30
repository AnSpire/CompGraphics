using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace lr5
{
    public partial class Form1 : Form
    {
        private Bitmap? originalBitmap;
        private Bitmap? modifiedBitmap;

        public Form1()
        {
            InitializeComponent();

            cmbAlgorithm.Items.AddRange(new object[]
            {
                "Оттенки серого",
                "Инверсия цветов",
                "Сепия",
                "Яркость +50",
                "Только красный канал"
            });
            cmbAlgorithm.SelectedIndex = 0;

            // DataGridView columns – added here so Designer stays clean
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colX",         HeaderText = "X",        FillWeight = 60  });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colY",         HeaderText = "Y",        FillWeight = 60  });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colR",         HeaderText = "R (кр)",   FillWeight = 80  });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colG",         HeaderText = "G (зел)",  FillWeight = 80  });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colB",         HeaderText = "B (син)",  FillWeight = 80  });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colA",         HeaderText = "A (альф)", FillWeight = 80  });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBrightness",HeaderText = "Яркость",  FillWeight = 100 });
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ── Load image ───────────────────────────────────────────────────

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
            using var ofd = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*",
                InitialDirectory = Directory.Exists(imagesDir) ? imagesDir : Environment.CurrentDirectory
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            originalBitmap?.Dispose();
            try
            {
                originalBitmap = new Bitmap(ofd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pictureBox1.Image  = originalBitmap;
            lblImageFile.Text  = Path.GetFileName(ofd.FileName);
            lblImageFile.ForeColor = Color.Black;

            numX1.Maximum = numX2.Maximum = originalBitmap.Width  - 1;
            numY1.Maximum = numY2.Maximum = originalBitmap.Height - 1;
            numX1.Value   = 0;
            numY1.Value   = 0;
            numX2.Value   = Math.Min(49, originalBitmap.Width  - 1);
            numY2.Value   = Math.Min(49, originalBitmap.Height - 1);

            numPxX.Maximum = originalBitmap.Width  - 1;
            numPxY.Maximum = originalBitmap.Height - 1;
            numPxX.Value   = 0;
            numPxY.Value   = 0;

            modifiedBitmap?.Dispose();
            modifiedBitmap    = null;
            pictureBox2.Image = null;

            dataGridView1.Rows.Clear();
            listBox1.Items.Clear();
            txtPixelInfo.Clear();
        }

        // ── Analyze pixel range ──────────────────────────────────────────

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (originalBitmap == null)
            {
                MessageBox.Show("Сначала загрузите изображение.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int x1 = (int)numX1.Value, x2 = (int)numX2.Value;
            int y1 = (int)numY1.Value, y2 = (int)numY2.Value;

            if (x1 > x2) (x1, x2) = (x2, x1);
            if (y1 > y2) (y1, y2) = (y2, y1);

            x2 = Math.Min(x2, originalBitmap.Width  - 1);
            y2 = Math.Min(y2, originalBitmap.Height - 1);

            int count = (x2 - x1 + 1) * (y2 - y1 + 1);
            if (count > 10_000)
            {
                var answer = MessageBox.Show(
                    $"Выбранный диапазон содержит {count} пикселей.\nЭто может занять время. Продолжить?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.No) return;
            }

            dataGridView1.SuspendLayout();
            dataGridView1.Rows.Clear();
            listBox1.BeginUpdate();
            listBox1.Items.Clear();

            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    Color c          = originalBitmap.GetPixel(x, y);
                    int   brightness = Brightness(c);

                    int rowIdx = dataGridView1.Rows.Add(x, y, c.R, c.G, c.B, c.A, brightness);
                    var row    = dataGridView1.Rows[rowIdx];

                    // Colour the row with the actual pixel colour
                    Color bg = Color.FromArgb(c.R, c.G, c.B);
                    Color fg = brightness < 128 ? Color.White : Color.Black;
                    row.DefaultCellStyle.BackColor          = bg;
                    row.DefaultCellStyle.ForeColor          = fg;
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(
                        Math.Clamp(c.R + 60, 0, 255),
                        Math.Clamp(c.G + 60, 0, 255),
                        Math.Clamp(c.B + 60, 0, 255));
                    row.DefaultCellStyle.SelectionForeColor = fg;

                    listBox1.Items.Add(
                        $"({x,4}, {y,4})   R={c.R,3}  G={c.G,3}  B={c.B,3}  A={c.A,3}   Яркость={brightness,3}   #{c.R:X2}{c.G:X2}{c.B:X2}");
                }
            }

            dataGridView1.ResumeLayout();
            listBox1.EndUpdate();
        }

        // ── Show single pixel info ───────────────────────────────────────

        private void btnShowPixel_Click(object sender, EventArgs e)
        {
            if (originalBitmap == null)
            {
                MessageBox.Show("Сначала загрузите изображение.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int x = (int)numPxX.Value;
            int y = (int)numPxY.Value;

            Color c          = originalBitmap.GetPixel(x, y);
            int   brightness = Brightness(c);

            txtPixelInfo.Text =
                $"Координаты   : ({x}, {y})\r\n" +
                $"R (красный)  : {c.R}\r\n" +
                $"G (зелёный)  : {c.G}\r\n" +
                $"B (синий)    : {c.B}\r\n" +
                $"A (прозрачн.): {c.A}\r\n" +
                $"Яркость (ITU-R BT.601): {brightness}\r\n";
        }

        // ── Apply colour filter ──────────────────────────────────────────

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (originalBitmap == null)
            {
                MessageBox.Show("Сначала загрузите изображение.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Func<Color, Color> filter = cmbAlgorithm.SelectedIndex switch
            {
                0 => ToGrayscale,
                1 => Invert,
                2 => ToSepia,
                3 => c => BrightnessPlus(c, 50),
                4 => RedOnly,
                _ => c => c
            };

            Cursor = Cursors.WaitCursor;
            try
            {
                modifiedBitmap?.Dispose();
                modifiedBitmap    = ApplyFilter(originalBitmap, filter);
                pictureBox2.Image = modifiedBitmap;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // ── Filter algorithms ────────────────────────────────────────────

        private static Color ToGrayscale(Color c)
        {
            int g = Brightness(c);
            return Color.FromArgb(c.A, g, g, g);
        }

        private static Color Invert(Color c) =>
            Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B);

        private static Color ToSepia(Color c)
        {
            int r = Math.Min(255, (int)(c.R * 0.393 + c.G * 0.769 + c.B * 0.189));
            int g = Math.Min(255, (int)(c.R * 0.349 + c.G * 0.686 + c.B * 0.168));
            int b = Math.Min(255, (int)(c.R * 0.272 + c.G * 0.534 + c.B * 0.131));
            return Color.FromArgb(c.A, r, g, b);
        }

        private static Color BrightnessPlus(Color c, int delta) =>
            Color.FromArgb(c.A,
                Math.Clamp(c.R + delta, 0, 255),
                Math.Clamp(c.G + delta, 0, 255),
                Math.Clamp(c.B + delta, 0, 255));

        private static Color RedOnly(Color c) =>
            Color.FromArgb(c.A, c.R, 0, 0);

        // ── Helpers ──────────────────────────────────────────────────────

        // ITU-R BT.601 luma
        private static int Brightness(Color c) =>
            (c.R * 299 + c.G * 587 + c.B * 114) / 1000;

        // Fast pixel manipulation via LockBits / Marshal
        private static Bitmap ApplyFilter(Bitmap src, Func<Color, Color> filter)
        {
            int w = src.Width, h = src.Height;
            var dst  = new Bitmap(w, h);
            var rect = new Rectangle(0, 0, w, h);

            var srcData = src.LockBits(rect, ImageLockMode.ReadOnly,  PixelFormat.Format32bppArgb);
            var dstData = dst.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int stride   = srcData.Stride;          // always positive for 32bpp
            int byteLen  = stride * h;
            byte[] srcBuf = new byte[byteLen];
            byte[] dstBuf = new byte[byteLen];

            Marshal.Copy(srcData.Scan0, srcBuf, 0, byteLen);

            // Memory layout for Format32bppArgb: B G R A per pixel
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int i = y * stride + x * 4;
                    Color c = Color.FromArgb(srcBuf[i + 3], srcBuf[i + 2], srcBuf[i + 1], srcBuf[i]);
                    Color r = filter(c);
                    dstBuf[i]     = r.B;
                    dstBuf[i + 1] = r.G;
                    dstBuf[i + 2] = r.R;
                    dstBuf[i + 3] = r.A;
                }
            }

            Marshal.Copy(dstBuf, 0, dstData.Scan0, byteLen);
            src.UnlockBits(srcData);
            dst.UnlockBits(dstData);

            return dst;
        }
    }
}
