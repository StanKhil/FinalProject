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
            currentFile = new TextBox();
            add = new Button();
            inputWords = new TextBox();
            delete = new Button();
            wordsList = new ListBox();
            process = new ProgressBar();
            startDirectory = new Label();
            startDirectoryInput = new TextBox();
            pause = new Button();
            resume = new Button();
            stop = new Button();
            copyDirectory = new Label();
            copyDirectoryInput = new TextBox();
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
            // currentFile
            // 
            currentFile.Location = new Point(70, 359);
            currentFile.Name = "currentFile";
            currentFile.Size = new Size(677, 23);
            currentFile.TabIndex = 2;
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
            startDirectory.Location = new Point(104, 53);
            startDirectory.Name = "startDirectory";
            startDirectory.Size = new Size(82, 15);
            startDirectory.TabIndex = 10;
            startDirectory.Text = "Start Directory";
            // 
            // startDirectoryInput
            // 
            startDirectoryInput.Location = new Point(104, 82);
            startDirectoryInput.Name = "startDirectoryInput";
            startDirectoryInput.Size = new Size(289, 23);
            startDirectoryInput.TabIndex = 11;
            // 
            // pause
            // 
            pause.Location = new Point(142, 398);
            pause.Name = "pause";
            pause.Size = new Size(75, 27);
            pause.TabIndex = 12;
            pause.Text = "pause";
            pause.UseVisualStyleBackColor = true;
            pause.Click += pause_Click;
            // 
            // resume
            // 
            resume.Location = new Point(249, 398);
            resume.Name = "resume";
            resume.Size = new Size(75, 27);
            resume.TabIndex = 13;
            resume.Text = "resume";
            resume.UseVisualStyleBackColor = true;
            resume.Click += resume_Click;
            // 
            // stop
            // 
            stop.Location = new Point(361, 398);
            stop.Name = "stop";
            stop.Size = new Size(75, 27);
            stop.TabIndex = 14;
            stop.Text = "stop";
            stop.UseVisualStyleBackColor = true;
            stop.Click += stop_Click;
            // 
            // copyDirectory
            // 
            copyDirectory.AutoSize = true;
            copyDirectory.Location = new Point(104, 140);
            copyDirectory.Name = "copyDirectory";
            copyDirectory.Size = new Size(86, 15);
            copyDirectory.TabIndex = 15;
            copyDirectory.Text = "Copy Directory";
            // 
            // copyDirectoryInput
            // 
            copyDirectoryInput.Location = new Point(104, 172);
            copyDirectoryInput.Name = "copyDirectoryInput";
            copyDirectoryInput.Size = new Size(289, 23);
            copyDirectoryInput.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(copyDirectoryInput);
            Controls.Add(copyDirectory);
            Controls.Add(stop);
            Controls.Add(resume);
            Controls.Add(pause);
            Controls.Add(startDirectoryInput);
            Controls.Add(startDirectory);
            Controls.Add(process);
            Controls.Add(wordsList);
            Controls.Add(delete);
            Controls.Add(inputWords);
            Controls.Add(add);
            Controls.Add(currentFile);
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
        private TextBox currentFile;
        private Button add;
        private TextBox inputWords;
        private Button delete;
        private ListBox wordsList;
        private ProgressBar process;
        private Label startDirectory;
        private TextBox startDirectoryInput;
        private Button pause;
        private Button resume;
        private Button stop;
        private Label copyDirectory;
        private TextBox copyDirectoryInput;
    }
}
