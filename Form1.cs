namespace FileCompare
{
    public partial class Form1 : Form
    {
        private Dictionary<string, FileInfo> leftFiles = new Dictionary<string, FileInfo>();
        private Dictionary<string, FileInfo> rightFiles = new Dictionary<string, FileInfo>();
        private Dictionary<string, DirectoryInfo> leftDirs = new Dictionary<string, DirectoryInfo>();
        private Dictionary<string, DirectoryInfo> rightDirs = new Dictionary<string, DirectoryInfo>();
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
                    PopulateEntryList(lvwLeftDir, dlg.SelectedPath, leftFiles, leftDirs);
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
                    PopulateEntryList(lvwRightDir, dlg.SelectedPath, rightFiles, rightDirs);
                    CompareAndColorize();
                }
            }
        }
        private void PopulateEntryList(
    ListView lv,
    string folderPath,
    Dictionary<string, FileInfo> fileDict,
    Dictionary<string, DirectoryInfo> dirDict)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            fileDict.Clear();
            dirDict.Clear();

            try
            {
                var dirs = Directory.EnumerateDirectories(folderPath)
                                    .Select(path => new DirectoryInfo(path))
                                    .OrderBy(d => d.Name);

                foreach (var dir in dirs)
                {
                    dirDict[dir.Name] = dir;

                    var item = new ListViewItem(dir.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(dir.LastWriteTime.ToString("yyyy-MM-dd tt h:mm"));
                    lv.Items.Add(item);
                }

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
            ColorizeEntryList(lvwLeftDir, leftFiles, rightFiles, leftDirs, rightDirs);
            ColorizeEntryList(lvwRightDir, rightFiles, leftFiles, rightDirs, leftDirs);
        }
        private void ColorizeEntryList(
    ListView lv,
    Dictionary<string, FileInfo> myFiles,
    Dictionary<string, FileInfo> otherFiles,
    Dictionary<string, DirectoryInfo> myDirs,
    Dictionary<string, DirectoryInfo> otherDirs)
        {
            foreach (ListViewItem item in lv.Items)
            {
                string name = item.Text;

                // 1. 파일인 경우
                if (myFiles.ContainsKey(name))
                {
                    FileInfo myFile = myFiles[name];

                    if (otherFiles.ContainsKey(name))
                    {
                        FileInfo otherFile = otherFiles[name];

                        if (myFile.LastWriteTime == otherFile.LastWriteTime)
                            item.ForeColor = Color.Black;
                        else if (myFile.LastWriteTime > otherFile.LastWriteTime)
                            item.ForeColor = Color.Red;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;
                    }
                }
                // 2. 폴더인 경우
                else if (myDirs.ContainsKey(name))
                {
                    DirectoryInfo myDir = myDirs[name];

                    if (otherDirs.ContainsKey(name))
                    {
                        DirectoryInfo otherDir = otherDirs[name];

                        if (myDir.LastWriteTime == otherDir.LastWriteTime)
                            item.ForeColor = Color.Black;
                        else if (myDir.LastWriteTime > otherDir.LastWriteTime)
                            item.ForeColor = Color.Red;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;
                    }
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

            // 복사 확인 메시지 박스 및 아니요 선택 시 취소 처리 추가
            if (MessageBox.Show("지금 복사하시겠습니까?", "복사 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            foreach (ListViewItem item in lvwLeftDir.SelectedItems)
            {
                string fileName = item.Text;

                if (!leftFiles.ContainsKey(fileName))
                    continue;

                FileInfo src = leftFiles[fileName];
                string destPath = Path.Combine(txtRightDir.Text, src.Name);

                if (!CopyFileWithConfirmation(src.FullName, destPath))
                {
                    break;
                }
            }

            // CS7036 오류 수정: dirDict 매개변수 누락 추가 (leftDirs, rightDirs)
            PopulateEntryList(lvwLeftDir, txtLeftDir.Text, leftFiles, leftDirs);
            PopulateEntryList(lvwRightDir, txtRightDir.Text, rightFiles, rightDirs);
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

            // 복사 확인 메시지 박스 및 아니요 선택 시 취소 처리 추가
            if (MessageBox.Show("지금 복사하시겠습니까?", "복사 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                string fileName = item.Text;

                if (!rightFiles.ContainsKey(fileName))
                    continue;

                FileInfo src = rightFiles[fileName];
                string destPath = Path.Combine(txtLeftDir.Text, src.Name);

                if (!CopyFileWithConfirmation(src.FullName, destPath))
                {
                    break;
                }
            }

            // CS7036 오류 수정: dirDict 매개변수 누락 추가 (leftDirs, rightDirs)
            PopulateEntryList(lvwLeftDir, txtLeftDir.Text, leftFiles, leftDirs);
            PopulateEntryList(lvwRightDir, txtRightDir.Text, rightFiles, rightDirs);
            CompareAndColorize();
        }
        private bool CopyFileWithConfirmation(string srcPath, string destPath)
        {
            FileInfo srcInfo = new FileInfo(srcPath);

            if (File.Exists(destPath))
            {
                FileInfo destInfo = new FileInfo(destPath);

                // 원본 파일이 대상 파일보다 최신인 경우 확인 메시지를 생략합니다.
                if (srcInfo.LastWriteTime <= destInfo.LastWriteTime)
                {
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
                        return false;
                }
            }

            File.Copy(srcPath, destPath, true);
            return true;
        }
        private void CopyDirectoryRecursive(string sourceDir, string targetDir)
        {
            // 대상 폴더가 없으면 만든다
            Directory.CreateDirectory(targetDir);

            // 현재 폴더 안의 파일 복사
            foreach (string filePath in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(filePath);
                string destFilePath = Path.Combine(targetDir, fileName);
                File.Copy(filePath, destFilePath, true);
            }

            // 현재 폴더 안의 하위 폴더 복사
            foreach (string dirPath in Directory.GetDirectories(sourceDir))
            {
                string dirName = Path.GetFileName(dirPath);
                string destSubDir = Path.Combine(targetDir, dirName);

                // 자기 자신을 다시 호출
                CopyDirectoryRecursive(dirPath, destSubDir);
            }
        }
    }
}
