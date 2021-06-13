
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
            this.animationButton = new System.Windows.Forms.Button();
            this.speedTestButton = new System.Windows.Forms.Button();
            this.framesText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.iterationsText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.heightText = new System.Windows.Forms.TextBox();
            this.widthText = new System.Windows.Forms.TextBox();
            this.changeButton = new System.Windows.Forms.Button();
            this.generateAllButton = new System.Windows.Forms.Button();
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
            this.sendingButton.Location = new System.Drawing.Point(723, 601);
            this.sendingButton.Name = "sendingButton";
            this.sendingButton.Size = new System.Drawing.Size(75, 23);
            this.sendingButton.TabIndex = 8;
            this.sendingButton.Text = "Zoom";
            this.sendingButton.UseVisualStyleBackColor = true;
            this.sendingButton.Click += new System.EventHandler(this.sendingButton_Click);
            // 
            // animationButton
            // 
            this.animationButton.Location = new System.Drawing.Point(842, 630);
            this.animationButton.Name = "animationButton";
            this.animationButton.Size = new System.Drawing.Size(75, 23);
            this.animationButton.TabIndex = 9;
            this.animationButton.Text = "Animate";
            this.animationButton.UseVisualStyleBackColor = true;
            this.animationButton.Click += new System.EventHandler(this.animationButton_Click);
            // 
            // speedTestButton
            // 
            this.speedTestButton.Location = new System.Drawing.Point(842, 659);
            this.speedTestButton.Name = "speedTestButton";
            this.speedTestButton.Size = new System.Drawing.Size(75, 23);
            this.speedTestButton.TabIndex = 10;
            this.speedTestButton.Text = "Speed Test";
            this.speedTestButton.UseVisualStyleBackColor = true;
            this.speedTestButton.Click += new System.EventHandler(this.speedTestButton_Click);
            // 
            // framesText
            // 
            this.framesText.Location = new System.Drawing.Point(723, 222);
            this.framesText.Name = "framesText";
            this.framesText.Size = new System.Drawing.Size(194, 20);
            this.framesText.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(720, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Frames per zoom:";
            // 
            // iterationsText
            // 
            this.iterationsText.Location = new System.Drawing.Point(723, 275);
            this.iterationsText.Name = "iterationsText";
            this.iterationsText.Size = new System.Drawing.Size(194, 20);
            this.iterationsText.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(720, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Max iterations:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(720, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Height";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(720, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Width";
            // 
            // heightText
            // 
            this.heightText.Location = new System.Drawing.Point(723, 327);
            this.heightText.Name = "heightText";
            this.heightText.Size = new System.Drawing.Size(194, 20);
            this.heightText.TabIndex = 17;
            // 
            // widthText
            // 
            this.widthText.Location = new System.Drawing.Point(723, 377);
            this.widthText.Name = "widthText";
            this.widthText.Size = new System.Drawing.Size(194, 20);
            this.widthText.TabIndex = 18;
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(842, 414);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 23);
            this.changeButton.TabIndex = 19;
            this.changeButton.Text = "Change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // generateAllButton
            // 
            this.generateAllButton.Location = new System.Drawing.Point(842, 601);
            this.generateAllButton.Name = "generateAllButton";
            this.generateAllButton.Size = new System.Drawing.Size(75, 23);
            this.generateAllButton.TabIndex = 20;
            this.generateAllButton.Text = "Generate";
            this.generateAllButton.UseVisualStyleBackColor = true;
            this.generateAllButton.Click += new System.EventHandler(this.generateAllButton_Click);
            // 
            // Mandelbrot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 728);
            this.Controls.Add(this.generateAllButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.widthText);
            this.Controls.Add(this.heightText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.iterationsText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.framesText);
            this.Controls.Add(this.speedTestButton);
            this.Controls.Add(this.animationButton);
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
        private System.Windows.Forms.Button animationButton;
        private System.Windows.Forms.Button speedTestButton;
        private System.Windows.Forms.TextBox framesText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox iterationsText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox heightText;
        private System.Windows.Forms.TextBox widthText;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Button generateAllButton;
    }
}

