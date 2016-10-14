namespace Pentomino
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startAlgButton = new System.Windows.Forms.Button();
            this.rowBox = new System.Windows.Forms.TextBox();
            this.colBox = new System.Windows.Forms.TextBox();
            this.buildAreaButton = new System.Windows.Forms.Button();
            this.blankCellsCheck = new System.Windows.Forms.CheckBox();
            this.isModAlgBox = new System.Windows.Forms.CheckBox();
            this.useTriminoCheck = new System.Windows.Forms.CheckBox();
            this.useTetraminoCheck = new System.Windows.Forms.CheckBox();
            this.usePentaminoCheck = new System.Windows.Forms.CheckBox();
            this.PrevSolButton = new System.Windows.Forms.Button();
            this.NextSolButton = new System.Windows.Forms.Button();
            this.saveFormButton = new System.Windows.Forms.Button();
            this.loadFormButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.aboutProgram = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(569, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(240, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 536);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // startAlgButton
            // 
            this.startAlgButton.Location = new System.Drawing.Point(12, 13);
            this.startAlgButton.Name = "startAlgButton";
            this.startAlgButton.Size = new System.Drawing.Size(97, 57);
            this.startAlgButton.TabIndex = 3;
            this.startAlgButton.Text = "Найти решение";
            this.startAlgButton.UseVisualStyleBackColor = true;
            this.startAlgButton.Click += new System.EventHandler(this.startAlgButton_Click);
            // 
            // rowBox
            // 
            this.rowBox.Location = new System.Drawing.Point(133, 82);
            this.rowBox.Name = "rowBox";
            this.rowBox.Size = new System.Drawing.Size(100, 20);
            this.rowBox.TabIndex = 4;
            // 
            // colBox
            // 
            this.colBox.Location = new System.Drawing.Point(133, 108);
            this.colBox.Name = "colBox";
            this.colBox.Size = new System.Drawing.Size(100, 20);
            this.colBox.TabIndex = 5;
            // 
            // buildAreaButton
            // 
            this.buildAreaButton.Location = new System.Drawing.Point(12, 134);
            this.buildAreaButton.Name = "buildAreaButton";
            this.buildAreaButton.Size = new System.Drawing.Size(221, 43);
            this.buildAreaButton.TabIndex = 6;
            this.buildAreaButton.Text = "Построить поле";
            this.buildAreaButton.UseVisualStyleBackColor = true;
            this.buildAreaButton.Click += new System.EventHandler(this.buildAreaButton_Click);
            // 
            // blankCellsCheck
            // 
            this.blankCellsCheck.AutoSize = true;
            this.blankCellsCheck.Location = new System.Drawing.Point(13, 314);
            this.blankCellsCheck.Name = "blankCellsCheck";
            this.blankCellsCheck.Size = new System.Drawing.Size(153, 17);
            this.blankCellsCheck.TabIndex = 7;
            this.blankCellsCheck.Text = "Добавить пустые клетки";
            this.blankCellsCheck.UseVisualStyleBackColor = true;
            // 
            // isModAlgBox
            // 
            this.isModAlgBox.AutoSize = true;
            this.isModAlgBox.Location = new System.Drawing.Point(13, 337);
            this.isModAlgBox.Name = "isModAlgBox";
            this.isModAlgBox.Size = new System.Drawing.Size(180, 17);
            this.isModAlgBox.TabIndex = 8;
            this.isModAlgBox.Text = "Модифицированный алгоритм";
            this.isModAlgBox.UseVisualStyleBackColor = true;
            this.isModAlgBox.CheckedChanged += new System.EventHandler(this.isModAlgBox_CheckedChanged);
            // 
            // useTriminoCheck
            // 
            this.useTriminoCheck.AutoSize = true;
            this.useTriminoCheck.Location = new System.Drawing.Point(13, 217);
            this.useTriminoCheck.Name = "useTriminoCheck";
            this.useTriminoCheck.Size = new System.Drawing.Size(71, 17);
            this.useTriminoCheck.TabIndex = 9;
            this.useTriminoCheck.Text = "Тримино";
            this.useTriminoCheck.UseVisualStyleBackColor = true;
            // 
            // useTetraminoCheck
            // 
            this.useTetraminoCheck.AutoSize = true;
            this.useTetraminoCheck.Location = new System.Drawing.Point(13, 240);
            this.useTetraminoCheck.Name = "useTetraminoCheck";
            this.useTetraminoCheck.Size = new System.Drawing.Size(82, 17);
            this.useTetraminoCheck.TabIndex = 10;
            this.useTetraminoCheck.Text = "Тетрамино";
            this.useTetraminoCheck.UseVisualStyleBackColor = true;
            // 
            // usePentaminoCheck
            // 
            this.usePentaminoCheck.AutoSize = true;
            this.usePentaminoCheck.Location = new System.Drawing.Point(13, 263);
            this.usePentaminoCheck.Name = "usePentaminoCheck";
            this.usePentaminoCheck.Size = new System.Drawing.Size(83, 17);
            this.usePentaminoCheck.TabIndex = 11;
            this.usePentaminoCheck.Text = "Пентамино";
            this.usePentaminoCheck.UseVisualStyleBackColor = true;
            // 
            // PrevSolButton
            // 
            this.PrevSolButton.Location = new System.Drawing.Point(264, 13);
            this.PrevSolButton.Name = "PrevSolButton";
            this.PrevSolButton.Size = new System.Drawing.Size(134, 57);
            this.PrevSolButton.TabIndex = 12;
            this.PrevSolButton.Text = "Предыдущее решение";
            this.PrevSolButton.UseVisualStyleBackColor = true;
            this.PrevSolButton.Click += new System.EventHandler(this.PrevSolButton_Click);
            // 
            // NextSolButton
            // 
            this.NextSolButton.Location = new System.Drawing.Point(413, 13);
            this.NextSolButton.Name = "NextSolButton";
            this.NextSolButton.Size = new System.Drawing.Size(134, 57);
            this.NextSolButton.TabIndex = 13;
            this.NextSolButton.Text = "Следующее решение";
            this.NextSolButton.UseVisualStyleBackColor = true;
            this.NextSolButton.Click += new System.EventHandler(this.NextSolButton_Click);
            // 
            // saveFormButton
            // 
            this.saveFormButton.Location = new System.Drawing.Point(979, 13);
            this.saveFormButton.Name = "saveFormButton";
            this.saveFormButton.Size = new System.Drawing.Size(107, 23);
            this.saveFormButton.TabIndex = 14;
            this.saveFormButton.Text = "Сохранить форму";
            this.saveFormButton.UseVisualStyleBackColor = true;
            this.saveFormButton.Click += new System.EventHandler(this.saveFormButton_Click);
            // 
            // loadFormButton
            // 
            this.loadFormButton.Location = new System.Drawing.Point(979, 39);
            this.loadFormButton.Name = "loadFormButton";
            this.loadFormButton.Size = new System.Drawing.Size(107, 23);
            this.loadFormButton.TabIndex = 15;
            this.loadFormButton.Text = "Загрузить форму";
            this.loadFormButton.UseVisualStyleBackColor = true;
            this.loadFormButton.Click += new System.EventHandler(this.loadFormButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(121, 13);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(113, 57);
            this.stopButton.TabIndex = 16;
            this.stopButton.Text = "Остановить поиск";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // aboutProgram
            // 
            this.aboutProgram.Location = new System.Drawing.Point(1111, 39);
            this.aboutProgram.Name = "aboutProgram";
            this.aboutProgram.Size = new System.Drawing.Size(98, 23);
            this.aboutProgram.TabIndex = 17;
            this.aboutProgram.Text = "О программе";
            this.aboutProgram.UseVisualStyleBackColor = true;
            this.aboutProgram.Click += new System.EventHandler(this.aboutProgram_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Количество строк";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Количество столбцов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Используемые объекты:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Используемые опции:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 624);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.aboutProgram);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.loadFormButton);
            this.Controls.Add(this.saveFormButton);
            this.Controls.Add(this.NextSolButton);
            this.Controls.Add(this.PrevSolButton);
            this.Controls.Add(this.usePentaminoCheck);
            this.Controls.Add(this.useTetraminoCheck);
            this.Controls.Add(this.useTriminoCheck);
            this.Controls.Add(this.isModAlgBox);
            this.Controls.Add(this.blankCellsCheck);
            this.Controls.Add(this.buildAreaButton);
            this.Controls.Add(this.colBox);
            this.Controls.Add(this.rowBox);
            this.Controls.Add(this.startAlgButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Программа составления фигур из пентамино";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button startAlgButton;
        private System.Windows.Forms.TextBox rowBox;
        private System.Windows.Forms.TextBox colBox;
        private System.Windows.Forms.Button buildAreaButton;
        private System.Windows.Forms.CheckBox blankCellsCheck;
        private System.Windows.Forms.CheckBox isModAlgBox;
        private System.Windows.Forms.CheckBox useTriminoCheck;
        private System.Windows.Forms.CheckBox useTetraminoCheck;
        private System.Windows.Forms.CheckBox usePentaminoCheck;
        private System.Windows.Forms.Button PrevSolButton;
        private System.Windows.Forms.Button NextSolButton;
        private System.Windows.Forms.Button saveFormButton;
        private System.Windows.Forms.Button loadFormButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button aboutProgram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

