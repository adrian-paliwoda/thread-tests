﻿namespace MultithreadUINoRespond
{
    partial class Form1
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
            this.noRespondAction = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // noRespondAction
            // 
            this.noRespondAction.Location = new System.Drawing.Point(13, 13);
            this.noRespondAction.Name = "noRespondAction";
            this.noRespondAction.Size = new System.Drawing.Size(103, 32);
            this.noRespondAction.TabIndex = 0;
            this.noRespondAction.Text = "No respond action";
            this.noRespondAction.UseVisualStyleBackColor = true;
            this.noRespondAction.Click += new System.EventHandler(this.noRespondAction_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.noRespondAction);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button noRespondAction;
    }
}

