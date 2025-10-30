namespace Dentist_CorePractice
{
    partial class DataMigrationForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.patientReviewTab = new System.Windows.Forms.TabPage();
            this.patientGrid = new System.Windows.Forms.DataGridView();
            this.patientSummaryLabel = new System.Windows.Forms.Label();
            this.treatmentReviewTab = new System.Windows.Forms.TabPage();
            this.treatmentGridView = new System.Windows.Forms.DataGridView();
            this.lblTreatment = new System.Windows.Forms.Label();
            this.RtbValidationBox = new System.Windows.Forms.RichTextBox();
            this.ingestionLogRtBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IngestDataButton = new System.Windows.Forms.Button();
            this.RunValidationButton = new System.Windows.Forms.Button();
            this.LoadDataButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CopySqlButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.patientReviewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientGrid)).BeginInit();
            this.treatmentReviewTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treatmentGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.patientReviewTab);
            this.tabControl1.Controls.Add(this.treatmentReviewTab);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(988, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1457, 1300);
            this.tabControl1.TabIndex = 0;
            // 
            // patientReviewTab
            // 
            this.patientReviewTab.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.patientReviewTab.Controls.Add(this.patientGrid);
            this.patientReviewTab.Controls.Add(this.patientSummaryLabel);
            this.patientReviewTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientReviewTab.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.patientReviewTab.Location = new System.Drawing.Point(8, 39);
            this.patientReviewTab.Name = "patientReviewTab";
            this.patientReviewTab.Padding = new System.Windows.Forms.Padding(3);
            this.patientReviewTab.Size = new System.Drawing.Size(1441, 1192);
            this.patientReviewTab.TabIndex = 0;
            this.patientReviewTab.Text = "1. Patient Data Review";
            // 
            // patientGrid
            // 
            this.patientGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.patientGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.patientGrid.Location = new System.Drawing.Point(38, 84);
            this.patientGrid.Name = "patientGrid";
            this.patientGrid.RowHeadersWidth = 82;
            this.patientGrid.RowTemplate.Height = 33;
            this.patientGrid.Size = new System.Drawing.Size(1365, 1086);
            this.patientGrid.TabIndex = 2;
            // 
            // patientSummaryLabel
            // 
            this.patientSummaryLabel.AutoSize = true;
            this.patientSummaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientSummaryLabel.Location = new System.Drawing.Point(33, 36);
            this.patientSummaryLabel.Name = "patientSummaryLabel";
            this.patientSummaryLabel.Size = new System.Drawing.Size(385, 25);
            this.patientSummaryLabel.TabIndex = 1;
            this.patientSummaryLabel.Text = "Patient Summary : Load CSV to begin..";
            this.patientSummaryLabel.Click += new System.EventHandler(this.patientSummaryLabel_Click);
            // 
            // treatmentReviewTab
            // 
            this.treatmentReviewTab.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.treatmentReviewTab.Controls.Add(this.treatmentGridView);
            this.treatmentReviewTab.Controls.Add(this.lblTreatment);
            this.treatmentReviewTab.Location = new System.Drawing.Point(8, 39);
            this.treatmentReviewTab.Name = "treatmentReviewTab";
            this.treatmentReviewTab.Padding = new System.Windows.Forms.Padding(3);
            this.treatmentReviewTab.Size = new System.Drawing.Size(1441, 1253);
            this.treatmentReviewTab.TabIndex = 1;
            this.treatmentReviewTab.Text = "2. Treatment Data Review";
            this.treatmentReviewTab.Click += new System.EventHandler(this.treatmentReviewTab_Click);
            // 
            // treatmentGridView
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.treatmentGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.treatmentGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.treatmentGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.treatmentGridView.Location = new System.Drawing.Point(26, 74);
            this.treatmentGridView.Name = "treatmentGridView";
            this.treatmentGridView.RowHeadersWidth = 82;
            this.treatmentGridView.RowTemplate.Height = 33;
            this.treatmentGridView.Size = new System.Drawing.Size(1355, 1157);
            this.treatmentGridView.TabIndex = 5;
            // 
            // lblTreatment
            // 
            this.lblTreatment.AutoSize = true;
            this.lblTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTreatment.Location = new System.Drawing.Point(30, 27);
            this.lblTreatment.Name = "lblTreatment";
            this.lblTreatment.Size = new System.Drawing.Size(415, 25);
            this.lblTreatment.TabIndex = 4;
            this.lblTreatment.Text = "Treatment Summary : Load CSV to begin..";
            // 
            // RtbValidationBox
            // 
            this.RtbValidationBox.Location = new System.Drawing.Point(28, 303);
            this.RtbValidationBox.Name = "RtbValidationBox";
            this.RtbValidationBox.Size = new System.Drawing.Size(930, 286);
            this.RtbValidationBox.TabIndex = 3;
            this.RtbValidationBox.Text = "";
            // 
            // ingestionLogRtBox
            // 
            this.ingestionLogRtBox.Location = new System.Drawing.Point(28, 664);
            this.ingestionLogRtBox.Name = "ingestionLogRtBox";
            this.ingestionLogRtBox.Size = new System.Drawing.Size(910, 568);
            this.ingestionLogRtBox.TabIndex = 0;
            this.ingestionLogRtBox.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.IngestDataButton);
            this.panel1.Controls.Add(this.RunValidationButton);
            this.panel1.Controls.Add(this.LoadDataButton);
            this.panel1.Location = new System.Drawing.Point(37, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 182);
            this.panel1.TabIndex = 0;
            // 
            // IngestDataButton
            // 
            this.IngestDataButton.Enabled = false;
            this.IngestDataButton.Location = new System.Drawing.Point(623, 56);
            this.IngestDataButton.Name = "IngestDataButton";
            this.IngestDataButton.Size = new System.Drawing.Size(277, 67);
            this.IngestDataButton.TabIndex = 2;
            this.IngestDataButton.Text = "Ingest Data(Simulate DB)";
            this.IngestDataButton.UseVisualStyleBackColor = true;
            this.IngestDataButton.Click += new System.EventHandler(this.IngestDataButton_Click);
            // 
            // RunValidationButton
            // 
            this.RunValidationButton.Enabled = false;
            this.RunValidationButton.Location = new System.Drawing.Point(327, 56);
            this.RunValidationButton.Name = "RunValidationButton";
            this.RunValidationButton.Size = new System.Drawing.Size(260, 67);
            this.RunValidationButton.TabIndex = 1;
            this.RunValidationButton.Text = "Run Validation";
            this.RunValidationButton.UseVisualStyleBackColor = true;
            this.RunValidationButton.Click += new System.EventHandler(this.RunValidationButton_Click);
            // 
            // LoadDataButton
            // 
            this.LoadDataButton.Location = new System.Drawing.Point(35, 56);
            this.LoadDataButton.Name = "LoadDataButton";
            this.LoadDataButton.Size = new System.Drawing.Size(258, 67);
            this.LoadDataButton.TabIndex = 0;
            this.LoadDataButton.Text = "Load CSVs";
            this.LoadDataButton.UseVisualStyleBackColor = true;
            this.LoadDataButton.Click += new System.EventHandler(this.LoadDataButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Validation Summary:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 636);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data Ingestion Log:";
            // 
            // CopySqlButton
            // 
            this.CopySqlButton.Enabled = false;
            this.CopySqlButton.Location = new System.Drawing.Point(28, 1238);
            this.CopySqlButton.Name = "CopySqlButton";
            this.CopySqlButton.Size = new System.Drawing.Size(277, 67);
            this.CopySqlButton.TabIndex = 5;
            this.CopySqlButton.Text = "Copy SQL to Clipboard";
            this.CopySqlButton.UseVisualStyleBackColor = true;
            this.CopySqlButton.Click += new System.EventHandler(this.CopySqlButton_Click);
            // 
            // DataMigrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(2464, 1376);
            this.Controls.Add(this.CopySqlButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RtbValidationBox);
            this.Controls.Add(this.ingestionLogRtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(2196, 1376);
            this.Name = "DataMigrationForm";
            this.Text = "Core Practice Data Migration Tool";
            this.tabControl1.ResumeLayout(false);
            this.patientReviewTab.ResumeLayout(false);
            this.patientReviewTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientGrid)).EndInit();
            this.treatmentReviewTab.ResumeLayout(false);
            this.treatmentReviewTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treatmentGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage patientReviewTab;
        private System.Windows.Forms.TabPage treatmentReviewTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button IngestDataButton;
        private System.Windows.Forms.Button RunValidationButton;
        private System.Windows.Forms.Button LoadDataButton;
        private System.Windows.Forms.Label patientSummaryLabel;
        private System.Windows.Forms.DataGridView patientGrid;
        private System.Windows.Forms.DataGridView treatmentGridView;
        private System.Windows.Forms.Label lblTreatment;
        private System.Windows.Forms.RichTextBox ingestionLogRtBox;
        private System.Windows.Forms.RichTextBox RtbValidationBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CopySqlButton;
    }
}

