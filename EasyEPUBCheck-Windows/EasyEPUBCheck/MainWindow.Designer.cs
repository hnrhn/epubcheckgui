using System;

namespace EasyEPUBCheck
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chosenFileDisplay = new System.Windows.Forms.TextBox();
            this.chooseEpubButton = new System.Windows.Forms.Button();
            this.validateButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chosenFileDisplay
            // 
            this.chosenFileDisplay.Enabled = false;
            this.chosenFileDisplay.Location = new System.Drawing.Point(12, 12);
            this.chosenFileDisplay.Name = "chosenFileDisplay";
            this.chosenFileDisplay.Size = new System.Drawing.Size(682, 23);
            this.chosenFileDisplay.TabIndex = 0;
            // 
            // chooseEpubButton
            // 
            this.chooseEpubButton.Location = new System.Drawing.Point(700, 12);
            this.chooseEpubButton.Name = "chooseEpubButton";
            this.chooseEpubButton.Size = new System.Drawing.Size(88, 23);
            this.chooseEpubButton.TabIndex = 1;
            this.chooseEpubButton.Text = "Choose EPUB";
            this.chooseEpubButton.UseVisualStyleBackColor = true;
            this.chooseEpubButton.Click += new System.EventHandler(this.chooseEpubButton_Click);
            // 
            // validateButton
            // 
            this.validateButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.validateButton.Enabled = false;
            this.validateButton.Location = new System.Drawing.Point(320, 41);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(160, 23);
            this.validateButton.TabIndex = 2;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 336);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(114, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Result to File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(713, 336);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputTextBox.Location = new System.Drawing.Point(12, 70);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(776, 260);
            this.outputTextBox.TabIndex = 6;
            this.outputTextBox.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 381);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.chooseEpubButton);
            this.Controls.Add(this.chosenFileDisplay);
            this.Name = "MainWindow";
            this.Text = "EasyEPUBCheck";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chosenFileDisplay;
        private System.Windows.Forms.Button chooseEpubButton;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox outputTextBox;
    }
}