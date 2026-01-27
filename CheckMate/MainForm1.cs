using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CheckMate
{
    public partial class MainForm1 : Form
    {
        public MainForm1()
        {
            InitializeComponent();

            this.Text = "CheckMate Product Input";
            this.Size = new Size(700, 600);

            int leftMargin = 20;
            int labelOffset = 10;
            int top = 20;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Title";
            lblTitle.Location = new Point(leftMargin, top);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            TextBox txtTitle = new TextBox();
            txtTitle.Name = "txtTitle";
            txtTitle.Location = new Point(leftMargin, top + lblTitle.Height + labelOffset);
            txtTitle.Size = new Size(400, 25);
            this.Controls.Add(txtTitle);

            top += txtTitle.Height + lblTitle.Height + 2 * labelOffset;

            // Description
            Label lblDescription = new Label();
            lblDescription.Text = "Description";
            lblDescription.Location = new Point(leftMargin, top);
            lblDescription.AutoSize = true;
            this.Controls.Add(lblDescription);

            TextBox txtDescription = new TextBox();
            txtDescription.Name = "txtDescription";
            txtDescription.Location = new Point(leftMargin, top + lblDescription.Height + labelOffset);
            txtDescription.Size = new Size(500, 80);
            txtDescription.Multiline = true;
            this.Controls.Add(txtDescription);

            top += txtDescription.Height + lblDescription.Height + 2 * labelOffset;

            // Tags
            Label lblTags = new Label();
            lblTags.Text = "Tags (comma-separated)";
            lblTags.Location = new Point(leftMargin, top);
            lblTags.AutoSize = true;
            this.Controls.Add(lblTags);

            TextBox txtTags = new TextBox();
            txtTags.Name = "txtTags";
            txtTags.Location = new Point(leftMargin, top + lblTags.Height + labelOffset);
            txtTags.Size = new Size(400, 25);
            this.Controls.Add(txtTags);

            top += txtTags.Height + lblTags.Height + 2 * labelOffset;

            // Category
            Label lblCategory = new Label();
            lblCategory.Text = "Category";
            lblCategory.Location = new Point(leftMargin, top);
            lblCategory.AutoSize = true;
            this.Controls.Add(lblCategory);

            ComboBox cmbCategory = new ComboBox();
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Location = new Point(leftMargin, top + lblCategory.Height + labelOffset);
            cmbCategory.Size = new Size(200, 25);
            cmbCategory.Items.AddRange(new string[] { "T-Shirts", "Sweatshirts", "Jackets", "Pants", "Shoes" });
            this.Controls.Add(cmbCategory);

            top += cmbCategory.Height + lblCategory.Height + 2 * labelOffset;

            // Images
            Label lblImages = new Label();
            lblImages.Text = "Images";
            lblImages.Location = new Point(leftMargin, top);
            lblImages.AutoSize = true;
            this.Controls.Add(lblImages);

            ListBox lstImages = new ListBox();
            lstImages.Name = "lstImages";
            lstImages.Location = new Point(leftMargin, top + lblImages.Height + labelOffset);
            lstImages.Size = new Size(200, 100);
            this.Controls.Add(lstImages);

            Button btnAddImage = new Button();
            btnAddImage.Name = "btnAddImage";
            btnAddImage.Text = "Add Image";
            btnAddImage.Location = new Point(leftMargin + lstImages.Width + 10, top + lblImages.Height + labelOffset);
            btnAddImage.Size = new Size(100, 30);
            btnAddImage.Click += (s, e) =>
            {
                MessageBox.Show("Add image functionality not implemented yet");
            };
            this.Controls.Add(btnAddImage);

            top += lstImages.Height + lblImages.Height + 2 * labelOffset;

            // Variants
            Label lblVariants = new Label();
            lblVariants.Text = "Variants";
            lblVariants.Location = new Point(leftMargin, top);
            lblVariants.AutoSize = true;
            this.Controls.Add(lblVariants);

            DataGridView dgvVariants = new DataGridView();
            dgvVariants.Name = "dgvVariants";
            dgvVariants.Location = new Point(leftMargin, top + lblVariants.Height + labelOffset);
            dgvVariants.Size = new Size(500, 150);
            dgvVariants.ColumnCount = 3;
            dgvVariants.Columns[0].Name = "Color";
            dgvVariants.Columns[1].Name = "Size";
            dgvVariants.Columns[2].Name = "Price";
            this.Controls.Add(dgvVariants);

            // Buttons
            Button btnSave = new Button();
            btnSave.Name = "btnSave";
            btnSave.Text = "Save Product";
            btnSave.Size = new Size(120, 35);
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            Button btnLoad = new Button();
            btnLoad.Name = "btnLoad";
            btnLoad.Text = "Load Product";
            btnLoad.Size = new Size(120, 35);
            btnLoad.Click += btnLoad_Click;
            this.Controls.Add(btnLoad);

            Button btnCheck = new Button();
            btnCheck.Name = "btnCheck";
            btnCheck.Text = "Check Product";
            btnCheck.Size = new Size(140, 35);
            btnCheck.Click += btnCheck_Click;
            this.Controls.Add(btnCheck);

            //Button positioning
            int buttonsTop = top + dgvVariants.Height + 20;
            int buttonWidth = 120;
            int spacing = 20;

            btnSave.Location = new Point(leftMargin, buttonsTop);
            btnLoad.Location = new Point(leftMargin + buttonWidth + spacing, buttonsTop);
            btnCheck.Location = new Point(leftMargin + 2 * (buttonWidth + spacing), buttonsTop);
        }

        // Event methods
        private void btnSave_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Save feature will be implemented later.");
        }

        private void btnLoad_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Load feature will be implemented later.");
        }

        private void btnCheck_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Product check will be implemented later.");
        }
    } 
} 
