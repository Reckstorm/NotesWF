using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace NotesWF
{
    public partial class Form1 : Form
    {
        private string _path = "";
        private bool _dayOrNight = false;
        private TextBox _textBox = new TextBox();
        private Panel _panel = new Panel();
        private Button _save = new Button();
        private Button _bigger = new Button();
        private Button _smaller = new Button();
        private Button _clear = new Button();
        private Button _undo = new Button();
        private Button _dayNight = new Button();

        private MenuStrip menu = new MenuStrip();
        private ToolStripMenuItem fileItem = new ToolStripMenuItem();
        private ToolStripMenuItem open = new ToolStripMenuItem();
        private ToolStripMenuItem save = new ToolStripMenuItem();
        private ToolStripMenuItem print = new ToolStripMenuItem();
        private ToolStripMenuItem exit = new ToolStripMenuItem();
        public Form1()
        {
            InitializeComponent();
            Load += NyMote;
            this.FormClosing += (s, e) =>
            {
                if ((_path.Equals(string.Empty) && !_textBox.Text.Equals(string.Empty)) || (!_path.Equals(string.Empty) && !_textBox.Text.Equals(File.ReadAllText(_path))))
                {
                    DialogResult res = MessageBox.Show("Save changes?", "Close", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes) SaveToTxt(s, e);
                }
            };
        }

        private void NyMote(object sender, EventArgs e)
        {
            StripMenu();
            _panel.Location = new Point(0, 0);
            _panel.Width = 150;
            _panel.Height = this.Height;
            _panel.BackColor = Color.LightSlateGray;

            _textBox.Multiline = true;
            _textBox.ScrollBars = ScrollBars.Vertical;
            _textBox.BorderStyle = BorderStyle.None;
            _textBox.Width = this.Width - _panel.Width - 18;
            _textBox.Height = this.Height;
            _textBox.Location = new Point(_panel.Width, 27);
            _textBox.WordWrap = true;
            _textBox.Text = LoadFromTxt();

            _save.Location = new Point(25, 52);
            _save.Width = 100;
            _save.Height = 30;
            _save.Text = "Save";
            _save.ForeColor = Color.White;
            _save.Click += SaveToTxt;

            _bigger.Location = new Point(_save.Location.X, _save.Location.Y + _save.Height + 25);
            _bigger.Width = 100;
            _bigger.Height = 30;
            _bigger.Text = "Bigger";
            _bigger.ForeColor = Color.White;
            _bigger.Click += Bigger;

            _smaller.Location = new Point(_bigger.Location.X, _bigger.Location.Y + _bigger.Height + 25);
            _smaller.Width = 100;
            _smaller.Height = 30;
            _smaller.Text = "Smaller";
            _smaller.ForeColor = Color.White;
            _smaller.Click += Smaller;

            _clear.Location = new Point(_smaller.Location.X, _smaller.Location.Y + _smaller.Height + 25);
            _clear.Width = 100;
            _clear.Height = 30;
            _clear.Text = "Clear";
            _clear.ForeColor = Color.White;
            _clear.Click += Clear;

            _undo.Location = new Point(_clear.Location.X, _clear.Location.Y + _clear.Height + 25);
            _undo.Width = 100;
            _undo.Height = 30;
            _undo.Text = "Undo";
            _undo.ForeColor = Color.White;
            _undo.Click += Undo;

            _dayNight.Location = new Point(_undo.Location.X, _undo.Location.Y + _undo.Height + 25);
            _dayNight.Width = 100;
            _dayNight.Height = 30;
            _dayNight.Text = "Night";
            _dayNight.ForeColor = Color.White;
            _dayNight.Click += DayNight;

            _panel.Controls.Add(_save);
            _panel.Controls.Add(_clear);
            _panel.Controls.Add(_bigger);
            _panel.Controls.Add(_smaller);
            _panel.Controls.Add(_undo);
            _panel.Controls.Add(_dayNight);
            this.Controls.Add(_panel);
            this.Controls.Add(_textBox);
        }

        private void StripMenu()
        {
            menu.BackColor = Color.WhiteSmoke;
            menu.ForeColor = Color.Black;
            fileItem.Text = "File";
            fileItem.ShowShortcutKeys = true;

            open.Text = "Open";
            open.ShortcutKeys = Keys.Control | Keys.O;
            open.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "text|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _path = openFileDialog.FileName;
                }
            };

            save.Text = "Save as";
            save.ShortcutKeys = Keys.Control | Keys.S;
            save.Click += Save;

            print.Text = "Print";
            print.ShortcutKeys = Keys.Control | Keys.P;
            print.Click += (s, e) =>
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {

                }
            };
            exit.Text = "Exit";
            exit.ShortcutKeys = Keys.Alt | Keys.Q;
            exit.Click += (s, e) => this.Close();

            fileItem.DropDownItems.Add(open);
            fileItem.DropDownItems.Add(save);
            fileItem.DropDownItems.Add(print);
            fileItem.DropDownItems.Add(exit);
            menu.Items.Add(fileItem);
            this.Controls.Add(menu);
        }
        private void DayNight(object sender, EventArgs e)
        {
            if (!_dayOrNight)
            {
                _dayNight.Text = "Day";
                _panel.BackColor = Color.DarkSlateGray;
                _textBox.BackColor = Color.FromArgb(255, 28, 26, 26);
                _textBox.ForeColor = Color.WhiteSmoke;
                _dayOrNight = true;
            }
            else
            {
                _dayNight.Text = "Night";
                _panel.BackColor = Color.LightSlateGray;
                _textBox.BackColor = Color.WhiteSmoke;
                _textBox.ForeColor = Color.Black;
                _dayOrNight = false;
            }
        }
        private void Clear(object sender, EventArgs e)
        { 
            if (_textBox.Text.Length > 0) _textBox.Clear();
        }
        private void Undo(object sender, EventArgs e)
        {
            _textBox.Undo();
        }

        private void Bigger(object sender, EventArgs e)
        {
            _textBox.Font = new Font(_textBox.Font.FontFamily, _textBox.Font.Size + 2);
        }

        private void Smaller(object sender, EventArgs e)
        {
            _textBox.Font = new Font(_textBox.Font.FontFamily, _textBox.Font.Size - 2);
        }

        private void SaveToTxt(object sender, EventArgs e)
        {
            if (!_path.Equals(string.Empty))
            {
                File.WriteAllText(_path, _textBox.Text);
            }
            else
            {
                Save(sender, e);
            }
        }
        private void Save(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "text|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string temp = string.Empty;
                if (saveFileDialog.FileName.Length > 0)
                {
                    temp = $"{saveFileDialog.FileName}_Stats.txt";
                }
                string[] words = _textBox.Text.Trim().Split(' ');
                int count = 0;
                foreach (string word in words)
                {
                    if (word.Any(Char.IsLetter)) count++;
                }
                string content = $"Length:{_textBox.Text.Length}, Digits:{_textBox.Text.Count(x => Char.IsDigit(x))}, Punctuation:{_textBox.Text.Count(x => Char.IsPunctuation(x))}, Words:{count}";
                File.WriteAllText(saveFileDialog.FileName, _textBox.Text);
                File.WriteAllText(temp, content);
                _path = saveFileDialog.FileName;
            }
        }
        private string LoadFromTxt()
        {
            if (!File.Exists(_path)) return string.Empty;
            else if (File.ReadAllText(_path).Length == 0) return string.Empty;
            return File.ReadAllText(_path);
        }
    }
}