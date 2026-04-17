using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        private Dictionary<string, FileInfo> leftFiles =
            new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, FileInfo> rightFiles =
            new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, DirectoryInfo> leftDirs =
            new Dictionary<string, DirectoryInfo>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, DirectoryInfo> rightDirs =
            new Dictionary<string, DirectoryInfo>(StringComparer.OrdinalIgnoreCase);

        public Form1()
        {
            InitializeComponent();

            InitializeListView(lvwLeftDir);
            InitializeListView(lvwRightDir);
        }

        private void InitializeListView(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.MultiSelect = true;
            lv.HideSelection = false;

            lv.Columns.Clear();
            lv.Columns.Add("이름", 260);
            lv.Columns.Add("크기", 110);
            lv.Columns.Add("수정일", 170);
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
                MessageBox.Show("폴더를 찾을 수 없습니다.", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("입출력 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (myFiles.ContainsKey(name))
                {
                    FileInfo myFile = myFiles[name];

                    if (otherFiles.ContainsKey(name))
                    {
                        FileInfo otherFile = otherFiles[name];

                        if (myFile.LastWriteTime == otherFile.LastWriteTime)
                            item.ForeColor = Color.Black;   // 동일
                        else if (myFile.LastWriteTime > otherFile.LastWriteTime)
                            item.ForeColor = Color.Red;     // New
                        else
                            item.ForeColor = Color.Gray;    // Old
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;      // 단독 파일
                    }
                }
                else if (myDirs.ContainsKey(name))
                {
                    DirectoryInfo myDir = myDirs[name];

                    if (otherDirs.ContainsKey(name))
                    {
                        DirectoryInfo otherDir = otherDirs[name];

                        if (myDir.LastWriteTime == otherDir.LastWriteTime)
                            item.ForeColor = Color.Black;   // 동일
                        else if (myDir.LastWriteTime > otherDir.LastWriteTime)
                            item.ForeColor = Color.Red;     // New
                        else
                            item.ForeColor = Color.Gray;    // Old
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;      // 단독 폴더
                    }
                }
            }
        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) || string.IsNullOrWhiteSpace(txtRightDir.Text))
            {
                MessageBox.Show("먼저 양쪽 폴더를 선택하세요.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lvwLeftDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("왼쪽 목록에서 복사할 항목을 선택하세요.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ListViewItem item in lvwLeftDir.SelectedItems)
            {
                string name = item.Text;

                if (leftFiles.ContainsKey(name))
                {
                    FileInfo src = leftFiles[name];
                    string destPath = Path.Combine(txtRightDir.Text, src.Name);
                    CopyFileSmart(src.FullName, destPath);
                }
                else if (leftDirs.ContainsKey(name))
                {
                    DirectoryInfo srcDir = leftDirs[name];
                    string destDir = Path.Combine(txtRightDir.Text, srcDir.Name);
                    CopyDirectoryRecursiveSmart(srcDir.FullName, destDir);
                }
            }

            RefreshBothLists();
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) || string.IsNullOrWhiteSpace(txtRightDir.Text))
            {
                MessageBox.Show("먼저 양쪽 폴더를 선택하세요.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lvwRightDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("오른쪽 목록에서 복사할 항목을 선택하세요.", "안내",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                string name = item.Text;

                if (rightFiles.ContainsKey(name))
                {
                    FileInfo src = rightFiles[name];
                    string destPath = Path.Combine(txtLeftDir.Text, src.Name);
                    CopyFileSmart(src.FullName, destPath);
                }
                else if (rightDirs.ContainsKey(name))
                {
                    DirectoryInfo srcDir = rightDirs[name];
                    string destDir = Path.Combine(txtLeftDir.Text, srcDir.Name);
                    CopyDirectoryRecursiveSmart(srcDir.FullName, destDir);
                }
            }

            RefreshBothLists();
        }

        private void CopyFileSmart(string srcPath, string destPath)
        {
            try
            {
                FileInfo srcInfo = new FileInfo(srcPath);

                // 대상 파일이 없으면 바로 복사
                if (!File.Exists(destPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath)!);
                    File.Copy(srcPath, destPath, true);
                    return;
                }

                FileInfo destInfo = new FileInfo(destPath);

                // New -> Old 는 확인 없이 바로 복사
                if (srcInfo.LastWriteTime > destInfo.LastWriteTime)
                {
                    File.Copy(srcPath, destPath, true);
                    return;
                }

                // Old -> New 또는 같으면 반드시 확인
                string message =
                    $"대상 폴더에 동일한 이름의 파일이 이미 있습니다.\n" +
                    $"현재 복사하려는 파일이 대상 파일보다 최신이 아닙니다.\n" +
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

                File.Copy(srcPath, destPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 복사 중 오류가 발생했습니다.\n" + ex.Message,
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyDirectoryRecursiveSmart(string sourceDir, string targetDir)
        {
            try
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(sourceDir);

                // 대상 폴더가 없으면 생성
                Directory.CreateDirectory(targetDir);

                // 현재 폴더 안의 파일 복사
                foreach (string filePath in Directory.GetFiles(sourceDir))
                {
                    string fileName = Path.GetFileName(filePath);
                    string destFilePath = Path.Combine(targetDir, fileName);

                    CopyFileSmart(filePath, destFilePath);
                }

                // 현재 폴더 안의 하위 폴더 복사
                foreach (string dirPath in Directory.GetDirectories(sourceDir))
                {
                    string dirName = Path.GetFileName(dirPath);
                    string destSubDir = Path.Combine(targetDir, dirName);

                    CopyDirectoryRecursiveSmart(dirPath, destSubDir);
                }

                // 핵심: 폴더 자체의 수정시간을 원본과 같게 맞춤
                Directory.SetLastWriteTime(targetDir, sourceInfo.LastWriteTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show("폴더 복사 중 오류가 발생했습니다.\n" + ex.Message,
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshBothLists()
        {
            if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) && Directory.Exists(txtLeftDir.Text))
            {
                PopulateEntryList(lvwLeftDir, txtLeftDir.Text, leftFiles, leftDirs);
            }

            if (!string.IsNullOrWhiteSpace(txtRightDir.Text) && Directory.Exists(txtRightDir.Text))
            {
                PopulateEntryList(lvwRightDir, txtRightDir.Text, rightFiles, rightDirs);
            }

            CompareAndColorize();
        }
    }
}