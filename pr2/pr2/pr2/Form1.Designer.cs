namespace pr2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl = new OpenTK.GLControl();
            this.comboBoxScenes = new System.Windows.Forms.ComboBox();
            this.labelScene = new System.Windows.Forms.Label();
            this.buttonRedraw = new System.Windows.Forms.Button();
            this.labelCurrentScene = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point(12, 45);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(760, 400);
            this.glControl.TabIndex = 0;
            this.glControl.Load += new System.EventHandler(this.glControl_Load);
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
            this.glControl.Resize += new System.EventHandler(this.glControl_Resize);
            // 
            // comboBoxScenes
            // 
            this.comboBoxScenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScenes.Location = new System.Drawing.Point(120, 12);
            this.comboBoxScenes.Name = "comboBoxScenes";
            this.comboBoxScenes.Size = new System.Drawing.Size(200, 23);
            this.comboBoxScenes.TabIndex = 1;
            this.comboBoxScenes.SelectedIndexChanged += new System.EventHandler(this.comboBoxScenes_SelectedIndexChanged);
            // 
            // labelScene
            // 
            this.labelScene.AutoSize = true;
            this.labelScene.Location = new System.Drawing.Point(12, 15);
            this.labelScene.Name = "labelScene";
            this.labelScene.Size = new System.Drawing.Size(102, 15);
            this.labelScene.TabIndex = 2;
            this.labelScene.Text = "Выберите сцену:";
            // 
            // buttonRedraw
            // 
            this.buttonRedraw.Location = new System.Drawing.Point(340, 11);
            this.buttonRedraw.Name = "buttonRedraw";
            this.buttonRedraw.Size = new System.Drawing.Size(100, 25);
            this.buttonRedraw.TabIndex = 3;
            this.buttonRedraw.Text = "Перерисовать";
            this.buttonRedraw.UseVisualStyleBackColor = true;
            this.buttonRedraw.Click += new System.EventHandler(this.buttonRedraw_Click);
            // 
            // labelCurrentScene
            // 
            this.labelCurrentScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentScene.AutoSize = true;
            this.labelCurrentScene.Location = new System.Drawing.Point(500, 15);
            this.labelCurrentScene.Name = "labelCurrentScene";
            this.labelCurrentScene.Size = new System.Drawing.Size(120, 15);
            this.labelCurrentScene.TabIndex = 4;
            this.labelCurrentScene.Text = "Треугольник (3D)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.labelCurrentScene);
            this.Controls.Add(this.buttonRedraw);
            this.Controls.Add(this.labelScene);
            this.Controls.Add(this.comboBoxScenes);
            this.Controls.Add(this.glControl);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenGL Demo - WinForms + OpenTK";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.ComboBox comboBoxScenes;
        private System.Windows.Forms.Label labelScene;
        private System.Windows.Forms.Button buttonRedraw;
        private System.Windows.Forms.Label labelCurrentScene;
    }
}
