using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        private List<string> words = new List<string>();
        private CancellationTokenSource cts;
        private static Mutex mutex = new Mutex(true, "FinalProjectMutex");
        private ConcurrentDictionary<string, int> wordCount = new ConcurrentDictionary<string, int>();
        private List<string> files;
        private int lastProcessedIndex = 0;

        public Form1()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Програма вже запущена!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            InitializeComponent();
            startDirectoryInput.Text = @"C:\Users\user\";
            copyDirectoryInput.Text = @"C:\Users\user\";
        }

        private async void start_Click(object sender, EventArgs e)
        {
            if (words.Count == 0)
            {
                MessageBox.Show("Список слів порожній");
                return;
            }

            string startDirectory = startDirectoryInput.Text;
            if (!Directory.Exists(startDirectory))
            {
                MessageBox.Show("Вказана директорія не існує!");
                return;
            }

            cts = new CancellationTokenSource();
            var token = cts.Token;

            if (lastProcessedIndex == 0 || files == null)
            {
                files = await Task.Run(() => GetAllFiles(startDirectory));
                if (files.Count == 0)
                {
                    MessageBox.Show("Не знайдено жодного файлу.");
                    return;
                }
            }

            process.Maximum = files.Count;
            process.Value = lastProcessedIndex;
            wordCount.Clear();
            MessageBox.Show($"Знайдено {files.Count} файлів. Сканування розпочато");

            try
            {
                await Parallel.ForEachAsync(
                    files.Skip(lastProcessedIndex),
                    new ParallelOptions { CancellationToken = token, MaxDegreeOfParallelism = Environment.ProcessorCount },
                    async (file, _) =>
                    {
                        await ProcessFileAsync(file, token);
                        lastProcessedIndex++;
                    });

                GenerateReport(files);
                MessageBox.Show("Сканування завершено.");
                lastProcessedIndex = 0;
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Сканування призупинено.");
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Users\user\error_log.txt", $"Помилка під час сканування: {ex.Message}\n");
            }
        }

        private List<string> GetAllFiles(string root)
        {
            MessageBox.Show("Іде підготовка директорії, дочікуйтеся старту");
            List<string> allFiles = new List<string>();
            Queue<string> directories = new Queue<string>();
            directories.Enqueue(root);

            while (directories.Count > 0)
            {
                string currentDirectory = directories.Dequeue();
                try
                {
                    allFiles.AddRange(Directory.GetFiles(currentDirectory, "*.txt"));
                    foreach (var subDir in Directory.GetDirectories(currentDirectory))
                        directories.Enqueue(subDir);
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception ex) { }
            }

            return allFiles;
        }

        private async Task ProcessFileAsync(string file, CancellationToken token)
        {
            try
            {
                if (!File.Exists(file)) return;
                string content = await File.ReadAllTextAsync(file, token);
                string modifiedContent = content;
                bool modified = false;

                foreach (var word in words)
                {
                    int count = content.Split(new string[] { word }, StringSplitOptions.None).Length - 1;
                    if (count > 0)
                    {
                        wordCount.AddOrUpdate(word, count, (_, oldCount) => oldCount + count);
                        modifiedContent = modifiedContent.Replace(word, new string('*', word.Length));
                        modified = true;
                        //MessageBox.Show($"Файл містить слово {word} :  {Path.GetFileName(file)}\n{file}");
                    }
                }

                if (modified)
                {
                    await CreateCopyAsync(file, modifiedContent);
                }

                Invoke((MethodInvoker)(() => currentFile.Text = file));
                Invoke((MethodInvoker)(() => process.Value++));
            }
            catch (Exception ex) { }
        }

        private async Task CreateCopyAsync(string originalFilePath, string newContent)
        {
            try
            {
                string changedFilesDir = Path.Combine(copyDirectoryInput.Text, "ChangedFiles");
                Directory.CreateDirectory(changedFilesDir);
                string newFilePath = Path.Combine(changedFilesDir, Path.GetFileName(originalFilePath));
                await File.WriteAllTextAsync(newFilePath, newContent);
            }
            catch (Exception ex) { }
        }

        private void GenerateReport(List<string> files)
        {
            try
            {
                string reportPath = Path.Combine(copyDirectoryInput.Text, "scan_report.txt");
                var topWords = wordCount.OrderByDescending(x => x.Value).Take(10);
                using (StreamWriter writer = new StreamWriter(reportPath))
                {
                    writer.WriteLine("Звіт про сканування файлів");
                    writer.WriteLine("======================================");
                    writer.WriteLine($"Дата: {DateTime.Now}");
                    writer.WriteLine($"Кількість перевірених файлів: {files.Count}");
                    writer.WriteLine("======================================");
                    writer.WriteLine("Топ-10 заборонених слів:");
                    foreach (var item in topWords)
                    {
                        writer.WriteLine($"{item.Key}: {item.Value} разів");
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void pause_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            MessageBox.Show($"Сканування призупинено. Оброблено {lastProcessedIndex} файлів.");
        }

        private void resume_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Сканування відновлено");
            if (lastProcessedIndex < files.Count)
            {
                start_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Сканування вже завершено.");
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            MessageBox.Show("Сканування зупинено.");
            lastProcessedIndex = 0;
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
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
    }
}
