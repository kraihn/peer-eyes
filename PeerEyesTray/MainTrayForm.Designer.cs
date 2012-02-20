namespace PeerEyesTray
{
    partial class MainTrayForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainTrayForm));
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nicTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiPeers = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiPeers,
            this.tmiExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(103, 48);
            // 
            // nicTray
            // 
            this.nicTray.ContextMenuStrip = this.cmsTray;
            this.nicTray.Icon = ((System.Drawing.Icon)(resources.GetObject("nicTray.Icon")));
            this.nicTray.Text = "Peer Eyes";
            this.nicTray.Visible = true;
            this.nicTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nicTray_MouseClick);
            // 
            // tmiExit
            // 
            this.tmiExit.Name = "tmiExit";
            this.tmiExit.Size = new System.Drawing.Size(102, 22);
            this.tmiExit.Text = "Exit";
            // 
            // tmiPeers
            // 
            this.tmiPeers.Name = "tmiPeers";
            this.tmiPeers.Size = new System.Drawing.Size(102, 22);
            this.tmiPeers.Text = "Peers";
            // 
            // MainTrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainTrayForm";
            this.ShowInTaskbar = false;
            this.Text = "Peer Eyes";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.NotifyIcon nicTray;
        private System.Windows.Forms.ToolStripMenuItem tmiPeers;
        private System.Windows.Forms.ToolStripMenuItem tmiExit;

    }
}

