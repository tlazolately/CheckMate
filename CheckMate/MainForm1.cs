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

            // Form Settings
            this.Text = "CheckMate - Product Checker";
            this.Size = new System.Drawing.Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title Input
            Label lblTitle = new Label();
            lblTitle.Text = "Product Title:";
            lblTitle.Location = new System.Drawing.Point(20, 20); // X=20, Y=20
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            TextBox txtTitle = new TextBox();
            txtTitle.Name = "txtTitle";
            txtTitle.Location = new System.Drawing.Point(20, 45);
            txtTitle.Size = new System.Drawing.Size(400, 25);
            this.Controls.Add(txtTitle);

            // Description Input
            Label lblDescription = new Label();
            lblDescription.Text = "Product Description:";
            lblDescription.Location = new System.Drawing.Point(20, 85);
            lblDescription.AutoSize = true;
            this.Controls.Add(lblDescription);

            TextBox txtDescription = new TextBox();
            txtDescription.Name = "txtDescription";
            txtDescription.Location = new System.Drawing.Point(20, 110);
            txtDescription.Size = new System.Drawing.Size(400, 150);
            txtDescription.Multiline = true;
            this.Controls.Add(txtDescription);

            // Tags Input
            Label lblTags = new Label();
            lblTags.Text = "Product Tags:";
            lblTags.Location = new System.Drawing.Point(20, 280);
            lblTags.AutoSize = true;
            this.Controls.Add(lblTags);

            TextBox txtTags = new TextBox();
            txtTags.Name = "txtTags";
            txtTags.Location = new System.Drawing.Point(20, 305);
            txtTags.Size = new System.Drawing.Size(400, 25);
            this.Controls.Add(txtTags);

            // Category Input
            Label lblCategory = new Label();
            lblCategory.Text = "Product Category:";
            lblCategory.Location = new System.Drawing.Point(20, 345);
            lblCategory.AutoSize = true;
            this.Controls.Add(lblCategory);

            ComboBox cmbCategory = new ComboBox();
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Location = new System.Drawing.Point(20, 370);
            cmbCategory.Size = new System.Drawing.Size(200, 25);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(cmbCategory);

            // Add predefined categories to ComboBox
            cmbCategory.Items.Add("Tshirts");      // Example: Tshirts
            cmbCategory.Items.Add("Sweatshirts");  // Example: Sweatshirts
            cmbCategory.Items.Add("Jackets");      // Example: Jackets
            cmbCategory.Items.Add("Pants");        // Example: Pants
            cmbCategory.Items.Add("Shoes");        // Example: Shoes

            // Optionally select the first item by default
            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }
    }
}
