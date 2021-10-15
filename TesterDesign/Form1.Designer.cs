
namespace TesterDesign
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
            this.NewTest = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NewTest
            // 
            this.NewTest.BackColor = System.Drawing.Color.DarkSlateGray;
            this.NewTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewTest.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.NewTest.Location = new System.Drawing.Point(53, 45);
            this.NewTest.Name = "NewTest";
            this.NewTest.Size = new System.Drawing.Size(183, 50);
            this.NewTest.TabIndex = 0;
            this.NewTest.Text = "New Test";
            this.NewTest.UseVisualStyleBackColor = false;
            this.NewTest.Click += new System.EventHandler(this.NewTest_Click);
            // 
            // Edit
            // 
            this.Edit.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Edit.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.Edit.Location = new System.Drawing.Point(53, 124);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(183, 50);
            this.Edit.TabIndex = 1;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = false;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(296, 231);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.NewTest);
            this.Name = "Form1";
            this.Text = "Create Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NewTest;
        private System.Windows.Forms.Button Edit;
    }
}

