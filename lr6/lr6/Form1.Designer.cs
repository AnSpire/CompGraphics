namespace lr6
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private OpenTK.GLControl glControl;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.RadioButton radioFilled;
        private System.Windows.Forms.RadioButton radioWireframe;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            topPanel       = new System.Windows.Forms.Panel();
            radioFilled    = new System.Windows.Forms.RadioButton();
            radioWireframe = new System.Windows.Forms.RadioButton();
            glControl      = new OpenTK.GLControl();

            topPanel.SuspendLayout();
            SuspendLayout();

            // radioFilled
            radioFilled.Text     = "Filled (Polygon)";
            radioFilled.Checked  = true;
            radioFilled.AutoSize = true;
            radioFilled.Location = new System.Drawing.Point(8, 9);

            // radioWireframe
            radioWireframe.Text           = "Wireframe (LineLoop)";
            radioWireframe.AutoSize       = true;
            radioWireframe.Location       = new System.Drawing.Point(170, 9);
            radioWireframe.CheckedChanged += RadioWireframe_CheckedChanged;

            // topPanel
            topPanel.Dock      = System.Windows.Forms.DockStyle.Top;
            topPanel.Height    = 34;
            topPanel.BackColor = System.Drawing.SystemColors.Control;
            topPanel.Controls.Add(radioFilled);
            topPanel.Controls.Add(radioWireframe);

            // glControl
            glControl.Dock    = System.Windows.Forms.DockStyle.Fill;
            glControl.Load   += GlControl_Load;
            glControl.Paint  += GlControl_Paint;
            glControl.Resize += GlControl_Resize;

            // Form1
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize    = new System.Drawing.Size(960, 600);
            Text          = "LR6 — 3D Shapes (OpenTK)";
            Controls.Add(glControl);  // Fill — первым
            Controls.Add(topPanel);   // Top  — последним, займёт верх

            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}
