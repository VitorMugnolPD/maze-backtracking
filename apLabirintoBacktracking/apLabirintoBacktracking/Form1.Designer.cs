﻿namespace apLabirintoBacktracking
{
    partial class Form1
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
            this.btnEncontrarCaminhos = new System.Windows.Forms.Button();
            this.btnAbrirArquivo = new System.Windows.Forms.Button();
            this.dgvCaminhos = new System.Windows.Forms.DataGridView();
            this.dgvLabirinto = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dlgAbrir = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaminhos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabirinto)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEncontrarCaminhos
            // 
            this.btnEncontrarCaminhos.Location = new System.Drawing.Point(794, 12);
            this.btnEncontrarCaminhos.Name = "btnEncontrarCaminhos";
            this.btnEncontrarCaminhos.Size = new System.Drawing.Size(84, 58);
            this.btnEncontrarCaminhos.TabIndex = 0;
            this.btnEncontrarCaminhos.Text = "Encontrar caminhos";
            this.btnEncontrarCaminhos.UseVisualStyleBackColor = true;
            this.btnEncontrarCaminhos.Click += new System.EventHandler(this.btnEncontrarCaminhos_Click);
            // 
            // btnAbrirArquivo
            // 
            this.btnAbrirArquivo.Location = new System.Drawing.Point(704, 12);
            this.btnAbrirArquivo.Name = "btnAbrirArquivo";
            this.btnAbrirArquivo.Size = new System.Drawing.Size(84, 58);
            this.btnAbrirArquivo.TabIndex = 1;
            this.btnAbrirArquivo.Text = "Abrir arquivo";
            this.btnAbrirArquivo.UseVisualStyleBackColor = true;
            this.btnAbrirArquivo.Click += new System.EventHandler(this.btnAbrirArquivo_Click);
            // 
            // dgvCaminhos
            // 
            this.dgvCaminhos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCaminhos.Location = new System.Drawing.Point(406, 85);
            this.dgvCaminhos.Name = "dgvCaminhos";
            this.dgvCaminhos.Size = new System.Drawing.Size(472, 353);
            this.dgvCaminhos.TabIndex = 2;
            // 
            // dgvLabirinto
            // 
            this.dgvLabirinto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvLabirinto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabirinto.Location = new System.Drawing.Point(12, 85);
            this.dgvLabirinto.Name = "dgvLabirinto";
            this.dgvLabirinto.RowHeadersWidth = 12;
            this.dgvLabirinto.Size = new System.Drawing.Size(388, 353);
            this.dgvLabirinto.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(403, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Caminhos encontrados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Labirinto";
            // 
            // dlgAbrir
            // 
            this.dlgAbrir.DefaultExt = "txt";
            this.dlgAbrir.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvLabirinto);
            this.Controls.Add(this.dgvCaminhos);
            this.Controls.Add(this.btnAbrirArquivo);
            this.Controls.Add(this.btnEncontrarCaminhos);
            this.Name = "Form1";
            this.Text = "Caminhos em Labirinto";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaminhos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabirinto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncontrarCaminhos;
        private System.Windows.Forms.Button btnAbrirArquivo;
        private System.Windows.Forms.DataGridView dgvCaminhos;
        private System.Windows.Forms.DataGridView dgvLabirinto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog dlgAbrir;
    }
}

