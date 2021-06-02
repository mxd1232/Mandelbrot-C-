
namespace Mandelbrot_Whole
{
    partial class Mandelbrot
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.serverIPText = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.serverPortText = new System.Windows.Forms.TextBox();
      this.connectButton = new System.Windows.Forms.Button();
      this.statusLabel = new System.Windows.Forms.Label();
      this.sendingButton = new System.Windows.Forms.Button();
      this.gif = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(700, 700);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
      this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label1.Location = new System.Drawing.Point(718, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(199, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "Connect to servers:";
      // 
      // serverIPText
      // 
      this.serverIPText.Location = new System.Drawing.Point(723, 67);
      this.serverIPText.Name = "serverIPText";
      this.serverIPText.Size = new System.Drawing.Size(194, 20);
      this.serverIPText.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label2.Location = new System.Drawing.Point(720, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(66, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "Server IP:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.label3.Location = new System.Drawing.Point(720, 103);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(77, 16);
      this.label3.TabIndex = 4;
      this.label3.Text = "Server port:";
      // 
      // serverPortText
      // 
      this.serverPortText.Location = new System.Drawing.Point(723, 122);
      this.serverPortText.Name = "serverPortText";
      this.serverPortText.Size = new System.Drawing.Size(194, 20);
      this.serverPortText.TabIndex = 5;
      // 
      // connectButton
      // 
      this.connectButton.Location = new System.Drawing.Point(842, 159);
      this.connectButton.Name = "connectButton";
      this.connectButton.Size = new System.Drawing.Size(75, 23);
      this.connectButton.TabIndex = 6;
      this.connectButton.Text = "Connect";
      this.connectButton.UseVisualStyleBackColor = true;
      this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
      // 
      // statusLabel
      // 
      this.statusLabel.AutoSize = true;
      this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.statusLabel.Location = new System.Drawing.Point(720, 159);
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(0, 16);
      this.statusLabel.TabIndex = 7;
      // 
      // sendingButton
      // 
      this.sendingButton.Location = new System.Drawing.Point(842, 616);
      this.sendingButton.Name = "sendingButton";
      this.sendingButton.Size = new System.Drawing.Size(75, 23);
      this.sendingButton.TabIndex = 8;
      this.sendingButton.Text = "Generate";
      this.sendingButton.UseVisualStyleBackColor = true;
      this.sendingButton.Click += new System.EventHandler(this.sendingButton_Click);
      // 
      // gif
      // 
      this.gif.Location = new System.Drawing.Point(842, 645);
      this.gif.Name = "gif";
      this.gif.Size = new System.Drawing.Size(75, 23);
      this.gif.TabIndex = 9;
      this.gif.Text = "Generate Gif";
      this.gif.UseVisualStyleBackColor = true;
      this.gif.Click += new System.EventHandler(this.gif_Click);
      // 
      // Mandelbrot
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(939, 728);
      this.Controls.Add(this.gif);
      this.Controls.Add(this.sendingButton);
      this.Controls.Add(this.statusLabel);
      this.Controls.Add(this.connectButton);
      this.Controls.Add(this.serverPortText);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.serverIPText);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.Name = "Mandelbrot";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Mandelbrot_Load);
      this.Shown += new System.EventHandler(this.Mandelbrot_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverIPText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverPortText;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button sendingButton;
    private System.Windows.Forms.Button gif;
  }
}

