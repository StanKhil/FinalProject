namespace FinalProject
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
            start = new Button();
            current = new Label();
            textBox1 = new TextBox();
            add = new Button();
            inputWords = new TextBox();
            delete = new Button();
            wordsList = new ListBox();
            process = new ProgressBar();
            startDirectory = new Label();
            startDirectoryInput = new TextBox();
            SuspendLayout();
            // 
            // start
            // 
            start.Location = new Point(361, 275);
            start.Name = "start";
            start.Size = new Size(75, 23);
            start.TabIndex = 0;
            start.Text = "Start";
            start.UseVisualStyleBackColor = true;
            start.Click += start_Click;
            // 
            // current
            // 
            current.AutoSize = true;
            current.Location = new Point(142, 275);
            current.Name = "current";
            current.Size = new Size(0, 15);
            current.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(70, 359);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(677, 23);
            textBox1.TabIndex = 2;
            // 
            // add
            // 
            add.Location = new Point(705, 82);
            add.Name = "add";
            add.Size = new Size(61, 40);
            add.TabIndex = 4;
            add.Text = "Add";
            add.UseVisualStyleBackColor = true;
            add.Click += add_Click;
            // 
            // inputWords
            // 
            inputWords.Location = new Point(558, 137);
            inputWords.Name = "inputWords";
            inputWords.Size = new Size(120, 23);
            inputWords.TabIndex = 5;
            // 
            // delete
            // 
            delete.Location = new Point(705, 28);
            delete.Name = "delete";
            delete.Size = new Size(61, 40);
            delete.TabIndex = 6;
            delete.Text = "Delete";
            delete.UseVisualStyleBackColor = true;
            delete.Click += delete_Click;
            // 
            // wordsList
            // 
            wordsList.FormattingEnabled = true;
            wordsList.Location = new Point(558, 28);
            wordsList.Name = "wordsList";
            wordsList.Size = new Size(120, 94);
            wordsList.TabIndex = 7;
            wordsList.DoubleClick += wordsList_DoubleClick;
            // 
            // process
            // 
            process.Location = new Point(331, 317);
            process.Name = "process";
            process.Size = new Size(136, 23);
            process.TabIndex = 9;
            // 
            // startDirectory
            // 
            startDirectory.AutoSize = true;
            startDirectory.Location = new Point(104, 95);
            startDirectory.Name = "startDirectory";
            startDirectory.Size = new Size(82, 15);
            startDirectory.TabIndex = 10;
            startDirectory.Text = "Start Directory";
            // 
            // startDirectoryInput
            // 
            startDirectoryInput.Location = new Point(104, 137);
            startDirectoryInput.Name = "startDirectoryInput";
            startDirectoryInput.Size = new Size(289, 23);
            startDirectoryInput.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(startDirectoryInput);
            Controls.Add(startDirectory);
            Controls.Add(process);
            Controls.Add(wordsList);
            Controls.Add(delete);
            Controls.Add(inputWords);
            Controls.Add(add);
            Controls.Add(textBox1);
            Controls.Add(current);
            Controls.Add(start);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button start;
        private Label current;
        private TextBox textBox1;
        private Button add;
        private TextBox inputWords;
        private Button delete;
        private ListBox wordsList;
        private ProgressBar process;
        private Label startDirectory;
        private TextBox startDirectoryInput;
    }
}
