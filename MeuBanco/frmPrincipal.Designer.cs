
namespace MeuBanco
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTransferencia = new System.Windows.Forms.Button();
            this.btnDeposito = new System.Windows.Forms.Button();
            this.btnSaque = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTransferencia
            // 
            this.btnTransferencia.Location = new System.Drawing.Point(543, 186);
            this.btnTransferencia.Name = "btnTransferencia";
            this.btnTransferencia.Size = new System.Drawing.Size(221, 66);
            this.btnTransferencia.TabIndex = 0;
            this.btnTransferencia.Text = "Transferência";
            this.btnTransferencia.UseVisualStyleBackColor = true;
            this.btnTransferencia.Click += new System.EventHandler(this.btnTransferencia_Click);
            // 
            // btnDeposito
            // 
            this.btnDeposito.Location = new System.Drawing.Point(287, 186);
            this.btnDeposito.Name = "btnDeposito";
            this.btnDeposito.Size = new System.Drawing.Size(221, 66);
            this.btnDeposito.TabIndex = 1;
            this.btnDeposito.Text = "Depósito";
            this.btnDeposito.UseVisualStyleBackColor = true;
            this.btnDeposito.Click += new System.EventHandler(this.btnDeposito_Click);
            // 
            // btnSaque
            // 
            this.btnSaque.Location = new System.Drawing.Point(31, 186);
            this.btnSaque.Name = "btnSaque";
            this.btnSaque.Size = new System.Drawing.Size(221, 66);
            this.btnSaque.TabIndex = 2;
            this.btnSaque.Text = "Saque";
            this.btnSaque.UseVisualStyleBackColor = true;
            this.btnSaque.Click += new System.EventHandler(this.btnSaque_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSaque);
            this.Controls.Add(this.btnDeposito);
            this.Controls.Add(this.btnTransferencia);
            this.Name = "frmPrincipal";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTransferencia;
        private System.Windows.Forms.Button btnDeposito;
        private System.Windows.Forms.Button btnSaque;
    }
}

