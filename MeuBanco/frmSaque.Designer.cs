
namespace MeuBanco
{
    partial class frmSaque
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
            this.txtSaque = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSacar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSaque
            // 
            this.txtSaque.Location = new System.Drawing.Point(72, 47);
            this.txtSaque.Name = "txtSaque";
            this.txtSaque.Size = new System.Drawing.Size(100, 20);
            this.txtSaque.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Valor";
            // 
            // btnSacar
            // 
            this.btnSacar.Location = new System.Drawing.Point(190, 45);
            this.btnSacar.Name = "btnSacar";
            this.btnSacar.Size = new System.Drawing.Size(107, 24);
            this.btnSacar.TabIndex = 2;
            this.btnSacar.Text = "Sacar";
            this.btnSacar.UseVisualStyleBackColor = true;
            this.btnSacar.Click += new System.EventHandler(this.btnSacar_Click);
            // 
            // frmSaque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 115);
            this.Controls.Add(this.btnSacar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSaque);
            this.Name = "frmSaque";
            this.Text = "frmSaque";
            this.Load += new System.EventHandler(this.frmSaque_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSaque;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSacar;
    }
}