using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        List<string> words = new List<string>();
        public Form1()
        {
            InitializeComponent();
            startDirectoryInput.Text = @"C:\Users\user\";
        }

        private async void start_Click(object sender, EventArgs e)
        {
            if(words.Count == 0)
            {
                MessageBox.Show("Words list is empty");
                return;
            }
            string rootPath = startDirectoryInput.Text;

            try
            {
                var directoriesQueue = new Queue<string>();
                directoriesQueue.Enqueue(rootPath);
                List<string> allFiles = new List<string>();

                MessageBox.Show("Іде підготовка директорії, дочікуйтеся старту");

                while (directoriesQueue.Count > 0)
                {
                    string currentDirectory = directoriesQueue.Dequeue();
                    try
                    {
                        allFiles.AddRange(Directory.GetFiles(currentDirectory, "*.txt"));

                        var subDirectories = Directory.GetDirectories(currentDirectory);
                        foreach (var subDirectory in subDirectories)
                        {
                            directoriesQueue.Enqueue(subDirectory);
                        }
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"Ошибка при обработке директории {currentDirectory}: {ex.Message}");
                    }
                }

                process.Maximum = allFiles.Count;
                process.Value = 0;


                foreach (string file in allFiles)
                {
                    try
                    {
                        textBox1.Text = file;
                        await ReadFileAsync(file);
                        process.Value++;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"Ошибка при обработке файла {file}: {ex.Message}");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            MessageBox.Show("Сканирование завершено.");
        }

        private async Task ReadFileAsync(string file)
        {
            string content = await File.ReadAllTextAsync(file);
            string fileName = Path.GetFileName(file);
            string newText = content;

            foreach (var word in words)
            {
                if (content.Contains(word))
                {
                    MessageBox.Show($"Файл содержит слово {word}: {fileName}\n{file}");
                    newText = newText.Replace(word, new string('*', word.Length));
                }
            }

            if (!content.Equals(newText))
            {
                await CreateCopyAsync(file, newText);
            }
        }


        private void add_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Слово добавлено в список");
            if (!string.IsNullOrWhiteSpace(inputWords.Text))
            {
                wordsList.Items.Add(inputWords.Text);
                words.Add(inputWords.Text);
                inputWords.Clear();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (wordsList.SelectedItem != null)
            {
                words.Remove(wordsList.SelectedItem.ToString());
                wordsList.Items.Remove(wordsList.SelectedItem);
                
            }
        }

        private void wordsList_DoubleClick(object sender, EventArgs e)
        {
            if (wordsList.SelectedItem != null)
            {
                words.Remove(wordsList.SelectedItem.ToString());
                wordsList.Items.Remove(wordsList.SelectedItem);
                
            }
        }

        private async Task CreateCopyAsync(string originalFilePath, string newContent)
        {
            try
            {
                string changedFilesDir = @"C:\Users\user\ChangedFiles";
                if (!Directory.Exists(changedFilesDir))
                {
                    Directory.CreateDirectory(changedFilesDir);
                }
                string fileName = Path.GetFileName(originalFilePath);
                string newFilePath = Path.Combine(changedFilesDir, fileName);

                await File.WriteAllTextAsync(newFilePath, newContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании копии файла: {ex.Message}");
            }
        }

    }
}
