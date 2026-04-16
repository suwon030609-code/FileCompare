namespace FileCompare
{
    public partial class Form1 : Form
    {
        private Dictionary<string, FileInfo> leftFiles = new Dictionary<string, FileInfo>();
        private Dictionary<string, FileInfo> rightFiles = new Dictionary<string, FileInfo>();
        public Form1()
        {
            InitializeComponent();

        }



        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "왼쪽 폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                    Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    PopulateFileList(lvwLeftDir, dlg.SelectedPath, leftFiles);
                    CompareAndColorize();
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "오른쪽 폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                    Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    PopulateFileList(lvwRightDir, dlg.SelectedPath, rightFiles);
                    CompareAndColorize();
                }
            }
        }
        private void PopulateFileList(ListView lv, string folderPath, Dictionary<string, FileInfo> fileDict)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            fileDict.Clear();

            try
            {
                var files = Directory.EnumerateFiles(folderPath)
                                     .Select(path => new FileInfo(path))
                                     .OrderBy(f => f.Name);

                foreach (var file in files)
                {
                    fileDict[file.Name] = file;

                    var item = new ListViewItem(file.Name);
                    item.SubItems.Add($"{file.Length:N0} 바이트");
                    item.SubItems.Add(file.LastWriteTime.ToString("yyyy-MM-dd tt h:mm"));
                    lv.Items.Add(item);
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("폴더를 찾을 수 없습니다.");
            }
            catch (IOException ex)
            {
                MessageBox.Show("입출력 오류: " + ex.Message);
            }
            finally
            {
                lv.EndUpdate();
            }
        }
        private void CompareAndColorize()
        {
            ColorizeList(lvwLeftDir, leftFiles, rightFiles);
            ColorizeList(lvwRightDir, rightFiles, leftFiles);
        }
        private void ColorizeList(ListView lv, Dictionary<string, FileInfo> myFiles, Dictionary<string, FileInfo> otherFiles)
        {
            foreach (ListViewItem item in lv.Items)
            {
                string fileName = item.Text;

                if (!myFiles.ContainsKey(fileName))
                    continue;

                FileInfo myFile = myFiles[fileName];

                if (otherFiles.ContainsKey(fileName))
                {
                    FileInfo otherFile = otherFiles[fileName];

                    if (myFile.LastWriteTime == otherFile.LastWriteTime)
                    {
                        item.ForeColor = Color.Black;
                    }
                    else if (myFile.LastWriteTime > otherFile.LastWriteTime)
                    {
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    item.ForeColor = Color.Purple;
                }
            }
        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) || string.IsNullOrWhiteSpace(txtRightDir.Text))
            {
                MessageBox.Show("먼저 양쪽 폴더를 선택하세요.");
                return;
            }

            if (lvwLeftDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 왼쪽에서 선택하세요.");
                return;
            }

            foreach (ListViewItem item in lvwLeftDir.SelectedItems)
            {
                string fileName = item.Text;

                if (!leftFiles.ContainsKey(fileName))
                    continue;

                FileInfo src = leftFiles[fileName];
                string destPath = Path.Combine(txtRightDir.Text, src.Name);

                CopyFileWithConfirmation(src.FullName, destPath);
            }

            PopulateFileList(lvwLeftDir, txtLeftDir.Text, leftFiles);
            PopulateFileList(lvwRightDir, txtRightDir.Text, rightFiles);
            CompareAndColorize();
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) || string.IsNullOrWhiteSpace(txtRightDir.Text))
            {
                MessageBox.Show("먼저 양쪽 폴더를 선택하세요.");
                return;
            }

            if (lvwRightDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 오른쪽에서 선택하세요.");
                return;
            }

            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                string fileName = item.Text;

                if (!rightFiles.ContainsKey(fileName))
                    continue;

                FileInfo src = rightFiles[fileName];
                string destPath = Path.Combine(txtLeftDir.Text, src.Name);

                CopyFileWithConfirmation(src.FullName, destPath);
            }

            PopulateFileList(lvwLeftDir, txtLeftDir.Text, leftFiles);
            PopulateFileList(lvwRightDir, txtRightDir.Text, rightFiles);
            CompareAndColorize();
        }
        private void CopyFileWithConfirmation(string srcPath, string destPath)
        {
            FileInfo srcInfo = new FileInfo(srcPath);

            if (File.Exists(destPath))
            {
                FileInfo destInfo = new FileInfo(destPath);

                string message =
                    $"대상 폴더에 동일한 이름의 파일이 이미 있습니다.\n" +
                    $"덮어쓰시겠습니까?\n\n" +
                    $"원본 파일 수정일: {srcInfo.LastWriteTime}\n" +
                    $"대상 파일 수정일: {destInfo.LastWriteTime}\n\n" +
                    $"원본: {srcPath}\n" +
                    $"대상: {destPath}";

                DialogResult result = MessageBox.Show(
                    message,
                    "덮어쓰기 확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            File.Copy(srcPath, destPath, true);
        }
    }
}
