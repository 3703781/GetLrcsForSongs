namespace GetLrcsForSongs
{
    partial class BetterTextBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BetterTextBox
            // 
            this.Name = "BetterTextBox";
            this.Enter += BetterTextBox_Enter;
            this.MouseUp += BetterTextBox_MouseUp;
            this.KeyPress += BetterTextBox_KeyPress;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10);
            this.TextChanged += BetterTextBox_TextChanged;
            this.ResumeLayout(true);
        }
        #endregion
    }
}
