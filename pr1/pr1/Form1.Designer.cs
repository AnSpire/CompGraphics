namespace pr1
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
            groupBoxControls = new GroupBox();
            radioArbitraryPolygon = new RadioButton();
            radioRegularPolygon = new RadioButton();
            labelPoints = new Label();
            listBoxPoints = new ListBox();
            buttonRemovePoint = new Button();
            buttonAddPoint = new Button();
            labelShapeType = new Label();
            comboBoxShapeType = new ComboBox();
            textBoxParams = new TextBox();
            labelParams = new Label();
            buttonClearAll = new Button();
            buttonErase = new Button();
            buttonDraw = new Button();
            buttonEnable = new Button();
            groupBoxCanvas = new GroupBox();
            panelCanvas = new Panel();
            groupBoxMessages = new GroupBox();
            labelStatus = new Label();
            groupBoxControls.SuspendLayout();
            groupBoxCanvas.SuspendLayout();
            groupBoxMessages.SuspendLayout();
            SuspendLayout();
            //
            // groupBoxControls
            //
            groupBoxControls.Controls.Add(radioArbitraryPolygon);
            groupBoxControls.Controls.Add(radioRegularPolygon);
            groupBoxControls.Controls.Add(labelPoints);
            groupBoxControls.Controls.Add(listBoxPoints);
            groupBoxControls.Controls.Add(buttonRemovePoint);
            groupBoxControls.Controls.Add(buttonAddPoint);
            groupBoxControls.Controls.Add(labelShapeType);
            groupBoxControls.Controls.Add(comboBoxShapeType);
            groupBoxControls.Controls.Add(textBoxParams);
            groupBoxControls.Controls.Add(labelParams);
            groupBoxControls.Controls.Add(buttonClearAll);
            groupBoxControls.Controls.Add(buttonErase);
            groupBoxControls.Controls.Add(buttonDraw);
            groupBoxControls.Controls.Add(buttonEnable);
            groupBoxControls.Location = new Point(85, 62);
            groupBoxControls.Name = "groupBoxControls";
            groupBoxControls.Size = new Size(280, 400);
            groupBoxControls.TabIndex = 0;
            groupBoxControls.TabStop = false;
            groupBoxControls.Text = "Управление";
            //
            // labelShapeType
            //
            labelShapeType.AutoSize = true;
            labelShapeType.Location = new Point(20, 32);
            labelShapeType.Name = "labelShapeType";
            labelShapeType.Size = new Size(47, 15);
            labelShapeType.TabIndex = 7;
            labelShapeType.Text = "Фигура";
            //
            // radioRegularPolygon
            //
            radioRegularPolygon.AutoSize = true;
            radioRegularPolygon.Location = new Point(150, 30);
            radioRegularPolygon.Name = "radioRegularPolygon";
            radioRegularPolygon.Size = new Size(120, 19);
            radioRegularPolygon.TabIndex = 15;
            radioRegularPolygon.Text = "Правильный";
            radioRegularPolygon.Checked = true;
            radioRegularPolygon.CheckedChanged += RadioPolygonType_CheckedChanged;
            //
            // radioArbitraryPolygon
            //
            radioArbitraryPolygon.AutoSize = true;
            radioArbitraryPolygon.Location = new Point(150, 50);
            radioArbitraryPolygon.Name = "radioArbitraryPolygon";
            radioArbitraryPolygon.Size = new Size(120, 19);
            radioArbitraryPolygon.TabIndex = 16;
            radioArbitraryPolygon.Text = "Произвольный";
            radioArbitraryPolygon.CheckedChanged += RadioPolygonType_CheckedChanged;
            //
            // labelPoints
            //
            labelPoints.AutoSize = true;
            labelPoints.Location = new Point(20, 240);
            labelPoints.Name = "labelPoints";
            labelPoints.Size = new Size(40, 15);
            labelPoints.TabIndex = 14;
            labelPoints.Text = "Точки";
            //
            // listBoxPoints
            //
            listBoxPoints.FormattingEnabled = true;
            listBoxPoints.ItemHeight = 15;
            listBoxPoints.Location = new Point(20, 260);
            listBoxPoints.Name = "listBoxPoints";
            listBoxPoints.Size = new Size(110, 94);
            listBoxPoints.TabIndex = 8;
            //
            // buttonAddPoint
            //
            buttonAddPoint.Location = new Point(150, 260);
            buttonAddPoint.Name = "buttonAddPoint";
            buttonAddPoint.Size = new Size(50, 25);
            buttonAddPoint.TabIndex = 9;
            buttonAddPoint.Text = "Доб.";
            buttonAddPoint.UseVisualStyleBackColor = true;
            buttonAddPoint.Click += ButtonAddPoint_Click;
            //
            // buttonRemovePoint
            //
            buttonRemovePoint.Location = new Point(150, 290);
            buttonRemovePoint.Name = "buttonRemovePoint";
            buttonRemovePoint.Size = new Size(50, 25);
            buttonRemovePoint.TabIndex = 10;
            buttonRemovePoint.Text = "Удал.";
            buttonRemovePoint.UseVisualStyleBackColor = true;
            buttonRemovePoint.Click += ButtonRemovePoint_Click;
            //
            // comboBoxShapeType
            // 
            comboBoxShapeType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxShapeType.Location = new Point(20, 50);
            comboBoxShapeType.Name = "comboBoxShapeType";
            comboBoxShapeType.Size = new Size(110, 23);
            comboBoxShapeType.TabIndex = 6;
            // 
            // textBoxParams
            // 
            textBoxParams.Location = new Point(20, 85);
            textBoxParams.Multiline = true;
            textBoxParams.Name = "textBoxParams";
            textBoxParams.Size = new Size(110, 110);
            textBoxParams.TabIndex = 4;
            textBoxParams.TextChanged += textBoxParams_TextChanged;
            // 
            // labelParams
            // 
            labelParams.Location = new Point(20, 205);
            labelParams.Name = "labelParams";
            labelParams.Size = new Size(120, 60);
            labelParams.TabIndex = 5;
            labelParams.Text = "Параметры:\nx1,y1,x2,y2\r\nцвет\r\nтолщина";
            // 
            // buttonClearAll
            // 
            buttonClearAll.Location = new Point(150, 210);
            buttonClearAll.Name = "buttonClearAll";
            buttonClearAll.Size = new Size(110, 30);
            buttonClearAll.TabIndex = 3;
            buttonClearAll.Text = "Очистить всё";
            buttonClearAll.UseVisualStyleBackColor = true;
            buttonClearAll.Click += buttonClearAll_Click;
            // 
            // buttonErase
            // 
            buttonErase.Location = new Point(150, 170);
            buttonErase.Name = "buttonErase";
            buttonErase.Size = new Size(110, 30);
            buttonErase.TabIndex = 2;
            buttonErase.Text = "Стереть";
            buttonErase.UseVisualStyleBackColor = true;
            buttonErase.Click += buttonErase_Click;
            // 
            // buttonDraw
            // 
            buttonDraw.Location = new Point(150, 130);
            buttonDraw.Name = "buttonDraw";
            buttonDraw.Size = new Size(110, 30);
            buttonDraw.TabIndex = 1;
            buttonDraw.Text = "Рисовать";
            buttonDraw.UseVisualStyleBackColor = true;
            buttonDraw.Click += buttonDraw_Click;
            // 
            // buttonEnable
            // 
            buttonEnable.Location = new Point(150, 90);
            buttonEnable.Name = "buttonEnable";
            buttonEnable.Size = new Size(110, 30);
            buttonEnable.TabIndex = 0;
            buttonEnable.Text = "Включить графику";
            buttonEnable.UseVisualStyleBackColor = true;
            buttonEnable.Click += buttonEnable_Click;
            // 
            // groupBoxCanvas
            // 
            groupBoxCanvas.Controls.Add(panelCanvas);
            groupBoxCanvas.Location = new Point(420, 62);
            groupBoxCanvas.Name = "groupBoxCanvas";
            groupBoxCanvas.Size = new Size(400, 350);
            groupBoxCanvas.TabIndex = 1;
            groupBoxCanvas.TabStop = false;
            groupBoxCanvas.Text = "Картинка";
            // 
            // panelCanvas
            // 
            panelCanvas.BorderStyle = BorderStyle.FixedSingle;
            panelCanvas.Location = new Point(20, 30);
            panelCanvas.Name = "panelCanvas";
            panelCanvas.Size = new Size(360, 300);
            panelCanvas.TabIndex = 0;
            //
            // groupBoxMessages
            //
            groupBoxMessages.Controls.Add(labelStatus);
            groupBoxMessages.Location = new Point(85, 520);
            groupBoxMessages.Name = "groupBoxMessages";
            groupBoxMessages.Size = new Size(280, 82);
            groupBoxMessages.TabIndex = 2;
            groupBoxMessages.TabStop = false;
            groupBoxMessages.Text = "Сообщения";
            //
            // labelStatus
            //
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(20, 35);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(88, 15);
            labelStatus.TabIndex = 0;
            labelStatus.Text = "Готов к работе";
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 650);
            Controls.Add(groupBoxMessages);
            Controls.Add(groupBoxCanvas);
            Controls.Add(groupBoxControls);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Графика - Рисование фигур";
            groupBoxControls.ResumeLayout(false);
            groupBoxControls.PerformLayout();
            groupBoxCanvas.ResumeLayout(false);
            groupBoxMessages.ResumeLayout(false);
            groupBoxMessages.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.Button buttonErase;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonEnable;
        private System.Windows.Forms.TextBox textBoxParams;
        private System.Windows.Forms.Label labelParams;
        private System.Windows.Forms.GroupBox groupBoxCanvas;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.GroupBox groupBoxMessages;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxShapeType;
        private System.Windows.Forms.Label labelShapeType;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.Button buttonAddPoint;
        private System.Windows.Forms.Button buttonRemovePoint;
        private System.Windows.Forms.Label labelPoints;
        private System.Windows.Forms.RadioButton radioRegularPolygon;
        private System.Windows.Forms.RadioButton radioArbitraryPolygon;
    }
}
