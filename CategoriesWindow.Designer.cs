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
            this.categoriesGrid = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubFolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyboardKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryListsComboBox = new System.Windows.Forms.ComboBox();
            this.saveCategoryListButton = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.categoriesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // categoriesGrid
            // 
            this.categoriesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.categoriesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.SubFolderName,
            this.KeyboardKey});
            this.categoriesGrid.Location = new System.Drawing.Point(0, 28);
            this.categoriesGrid.MultiSelect = false;
            this.categoriesGrid.Name = "categoriesGrid";
            this.categoriesGrid.Size = new System.Drawing.Size(391, 194);
            this.categoriesGrid.TabIndex = 1;
            this.categoriesGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.categoriesGrid_CellEndEdit);
            this.categoriesGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.categoriesGrid_UserAddedRow);
            this.categoriesGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.categoriesGrid_UserDeletedRow);
            this.categoriesGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.categoriesGrid_UserDeletingRow);
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
            // categoryListsComboBox
            // 
            this.categoryListsComboBox.FormattingEnabled = true;
            this.categoryListsComboBox.Location = new System.Drawing.Point(0, 1);
            this.categoryListsComboBox.Name = "categoryListsComboBox";
            this.categoryListsComboBox.Size = new System.Drawing.Size(344, 21);
            this.categoryListsComboBox.TabIndex = 2;
            this.categoryListsComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryListsComboBox_SelectedIndexChanged);
            // 
            // saveCategoryListButton
            // 
            this.saveCategoryListButton.Location = new System.Drawing.Point(349, 1);
            this.saveCategoryListButton.Name = "saveCategoryListButton";
            this.saveCategoryListButton.Size = new System.Drawing.Size(42, 21);
            this.saveCategoryListButton.TabIndex = 3;
            this.saveCategoryListButton.Text = "Save";
            this.saveCategoryListButton.UseVisualStyleBackColor = true;
            this.saveCategoryListButton.Click += new System.EventHandler(this.saveCategoryListButton_Click);
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
            this.Controls.Add(this.saveCategoryListButton);
            this.Controls.Add(this.categoryListsComboBox);
            this.Controls.Add(this.categoriesGrid);
            this.Name = "CategoriesWindow";
            this.Text = "Categories - Picture Viewer 2";
            ((System.ComponentModel.ISupportInitialize)(this.categoriesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView categoriesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyboardKey;
        private System.Windows.Forms.ComboBox categoryListsComboBox;
        private System.Windows.Forms.Button saveCategoryListButton;
        private System.Windows.Forms.ImageList imageList;
    }
}