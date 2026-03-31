namespace pr3
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartFunctions = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblFunction1 = new System.Windows.Forms.Label();
            this.txtFunction1 = new System.Windows.Forms.TextBox();
            this.lblFunction2 = new System.Windows.Forms.Label();
            this.txtFunction2 = new System.Windows.Forms.TextBox();
            this.lblFunction3 = new System.Windows.Forms.Label();
            this.txtFunction3 = new System.Windows.Forms.TextBox();
            this.btnPlot = new System.Windows.Forms.Button();
            this.lblChartType = new System.Windows.Forms.Label();
            this.cmbChartType = new System.Windows.Forms.ComboBox();
            this.lblXRange = new System.Windows.Forms.Label();
            this.nudXMin = new System.Windows.Forms.NumericUpDown();
            this.nudXMax = new System.Windows.Forms.NumericUpDown();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMax)).BeginInit();
            this.SuspendLayout();
            // 
            // chartFunctions
            // 
            this.chartFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Title = "X";
            chartArea1.AxisY.Title = "Y";
            chartArea1.Name = "ChartArea1";
            chartArea1.AxisX.MajorGrid.Enabled = true;
            chartArea1.AxisY.MajorGrid.Enabled = true;
            this.chartFunctions.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartFunctions.Legends.Add(legend1);
            this.chartFunctions.Location = new System.Drawing.Point(12, 150);
            this.chartFunctions.Name = "chartFunctions";
            this.chartFunctions.Size = new System.Drawing.Size(760, 400);
            this.chartFunctions.TabIndex = 0;
            // 
            // lblFunction1
            // 
            this.lblFunction1.AutoSize = true;
            this.lblFunction1.Location = new System.Drawing.Point(12, 15);
            this.lblFunction1.Name = "lblFunction1";
            this.lblFunction1.Size = new System.Drawing.Size(106, 15);
            this.lblFunction1.TabIndex = 1;
            this.lblFunction1.Text = "Функция 1 (синий):";
            // 
            // txtFunction1
            // 
            this.txtFunction1.Location = new System.Drawing.Point(124, 12);
            this.txtFunction1.Name = "txtFunction1";
            this.txtFunction1.Size = new System.Drawing.Size(150, 23);
            this.txtFunction1.TabIndex = 2;
            this.txtFunction1.Text = "Sin(x)";
            // 
            // lblFunction2
            // 
            this.lblFunction2.AutoSize = true;
            this.lblFunction2.Location = new System.Drawing.Point(12, 45);
            this.lblFunction2.Name = "lblFunction2";
            this.lblFunction2.Size = new System.Drawing.Size(109, 15);
            this.lblFunction2.TabIndex = 3;
            this.lblFunction2.Text = "Функция 2 (красный):";
            // 
            // txtFunction2
            // 
            this.txtFunction2.Location = new System.Drawing.Point(124, 42);
            this.txtFunction2.Name = "txtFunction2";
            this.txtFunction2.Size = new System.Drawing.Size(150, 23);
            this.txtFunction2.TabIndex = 4;
            this.txtFunction2.Text = "x^2";
            // 
            // lblFunction3
            // 
            this.lblFunction3.AutoSize = true;
            this.lblFunction3.Location = new System.Drawing.Point(12, 75);
            this.lblFunction3.Name = "lblFunction3";
            this.lblFunction3.Size = new System.Drawing.Size(108, 15);
            this.lblFunction3.TabIndex = 5;
            this.lblFunction3.Text = "Функция 3 (зелёный):";
            // 
            // txtFunction3
            // 
            this.txtFunction3.Location = new System.Drawing.Point(124, 72);
            this.txtFunction3.Name = "txtFunction3";
            this.txtFunction3.Size = new System.Drawing.Size(150, 23);
            this.txtFunction3.TabIndex = 6;
            this.txtFunction3.Text = "Cos(x)";
            // 
            // btnPlot
            // 
            this.btnPlot.Location = new System.Drawing.Point(280, 12);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(120, 30);
            this.btnPlot.TabIndex = 7;
            this.btnPlot.Text = "Построить графики";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // lblChartType
            // 
            this.lblChartType.AutoSize = true;
            this.lblChartType.Location = new System.Drawing.Point(280, 50);
            this.lblChartType.Name = "lblChartType";
            this.lblChartType.Size = new System.Drawing.Size(80, 15);
            this.lblChartType.TabIndex = 8;
            this.lblChartType.Text = "Тип графика:";
            // 
            // cmbChartType
            // 
            this.cmbChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChartType.Items.AddRange(new object[] { "Линейный", "Точечный", "Линии с маркерами" });
            this.cmbChartType.Location = new System.Drawing.Point(280, 68);
            this.cmbChartType.Name = "cmbChartType";
            this.cmbChartType.Size = new System.Drawing.Size(150, 23);
            this.cmbChartType.TabIndex = 9;
            this.cmbChartType.SelectedIndexChanged += new System.EventHandler(this.cmbChartType_SelectedIndexChanged);
            // 
            // lblXRange
            // 
            this.lblXRange.AutoSize = true;
            this.lblXRange.Location = new System.Drawing.Point(450, 15);
            this.lblXRange.Name = "lblXRange";
            this.lblXRange.Size = new System.Drawing.Size(120, 15);
            this.lblXRange.TabIndex = 10;
            this.lblXRange.Text = "Диапазон X (от ... до):";
            // 
            // nudXMin
            // 
            this.nudXMin.Location = new System.Drawing.Point(450, 40);
            this.nudXMin.Minimum = new decimal(new int[] { 1000, 0, 0, -2147483648 });
            this.nudXMin.Name = "nudXMin";
            this.nudXMin.Size = new System.Drawing.Size(80, 23);
            this.nudXMin.TabIndex = 11;
            this.nudXMin.Value = new decimal(new int[] { 10, 0, 0, -2147483648 });
            // 
            // nudXMax
            // 
            this.nudXMax.Location = new System.Drawing.Point(540, 40);
            this.nudXMax.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudXMax.Minimum = new decimal(new int[] { 1000, 0, 0, -2147483648 });
            this.nudXMax.Name = "nudXMax";
            this.nudXMax.Size = new System.Drawing.Size(80, 23);
            this.nudXMax.TabIndex = 12;
            this.nudXMax.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(536, 42);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(0, 15);
            this.lblTo.TabIndex = 13;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(12, 120);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 15);
            this.lblError.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.nudXMax);
            this.Controls.Add(this.nudXMin);
            this.Controls.Add(this.lblXRange);
            this.Controls.Add(this.cmbChartType);
            this.Controls.Add(this.lblChartType);
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.txtFunction3);
            this.Controls.Add(this.lblFunction3);
            this.Controls.Add(this.txtFunction2);
            this.Controls.Add(this.lblFunction2);
            this.Controls.Add(this.txtFunction1);
            this.Controls.Add(this.lblFunction1);
            this.Controls.Add(this.chartFunctions);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Построение графиков функций";
            ((System.ComponentModel.ISupportInitialize)(this.chartFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartFunctions;
        private System.Windows.Forms.Label lblFunction1;
        private System.Windows.Forms.TextBox txtFunction1;
        private System.Windows.Forms.Label lblFunction2;
        private System.Windows.Forms.TextBox txtFunction2;
        private System.Windows.Forms.Label lblFunction3;
        private System.Windows.Forms.TextBox txtFunction3;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.Label lblChartType;
        private System.Windows.Forms.ComboBox cmbChartType;
        private System.Windows.Forms.Label lblXRange;
        private System.Windows.Forms.NumericUpDown nudXMin;
        private System.Windows.Forms.NumericUpDown nudXMax;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblError;
    }
}
