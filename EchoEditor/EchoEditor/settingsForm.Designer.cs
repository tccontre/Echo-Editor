/*
# Copyright 2019 tccontre

# This file is part of EchoEditor.

# EchoEditor is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.

# EchoEditor is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.

# You should have received a copy of the GNU General Public License
# along with EchoEditor.  If not, see <http://www.gnu.org/licenses/>.

*/

namespace EchoEditor
{
    partial class settingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.outTextBox = new System.Windows.Forms.TextBox();
            this.templTextBox = new System.Windows.Forms.TextBox();
            this.sigmaNameTextBox = new System.Windows.Forms.TextBox();
            this.teamTextBox = new System.Windows.Forms.TextBox();
            this.mdomainTextBox = new System.Windows.Forms.TextBox();
            this.sigmacTextBox = new System.Windows.Forms.TextBox();
            this.settingSaveButton = new System.Windows.Forms.Button();
            this.settingEditButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.noteslabel8 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.IDtextBox = new System.Windows.Forms.TextBox();
            this.pythonFilePath = new System.Windows.Forms.Label();
            this.pythonPathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(25, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output Folder Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(25, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "template Folder Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(25, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "SIGMA Name Format";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Location = new System.Drawing.Point(25, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Team";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.Location = new System.Drawing.Point(25, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mitre Domain";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Info;
            this.label6.Location = new System.Drawing.Point(25, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Sigmac Folder Path";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // outTextBox
            // 
            this.outTextBox.Location = new System.Drawing.Point(193, 26);
            this.outTextBox.Name = "outTextBox";
            this.outTextBox.ReadOnly = true;
            this.outTextBox.Size = new System.Drawing.Size(595, 22);
            this.outTextBox.TabIndex = 6;
            // 
            // templTextBox
            // 
            this.templTextBox.Location = new System.Drawing.Point(193, 57);
            this.templTextBox.Name = "templTextBox";
            this.templTextBox.ReadOnly = true;
            this.templTextBox.Size = new System.Drawing.Size(595, 22);
            this.templTextBox.TabIndex = 7;
            // 
            // sigmaNameTextBox
            // 
            this.sigmaNameTextBox.Location = new System.Drawing.Point(193, 87);
            this.sigmaNameTextBox.Name = "sigmaNameTextBox";
            this.sigmaNameTextBox.ReadOnly = true;
            this.sigmaNameTextBox.Size = new System.Drawing.Size(595, 22);
            this.sigmaNameTextBox.TabIndex = 8;
            // 
            // teamTextBox
            // 
            this.teamTextBox.Location = new System.Drawing.Point(193, 118);
            this.teamTextBox.Name = "teamTextBox";
            this.teamTextBox.ReadOnly = true;
            this.teamTextBox.Size = new System.Drawing.Size(595, 22);
            this.teamTextBox.TabIndex = 9;
            // 
            // mdomainTextBox
            // 
            this.mdomainTextBox.Location = new System.Drawing.Point(193, 147);
            this.mdomainTextBox.Name = "mdomainTextBox";
            this.mdomainTextBox.ReadOnly = true;
            this.mdomainTextBox.Size = new System.Drawing.Size(595, 22);
            this.mdomainTextBox.TabIndex = 10;
            // 
            // sigmacTextBox
            // 
            this.sigmacTextBox.Location = new System.Drawing.Point(193, 178);
            this.sigmacTextBox.Name = "sigmacTextBox";
            this.sigmacTextBox.ReadOnly = true;
            this.sigmacTextBox.Size = new System.Drawing.Size(595, 22);
            this.sigmacTextBox.TabIndex = 11;
            // 
            // settingSaveButton
            // 
            this.settingSaveButton.Location = new System.Drawing.Point(28, 339);
            this.settingSaveButton.Name = "settingSaveButton";
            this.settingSaveButton.Size = new System.Drawing.Size(378, 23);
            this.settingSaveButton.TabIndex = 12;
            this.settingSaveButton.Text = "Save";
            this.settingSaveButton.UseVisualStyleBackColor = true;
            this.settingSaveButton.Click += new System.EventHandler(this.savebutton1_Click);
            // 
            // settingEditButton
            // 
            this.settingEditButton.Location = new System.Drawing.Point(415, 339);
            this.settingEditButton.Name = "settingEditButton";
            this.settingEditButton.Size = new System.Drawing.Size(376, 23);
            this.settingEditButton.TabIndex = 13;
            this.settingEditButton.Text = "Edit";
            this.settingEditButton.UseVisualStyleBackColor = true;
            this.settingEditButton.Click += new System.EventHandler(this.editbutton2_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Info;
            this.label7.Location = new System.Drawing.Point(25, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Author";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(193, 208);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.ReadOnly = true;
            this.authorTextBox.Size = new System.Drawing.Size(595, 22);
            this.authorTextBox.TabIndex = 15;
            // 
            // noteslabel8
            // 
            this.noteslabel8.AutoSize = true;
            this.noteslabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteslabel8.ForeColor = System.Drawing.Color.Blue;
            this.noteslabel8.Location = new System.Drawing.Point(25, 382);
            this.noteslabel8.Name = "noteslabel8";
            this.noteslabel8.Size = new System.Drawing.Size(405, 17);
            this.noteslabel8.TabIndex = 16;
            this.noteslabel8.Text = "notes: all folder is by default should be in the working directory.";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Info;
            this.label8.Location = new System.Drawing.Point(25, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "ID Name";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IDtextBox
            // 
            this.IDtextBox.Location = new System.Drawing.Point(193, 242);
            this.IDtextBox.Name = "IDtextBox";
            this.IDtextBox.ReadOnly = true;
            this.IDtextBox.Size = new System.Drawing.Size(595, 22);
            this.IDtextBox.TabIndex = 18;
            // 
            // pythonFilePath
            // 
            this.pythonFilePath.BackColor = System.Drawing.SystemColors.Info;
            this.pythonFilePath.Location = new System.Drawing.Point(25, 280);
            this.pythonFilePath.Name = "pythonFilePath";
            this.pythonFilePath.Size = new System.Drawing.Size(150, 20);
            this.pythonFilePath.TabIndex = 19;
            this.pythonFilePath.Text = "Python File Path";
            this.pythonFilePath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pythonPathTextBox
            // 
            this.pythonPathTextBox.Location = new System.Drawing.Point(193, 278);
            this.pythonPathTextBox.Name = "pythonPathTextBox";
            this.pythonPathTextBox.ReadOnly = true;
            this.pythonPathTextBox.Size = new System.Drawing.Size(595, 22);
            this.pythonPathTextBox.TabIndex = 20;
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 448);
            this.Controls.Add(this.pythonPathTextBox);
            this.Controls.Add(this.pythonFilePath);
            this.Controls.Add(this.IDtextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.noteslabel8);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.settingEditButton);
            this.Controls.Add(this.settingSaveButton);
            this.Controls.Add(this.sigmacTextBox);
            this.Controls.Add(this.mdomainTextBox);
            this.Controls.Add(this.teamTextBox);
            this.Controls.Add(this.sigmaNameTextBox);
            this.Controls.Add(this.templTextBox);
            this.Controls.Add(this.outTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "settingsForm";
            this.Text = "settings";
            this.Load += new System.EventHandler(this.settingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button settingSaveButton;
        private System.Windows.Forms.Button settingEditButton;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox outTextBox;
        public System.Windows.Forms.TextBox templTextBox;
        public System.Windows.Forms.TextBox sigmaNameTextBox;
        public System.Windows.Forms.TextBox teamTextBox;
        public System.Windows.Forms.TextBox mdomainTextBox;
        public System.Windows.Forms.TextBox sigmacTextBox;
        public System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.Label noteslabel8;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox IDtextBox;
        private System.Windows.Forms.Label pythonFilePath;
        public System.Windows.Forms.TextBox pythonPathTextBox;
    }
}
