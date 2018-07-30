namespace PictureViewer2
{
    partial class CategoriesWindow
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
            this.components = new System.ComponentModel.Container();
            this.categoryGrid = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubFolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyboardKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.presetComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.categoryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // categoryGrid
            // 
            this.categoryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.categoryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.SubFolderName,
            this.KeyboardKey});
            this.categoryGrid.Location = new System.Drawing.Point(0, 28);
            this.categoryGrid.MultiSelect = false;
            this.categoryGrid.Name = "categoryGrid";
            this.categoryGrid.Size = new System.Drawing.Size(391, 194);
            this.categoryGrid.TabIndex = 1;
            this.categoryGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.categoryGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserAddedRow);
            this.categoryGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserDeletedRow);
            this.categoryGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_UserDeletingRow);
            // 
            // Index
            // 
            this.Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Index.HeaderText = "#";
            this.Index.MinimumWidth = 2;
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 39;
            // 
            // SubFolderName
            // 
            this.SubFolderName.HeaderText = "Name";
            this.SubFolderName.Name = "SubFolderName";
            // 
            // KeyboardKey
            // 
            this.KeyboardKey.HeaderText = "Key";
            this.KeyboardKey.MaxInputLength = 1;
            this.KeyboardKey.Name = "KeyboardKey";
            // 
            // presetComboBox
            // 
            this.presetComboBox.FormattingEnabled = true;
            this.presetComboBox.Location = new System.Drawing.Point(0, 1);
            this.presetComboBox.Name = "presetComboBox";
            this.presetComboBox.Size = new System.Drawing.Size(344, 21);
            this.presetComboBox.TabIndex = 2;
            this.presetComboBox.SelectedIndexChanged += new System.EventHandler(this.presetComboBox_SelectedIndexChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(349, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(42, 21);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CategoriesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 223);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.presetComboBox);
            this.Controls.Add(this.categoryGrid);
            this.Name = "CategoriesWindow";
            this.Text = "Categories - Picture Viewer 2";
            ((System.ComponentModel.ISupportInitialize)(this.categoryGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView categoryGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyboardKey;
        private System.Windows.Forms.ComboBox presetComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ImageList imageList;
    }
}