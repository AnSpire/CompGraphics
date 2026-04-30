namespace lr5
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Left side – images
        private Label lblOriginal;
        private PictureBox pictureBox1;
        private Label lblModified;
        private PictureBox pictureBox2;
        private Panel pnlSeparator;

        // Top controls
        private Button btnLoad;
        private Label lblImageFile;
        private Label lblAlgorithmCaption;
        private ComboBox cmbAlgorithm;
        private Button btnModify;

        // Range controls
        private Label lblXFrom;
        private NumericUpDown numX1;
        private Label lblXTo;
        private NumericUpDown numX2;
        private Label lblYFrom;
        private NumericUpDown numY1;
        private Label lblYTo;
        private NumericUpDown numY2;
        private Button btnAnalyze;

        // Tab + data views
        private TabControl tabControl1;
        private TabPage tabPageGrid;
        private DataGridView dataGridView1;
        private TabPage tabPageList;
        private ListBox listBox1;

        // Pixel coordinate lookup
        private Label lblPxX;
        private NumericUpDown numPxX;
        private Label lblPxY;
        private NumericUpDown numPxY;
        private Button btnShowPixel;
        private TextBox txtPixelInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblOriginal          = new Label();
            pictureBox1          = new PictureBox();
            lblModified          = new Label();
            pictureBox2          = new PictureBox();
            pnlSeparator         = new Panel();

            btnLoad              = new Button();
            lblImageFile         = new Label();
            lblAlgorithmCaption  = new Label();
            cmbAlgorithm         = new ComboBox();
            btnModify            = new Button();

            lblXFrom  = new Label();
            numX1     = new NumericUpDown();
            lblXTo    = new Label();
            numX2     = new NumericUpDown();
            lblYFrom  = new Label();
            numY1     = new NumericUpDown();
            lblYTo    = new Label();
            numY2     = new NumericUpDown();
            btnAnalyze = new Button();

            tabControl1  = new TabControl();
            tabPageGrid  = new TabPage();
            dataGridView1 = new DataGridView();
            tabPageList  = new TabPage();
            listBox1     = new ListBox();

            lblPxX       = new Label();
            numPxX       = new NumericUpDown();
            lblPxY       = new Label();
            numPxY       = new NumericUpDown();
            btnShowPixel = new Button();
            txtPixelInfo = new TextBox();

            ((System.ComponentModel.ISupportInitialize)numX1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPxX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPxY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPageGrid.SuspendLayout();
            tabPageList.SuspendLayout();
            SuspendLayout();

            // ── Left: original image ─────────────────────────────────────
            lblOriginal.Text      = "Оригинальное изображение";
            lblOriginal.Location  = new Point(5, 5);
            lblOriginal.Size      = new Size(598, 18);
            lblOriginal.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);

            pictureBox1.Location  = new Point(5, 25);
            pictureBox1.Size      = new Size(598, 375);
            pictureBox1.SizeMode  = PictureBoxSizeMode.Zoom;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BackColor = Color.FromArgb(30, 30, 30);

            // ── Left: modified image ─────────────────────────────────────
            lblModified.Text      = "Изменённое изображение";
            lblModified.Location  = new Point(5, 405);
            lblModified.Size      = new Size(598, 18);
            lblModified.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);

            pictureBox2.Location  = new Point(5, 425);
            pictureBox2.Size      = new Size(598, 375);
            pictureBox2.SizeMode  = PictureBoxSizeMode.Zoom;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.BackColor = Color.FromArgb(30, 30, 30);

            // ── Vertical separator ───────────────────────────────────────
            pnlSeparator.Location  = new Point(604, 0);
            pnlSeparator.Size      = new Size(3, 825);
            pnlSeparator.BackColor = Color.Silver;

            // ── Row 1: load + algorithm ──────────────────────────────────
            btnLoad.Text     = "Загрузить...";
            btnLoad.Location = new Point(608, 5);
            btnLoad.Size     = new Size(100, 30);
            btnLoad.Click   += btnLoad_Click;

            lblImageFile.Text      = "(не загружено)";
            lblImageFile.Location  = new Point(715, 10);
            lblImageFile.Size      = new Size(265, 20);
            lblImageFile.ForeColor = Color.Gray;

            lblAlgorithmCaption.Text      = "Алгоритм:";
            lblAlgorithmCaption.Location  = new Point(990, 10);
            lblAlgorithmCaption.Size      = new Size(70, 20);
            lblAlgorithmCaption.TextAlign = ContentAlignment.MiddleLeft;

            cmbAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlgorithm.Location      = new Point(1065, 7);
            cmbAlgorithm.Size          = new Size(178, 25);

            btnModify.Text     = "Применить фильтр";
            btnModify.Location = new Point(1252, 5);
            btnModify.Size     = new Size(148, 30);
            btnModify.Click   += btnModify_Click;

            // ── Row 2: range controls ────────────────────────────────────
            lblXFrom.Text      = "X от:";
            lblXFrom.Location  = new Point(608, 46);
            lblXFrom.Size      = new Size(38, 23);
            lblXFrom.TextAlign = ContentAlignment.MiddleLeft;

            numX1.Location = new Point(649, 43);
            numX1.Size     = new Size(70, 25);
            numX1.Minimum  = 0;
            numX1.Maximum  = 9999;
            numX1.Value    = 0;

            lblXTo.Text      = "до:";
            lblXTo.Location  = new Point(724, 46);
            lblXTo.Size      = new Size(25, 23);
            lblXTo.TextAlign = ContentAlignment.MiddleLeft;

            numX2.Location = new Point(752, 43);
            numX2.Size     = new Size(70, 25);
            numX2.Minimum  = 0;
            numX2.Maximum  = 9999;
            numX2.Value    = 49;

            lblYFrom.Text      = "Y от:";
            lblYFrom.Location  = new Point(832, 46);
            lblYFrom.Size      = new Size(38, 23);
            lblYFrom.TextAlign = ContentAlignment.MiddleLeft;

            numY1.Location = new Point(873, 43);
            numY1.Size     = new Size(70, 25);
            numY1.Minimum  = 0;
            numY1.Maximum  = 9999;
            numY1.Value    = 0;

            lblYTo.Text      = "до:";
            lblYTo.Location  = new Point(948, 46);
            lblYTo.Size      = new Size(25, 23);
            lblYTo.TextAlign = ContentAlignment.MiddleLeft;

            numY2.Location = new Point(976, 43);
            numY2.Size     = new Size(70, 25);
            numY2.Minimum  = 0;
            numY2.Maximum  = 9999;
            numY2.Value    = 49;

            btnAnalyze.Text     = "Анализировать";
            btnAnalyze.Location = new Point(1056, 43);
            btnAnalyze.Size     = new Size(130, 25);
            btnAnalyze.Click   += btnAnalyze_Click;

            // ── TabControl ───────────────────────────────────────────────
            tabControl1.Location = new Point(608, 76);
            tabControl1.Size     = new Size(792, 570);
            tabControl1.Controls.Add(tabPageGrid);
            tabControl1.Controls.Add(tabPageList);

            tabPageGrid.Text = "Таблица (DataGridView)";
            tabPageGrid.Controls.Add(dataGridView1);

            dataGridView1.Dock                  = DockStyle.Fill;
            dataGridView1.AllowUserToAddRows    = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly              = true;
            dataGridView1.RowHeadersVisible     = false;
            dataGridView1.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.BackgroundColor       = SystemColors.Window;
            dataGridView1.BorderStyle           = BorderStyle.None;

            tabPageList.Text = "Список (ListBox)";
            tabPageList.Controls.Add(listBox1);

            listBox1.Dock               = DockStyle.Fill;
            listBox1.Font               = new Font("Consolas", 9f);
            listBox1.HorizontalScrollbar = true;

            // ── Pixel coordinate lookup ──────────────────────────────────
            lblPxX.Text      = "Пиксель X:";
            lblPxX.Location  = new Point(608, 656);
            lblPxX.Size      = new Size(65, 23);
            lblPxX.TextAlign = ContentAlignment.MiddleLeft;

            numPxX.Location = new Point(676, 653);
            numPxX.Size     = new Size(65, 25);
            numPxX.Minimum  = 0;
            numPxX.Maximum  = 9999;
            numPxX.Value    = 0;

            lblPxY.Text      = "Y:";
            lblPxY.Location  = new Point(748, 656);
            lblPxY.Size      = new Size(20, 23);
            lblPxY.TextAlign = ContentAlignment.MiddleLeft;

            numPxY.Location = new Point(771, 653);
            numPxY.Size     = new Size(65, 25);
            numPxY.Minimum  = 0;
            numPxY.Maximum  = 9999;
            numPxY.Value    = 0;

            btnShowPixel.Text     = "Показать пиксель";
            btnShowPixel.Location = new Point(843, 653);
            btnShowPixel.Size     = new Size(150, 25);
            btnShowPixel.Click   += btnShowPixel_Click;

            txtPixelInfo.Location    = new Point(608, 686);
            txtPixelInfo.Size        = new Size(792, 127);
            txtPixelInfo.Multiline   = true;
            txtPixelInfo.ReadOnly    = true;
            txtPixelInfo.ScrollBars  = ScrollBars.Vertical;
            txtPixelInfo.Font        = new Font("Consolas", 9f);
            txtPixelInfo.BackColor   = SystemColors.Window;

            // ── Form ─────────────────────────────────────────────────────
            AutoScaleDimensions = new SizeF(7f, 15f);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(1405, 825);
            Text                = "Работа с Bitmap изображением — lr5";

            Controls.Add(lblOriginal);
            Controls.Add(pictureBox1);
            Controls.Add(lblModified);
            Controls.Add(pictureBox2);
            Controls.Add(pnlSeparator);
            Controls.Add(btnLoad);
            Controls.Add(lblImageFile);
            Controls.Add(lblAlgorithmCaption);
            Controls.Add(cmbAlgorithm);
            Controls.Add(btnModify);
            Controls.Add(lblXFrom);
            Controls.Add(numX1);
            Controls.Add(lblXTo);
            Controls.Add(numX2);
            Controls.Add(lblYFrom);
            Controls.Add(numY1);
            Controls.Add(lblYTo);
            Controls.Add(numY2);
            Controls.Add(btnAnalyze);
            Controls.Add(tabControl1);
            Controls.Add(lblPxX);
            Controls.Add(numPxX);
            Controls.Add(lblPxY);
            Controls.Add(numPxY);
            Controls.Add(btnShowPixel);
            Controls.Add(txtPixelInfo);

            ((System.ComponentModel.ISupportInitialize)numX1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPxX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPxY).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPageGrid.ResumeLayout(false);
            tabPageList.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
