using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace GetLrcsForSongs
{
    public partial class BetterTextBox : TextBox
    {
        public BetterTextBox()
        {
            InitializeComponent();
        }
        public int Value
        {
            get
            {
                if (IsDigital)
                    return int.Parse(Text);
                throw new Exception("文本框不是数值类型,请检查IsDigital属性.");
            }
        }

        public override string Text
        {
            get { return base.Text.Trim(); }
            set { base.Text = value; }
        }
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private string watermarkText;
        private bool isDigital = false;
        private int maxInput = int.MaxValue;

        public string WatermarkText
        {
            get { return watermarkText; }
            set
            {
                watermarkText = value;
                SetWatermark(watermarkText);
            }
        }

        public bool IsDigital
        {
            get
            {
                return isDigital;
            }

            set
            {
                isDigital = value;
            }
        }

        public int MaxInput
        {
            get
            {
                return maxInput;
            }

            set
            {
                maxInput = value;
            }
        }

        private void SetWatermark(string watermarkText)
        {
            SendMessage(Handle, EM_SETCUEBANNER, 0, watermarkText);
        }
        private void BetterTextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Tag = 1;
            textBox.SelectAll();
        }
        private void BetterTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if ((int)textBox.Tag == 1)
            {
                textBox.SelectAll();
                textBox.Tag = 0;
            }
        }
        private void BetterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
                return;
            }
            if (IsDigital && !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
                return;
            }

        }
        private void BetterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;

                if (IsDigital)
                {
                    int numInput = Convert.ToInt32(textBox.Text);

                    if (numInput > MaxInput)
                    {
                        textBox.TextChanged -= BetterTextBox_TextChanged;
                        textBox.Text = maxInput.ToString();
                        textBox.TextChanged += BetterTextBox_TextChanged;
                        SendKeys.Send("{TAB}");
                    }
                    else if (textBox.TextLength == MaxLength)
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                else if (textBox.TextLength == MaxLength)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception)
            {

            }

        }
    }
}


