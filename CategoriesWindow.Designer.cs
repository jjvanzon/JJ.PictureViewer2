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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoriesWindow));
            this.categoriesGrid = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubFolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyboardKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryListsComboBox = new System.Windows.Forms.ComboBox();
            this.saveCategoryListButton = new System.Windows.Forms.Button();
            this.removeCategoryListButton = new System.Windows.Forms.Button();
            this.renameButton = new System.Windows.Forms.Button();
            this.removeCategoryButton = new System.Windows.Forms.Button();
            this.addCategoryButton = new System.Windows.Forms.Button();
            this.clearCategoriesButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
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
            this.Index.Visible = false;
            // 
            // SubFolderName
            // 
            this.SubFolderName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubFolderName.FillWeight = 200F;
            this.SubFolderName.HeaderText = "Sub Folder Name";
            this.SubFolderName.Name = "SubFolderName";
            // 
            // KeyboardKey
            // 
            this.KeyboardKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.KeyboardKey.HeaderText = "Keyboard Key";
            this.KeyboardKey.MaxInputLength = 1;
            this.KeyboardKey.Name = "KeyboardKey";
            // 
            // categoryListsComboBox
            // 
            this.categoryListsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryListsComboBox.FormattingEnabled = true;
            this.categoryListsComboBox.Location = new System.Drawing.Point(0, 1);
            this.categoryListsComboBox.Name = "categoryListsComboBox";
            this.categoryListsComboBox.Size = new System.Drawing.Size(138, 21);
            this.categoryListsComboBox.TabIndex = 2;
            this.categoryListsComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryListsComboBox_SelectedIndexChanged);
            // 
            // saveCategoryListButton
            // 
            this.saveCategoryListButton.Location = new System.Drawing.Point(152, 0);
            this.saveCategoryListButton.Name = "saveCategoryListButton";
            this.saveCategoryListButton.Size = new System.Drawing.Size(55, 21);
            this.saveCategoryListButton.TabIndex = 3;
            this.saveCategoryListButton.Text = "Save";
            this.saveCategoryListButton.UseVisualStyleBackColor = true;
            this.saveCategoryListButton.Click += new System.EventHandler(this.saveCategoryListButton_Click);
            // 
            // removeCategoryListButton
            // 
            this.removeCategoryListButton.Location = new System.Drawing.Point(275, 0);
            this.removeCategoryListButton.Name = "removeCategoryListButton";
            this.removeCategoryListButton.Size = new System.Drawing.Size(55, 21);
            this.removeCategoryListButton.TabIndex = 4;
            this.removeCategoryListButton.Text = "Remove";
            this.removeCategoryListButton.UseVisualStyleBackColor = true;
            this.removeCategoryListButton.Click += new System.EventHandler(this.removeCategoryListButton_Click);
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(214, 0);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(55, 21);
            this.renameButton.TabIndex = 5;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // removeCategoryButton
            // 
            this.removeCategoryButton.Location = new System.Drawing.Point(275, 228);
            this.removeCategoryButton.Name = "removeCategoryButton";
            this.removeCategoryButton.Size = new System.Drawing.Size(55, 21);
            this.removeCategoryButton.TabIndex = 6;
            this.removeCategoryButton.Text = "Remove";
            this.removeCategoryButton.UseVisualStyleBackColor = true;
            this.removeCategoryButton.Click += new System.EventHandler(this.removeCategoryButton_Click);
            // 
            // addCategoryButton
            // 
            this.addCategoryButton.Location = new System.Drawing.Point(213, 228);
            this.addCategoryButton.Name = "addCategoryButton";
            this.addCategoryButton.Size = new System.Drawing.Size(55, 21);
            this.addCategoryButton.TabIndex = 7;
            this.addCategoryButton.Text = "Add";
            this.addCategoryButton.UseVisualStyleBackColor = true;
            this.addCategoryButton.Click += new System.EventHandler(this.addCategoryButton_Click);
            // 
            // clearCategoriesButton
            // 
            this.clearCategoriesButton.Location = new System.Drawing.Point(336, 228);
            this.clearCategoriesButton.Name = "clearCategoriesButton";
            this.clearCategoriesButton.Size = new System.Drawing.Size(55, 21);
            this.clearCategoriesButton.TabIndex = 8;
            this.clearCategoriesButton.Text = "Clear";
            this.clearCategoriesButton.UseVisualStyleBackColor = true;
            this.clearCategoriesButton.Click += new System.EventHandler(this.clearCategoriesButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(0, 228);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 21);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // newButton
            // 
            this.newButton.Location = new System.Drawing.Point(336, 0);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(55, 21);
            this.newButton.TabIndex = 10;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // CategoriesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 323);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.clearCategoriesButton);
            this.Controls.Add(this.addCategoryButton);
            this.Controls.Add(this.removeCategoryButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.removeCategoryListButton);
            this.Controls.Add(this.saveCategoryListButton);
            this.Controls.Add(this.categoryListsComboBox);
            this.Controls.Add(this.categoriesGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CategoriesWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Categories - Picture Viewer 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CategoriesWindow_FormClosing);
            this.Resize += new System.EventHandler(this.CategoriesWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.categoriesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView categoriesGrid;
        private System.Windows.Forms.ComboBox categoryListsComboBox;
        private System.Windows.Forms.Button saveCategoryListButton;
        private System.Windows.Forms.Button removeCategoryListButton;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyboardKey;
        private System.Windows.Forms.Button removeCategoryButton;
        private System.Windows.Forms.Button addCategoryButton;
        private System.Windows.Forms.Button clearCategoriesButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button newButton;
    }
}