namespace NotesWF
{
    public partial class Form1 : Form
    {
        private string _path = "C:\\Users\\Admin\\Desktop\\NotesWF\\MyNote.txt";
        private TextBox _textBox = new TextBox();
        private Panel _panel = new Panel();
        private Button _save = new Button();
        private Button _bigger = new Button();
        private Button _smaller = new Button();
        private Button _clear = new Button();
        private Button _undo = new Button();
        public Form1()
        {
            InitializeComponent();
            Load += NyMote;
            this.FormClosing += (s, e) =>
            {
                if (!_textBox.Text.Equals(File.ReadAllText(_path)))
                {
                    DialogResult res = MessageBox.Show("Save changes?", "Close", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes) SaveToTxt(s, e);
                }
            };
        }

        private void NyMote(object sender, EventArgs e)
        {
            _panel.Location = new Point(0, 0);
            _panel.Width = 150;
            _panel.Height = this.Height;
            _panel.BackColor = Color.DarkSlateGray;

            _textBox.Multiline = true;
            _textBox.ScrollBars = ScrollBars.Vertical;
            _textBox.Width = this.Width - _panel.Width - 18;
            _textBox.Height = this.Height;
            _textBox.Location = new Point(_panel.Width, 0);
            _textBox.WordWrap = true;
            _textBox.Text = LoadFromTxt();

            _save.Location = new Point(25, 25);
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

            _panel.Controls.Add(_save);
            _panel.Controls.Add(_clear);
            _panel.Controls.Add(_bigger);
            _panel.Controls.Add(_smaller);
            _panel.Controls.Add(_undo);
            this.Controls.Add(_panel);
            this.Controls.Add(_textBox);
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
            File.WriteAllText(_path, _textBox.Text);
        }
        private string LoadFromTxt()
        {
            if (!File.Exists(_path)) return string.Empty;
            else if (File.ReadAllText(_path).Length == 0) return string.Empty;
            return File.ReadAllText(_path);
        }
    }
}