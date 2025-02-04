using System;
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
        private Dictionary<string, int> wordCount = new Dictionary<string, int>();
        private int lastProcessedIndex = 0;
        private List<string> files;

        public Form1()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("�������� ��� ��������!", "�������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            InitializeComponent();
            startDirectoryInput.Text = @"C:\\Users\\user\\";
            copyDirectoryInput.Text = @"C:\\Users\\user\\";
        }

        private async void start_Click(object sender, EventArgs e)
        {
            if (words.Count == 0)
            {
                MessageBox.Show("������ ��� �������");
                return;
            }

            string startDirectory = startDirectoryInput.Text;
            if (!Directory.Exists(startDirectory))
            {
                MessageBox.Show("������� ��������� �� ����!");
                return;
            }

            cts = new CancellationTokenSource();
            var token = cts.Token;

            try
            {
                if (files == null || lastProcessedIndex == 0)
                {
                    MessageBox.Show("������� �����...");
                    files = new List<string>();
                    try
                    {
                        var directoriesQueue = new Queue<string>();
                        directoriesQueue.Enqueue(startDirectory);

                        while (directoriesQueue.Count > 0)
                        {
                            string currentDirectory = directoriesQueue.Dequeue();
                            try
                            {
                                files.AddRange(Directory.GetFiles(currentDirectory, "*.txt"));

                                var subDirectories = Directory.GetDirectories(currentDirectory);
                                foreach (var subDirectory in subDirectories)
                                {
                                    directoriesQueue.Enqueue(subDirectory);
                                }
                            }
                            catch (UnauthorizedAccessException) { }
                            catch (Exception ex)
                            {
                                File.AppendAllText(@"C:\\Users\\user\\error_log.txt", $"������� ��� ������� �������� {currentDirectory}: {ex.Message}\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(@"C:\\Users\\user\\error_log.txt", $"������� ��� ������� �����: {ex.Message}\n");
                    }
                    lastProcessedIndex = 0;
                }

                if (files.Count == 0)
                {
                    MessageBox.Show("�� �������� ������� �����.");
                    return;
                }

                process.Maximum = files.Count;
                process.Value = lastProcessedIndex;
                wordCount.Clear();
                try
                {
                    for (int i = lastProcessedIndex; i < files.Count; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            lastProcessedIndex = i;
                            return;
                        }
                        currentFile.Text = files[i];
                        try
                        {
                            await ProcessFileAsync(files[i], token);
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllText(@"C:\\Users\\user\\error_log.txt", $"������� ��� ������� {files[i]}: {ex.Message}\n");
                        }
                        process.Value++;
                    }
                }
                catch(Exception ex)
                {
                }


                lastProcessedIndex = 0;
                GenerateReport(files);
                MessageBox.Show("���������� ���������.");
            }
            catch (Exception ex)
            {
            }
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
                        wordCount[word] = wordCount.ContainsKey(word) ? wordCount[word] + count : count;
                        modifiedContent = modifiedContent.Replace(word, new string('*', word.Length));
                        modified = true;
                        MessageBox.Show($"���� ������ ����� {word}: {Path.GetFileName(file)}");
                    }
                }

                if (modified)
                {
                    await CreateCopyAsync(file, modifiedContent);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\\Users\\user\\error_log.txt", $"������� ��� ������� {file}: {ex.Message}\n");
            }
        }

        private async Task CreateCopyAsync(string originalFilePath, string newContent)
        {
            try
            {
                string changedFilesDir = copyDirectoryInput.Text + "ChangedFiles";
                Directory.CreateDirectory(changedFilesDir);
                string newFilePath = Path.Combine(changedFilesDir, Path.GetFileName(originalFilePath));
                await File.WriteAllTextAsync(newFilePath, newContent);
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\\Users\\user\\error_log.txt", $"������� ��� ��������� ����� {originalFilePath}: {ex.Message}\n");
            }
        }

        private void GenerateReport(List<string> files)
        {
            try
            {
                string reportPath = copyDirectoryInput.Text + "scan_report.txt";
                var topWords = wordCount.OrderByDescending(x => x.Value).Take(10);
                using (StreamWriter writer = new StreamWriter(reportPath))
                {
                    writer.WriteLine("��� ��� ���������� �����");
                    writer.WriteLine("======================================");
                    writer.WriteLine($"����: {DateTime.Now}");
                    writer.WriteLine($"ʳ������ ���������� �����: {files.Count}");
                    writer.WriteLine("======================================");
                    writer.WriteLine("���-10 ����������� ���:");
                    foreach (var item in topWords)
                    {
                        writer.WriteLine($"{item.Key}: {item.Value} ����");
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\\Users\\user\\error_log.txt", $"������� ��� �������� ����: {ex.Message}\n");
            }
        }

        private void pause_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            MessageBox.Show("���������� �����������.");
        }

        private void resume_Click(object sender, EventArgs e)
        {
            start_Click(sender, e);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
            lastProcessedIndex = 0;
            //files = null;
            MessageBox.Show("���������� ��������.");
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
