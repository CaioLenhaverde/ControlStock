namespace View
{
    partial class FrmConsultaEstoque
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
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxFiltro = new System.Windows.Forms.ComboBox();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidademedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtdatual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label14.Location = new System.Drawing.Point(657, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 18);
            this.label14.TabIndex = 70;
            this.label14.Text = "Filtrar por Ação:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label13.Location = new System.Drawing.Point(10, 115);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(233, 18);
            this.label13.TabIndex = 71;
            this.label13.Text = "Pesquisar produto pela descrição:";
            // 
            // cbxFiltro
            // 
            this.cbxFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cbxFiltro.FormattingEnabled = true;
            this.cbxFiltro.Items.AddRange(new object[] {
            "Todas",
            "Entrada",
            "Saída"});
            this.cbxFiltro.Location = new System.Drawing.Point(660, 135);
            this.cbxFiltro.Name = "cbxFiltro";
            this.cbxFiltro.Size = new System.Drawing.Size(291, 28);
            this.cbxFiltro.TabIndex = 69;
            this.cbxFiltro.SelectedIndexChanged += new System.EventHandler(this.cbxFiltro_SelectedIndexChanged);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPesquisa.Location = new System.Drawing.Point(13, 136);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(605, 26);
            this.txtPesquisa.TabIndex = 68;
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);
            // 
            // dgvDados
            // 
            this.dgvDados.AllowUserToAddRows = false;
            this.dgvDados.AllowUserToDeleteRows = false;
            this.dgvDados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descricao,
            this.unidademedida,
            this.qtdatual,
            this.data,
            this.hora,
            this.motivo,
            this.acao,
            this.quantidade});
            this.dgvDados.Location = new System.Drawing.Point(13, 169);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.ReadOnly = true;
            this.dgvDados.RowHeadersVisible = false;
            this.dgvDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDados.Size = new System.Drawing.Size(938, 386);
            this.dgvDados.TabIndex = 67;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::View.Properties.Resources.stock;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 109);
            this.pictureBox1.TabIndex = 72;
            this.pictureBox1.TabStop = false;
            // 
            // descricao
            // 
            this.descricao.DataPropertyName = "produto";
            this.descricao.FillWeight = 114.6968F;
            this.descricao.HeaderText = "Produto";
            this.descricao.Name = "descricao";
            this.descricao.ReadOnly = true;
            // 
            // unidademedida
            // 
            this.unidademedida.DataPropertyName = "unidademedida";
            this.unidademedida.FillWeight = 100.6808F;
            this.unidademedida.HeaderText = "Unidade";
            this.unidademedida.Name = "unidademedida";
            this.unidademedida.ReadOnly = true;
            // 
            // qtdatual
            // 
            this.qtdatual.DataPropertyName = "qtdatual";
            this.qtdatual.FillWeight = 81.21827F;
            this.qtdatual.HeaderText = "Qtd Atual";
            this.qtdatual.Name = "qtdatual";
            this.qtdatual.ReadOnly = true;
            // 
            // data
            // 
            this.data.DataPropertyName = "data";
            this.data.FillWeight = 100.6808F;
            this.data.HeaderText = "Data";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            // 
            // hora
            // 
            this.hora.DataPropertyName = "hora";
            this.hora.FillWeight = 100.6808F;
            this.hora.HeaderText = "Hora";
            this.hora.Name = "hora";
            this.hora.ReadOnly = true;
            // 
            // motivo
            // 
            this.motivo.DataPropertyName = "motivo";
            this.motivo.FillWeight = 100.6808F;
            this.motivo.HeaderText = "Motivo";
            this.motivo.Name = "motivo";
            this.motivo.ReadOnly = true;
            // 
            // acao
            // 
            this.acao.DataPropertyName = "acao";
            this.acao.FillWeight = 100.6808F;
            this.acao.HeaderText = "Ação";
            this.acao.Name = "acao";
            this.acao.ReadOnly = true;
            // 
            // quantidade
            // 
            this.quantidade.DataPropertyName = "quantidade";
            this.quantidade.FillWeight = 100.6808F;
            this.quantidade.HeaderText = "Quantidade";
            this.quantidade.Name = "quantidade";
            this.quantidade.ReadOnly = true;
            // 
            // FrmConsultaEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::View.Properties.Resources.fundod;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(963, 558);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbxFiltro);
            this.Controls.Add(this.txtPesquisa);
            this.Controls.Add(this.dgvDados);
            this.Name = "FrmConsultaEstoque";
            this.Text = "FrmConsultaEstoque";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbxFiltro;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidademedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtdatual;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewTextBoxColumn hora;
        private System.Windows.Forms.DataGridViewTextBoxColumn motivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn acao;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantidade;
    }
}