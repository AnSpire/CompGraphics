namespace lr4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.openGLControl = new SharpGL.OpenGLControl();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelDepth = new System.Windows.Forms.Label();
            this.trackBarDepth = new System.Windows.Forms.TrackBar();

            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();

            // openGLControl
            this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.FrameRate = 28;
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);

            // labelDepth
            this.labelDepth.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelDepth.Width = 130;
            this.labelDepth.Text = "Глубина: 6";
            this.labelDepth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelDepth.Font = new System.Drawing.Font("Segoe UI", 10F);

            // trackBarDepth
            this.trackBarDepth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarDepth.Minimum = 0;
            this.trackBarDepth.Maximum = 9;
            this.trackBarDepth.Value = 6;
            this.trackBarDepth.TickFrequency = 1;
            this.trackBarDepth.ValueChanged += new System.EventHandler(this.trackBarDepth_ValueChanged);

            // panelBottom
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.Controls.Add(this.trackBarDepth);
            this.panelBottom.Controls.Add(this.labelDepth);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.openGLControl);
            this.Controls.Add(this.panelBottom);
            this.Text = "Треугольник Серпинского — OpenGL";
            this.MinimumSize = new System.Drawing.Size(400, 300);

            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);
        }

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.TrackBar trackBarDepth;
    }
}
