using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace CheckMate
{
    public partial class MainForm1 : Form
    {
        //Form-level controls
        private TextBox? txtTitle;
        private TextBox? txtDescription;
        private TextBox? txtTags;
        private ComboBox? cmbCategory;
        private ListBox? lstImages;
        private DataGridView? dgvVariants;
        private Button? btnSave;
        private Button? btnLoad;
        private Button? btnCheck;
        private TextBox? txtFeedback;
        public MainForm1()
        {
            InitializeComponent();

            this.Text = "CheckMate Product Input";
            this.Size = new Size(700, 600);
            this.AutoScroll = true;


            int leftMargin = 20;
            int labelOffset = 10;
            int top = 20;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Title";
            lblTitle.Location = new Point(leftMargin, top);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            txtTitle = new TextBox();
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

            txtDescription = new TextBox();
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

            txtTags = new TextBox();
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

            cmbCategory = new ComboBox();
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

            lstImages = new ListBox();
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

            dgvVariants = new DataGridView();
            dgvVariants.Name = "dgvVariants";
            dgvVariants.Location = new Point(leftMargin, top + lblVariants.Height + labelOffset);
            dgvVariants.Size = new Size(500, 150);
            dgvVariants.ColumnCount = 3;
            dgvVariants.Columns[0].Name = "Color";
            dgvVariants.Columns[1].Name = "Size";
            dgvVariants.Columns[2].Name = "Price";
            this.Controls.Add(dgvVariants);

            // Buttons
            btnSave = new Button();
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

            //Feedback TextBox
            txtFeedback = new TextBox();
            txtFeedback.Name = "txtFeedback";
            txtFeedback.Multiline = true;
            txtFeedback.ReadOnly = true;
            txtFeedback.ScrollBars = ScrollBars.Vertical;
            txtFeedback.Size = new Size(650, 100);
            txtFeedback.Location = new Point(leftMargin, buttonsTop + 50);
            txtFeedback.ForeColor = Color.Red;
            this.Controls.Add(txtFeedback);

        }

        // Event methods
        private void btnSave_Click(object? sender, EventArgs e)
        {
            SaveProduct();
        }

        private void btnLoad_Click(object? sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnCheck_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Product check will be implemented later.");
            List<string> feedback = new List<string>();

            // Title check
            if (string.IsNullOrWhiteSpace(txtTitle?.Text) || txtTitle.Text.Length < 5)
                feedback.Add("Title is too short or empty.");

            // Description check
            if (string.IsNullOrWhiteSpace(txtDescription?.Text) || txtDescription.Text.Length < 10)
                feedback.Add("Description is too short or empty.");

            // Category check
            if (cmbCategory?.SelectedIndex == -1)
                feedback.Add("No category selected.");

            // Tags check
            if (string.IsNullOrWhiteSpace(txtTags?.Text))
                feedback.Add("Tags are empty.");

            // Images check
            if (lstImages == null || lstImages.Items.Count == 0)
                feedback.Add("No images added.");

            // Variants check
            if (dgvVariants == null || dgvVariants.Rows.Count == 0 || dgvVariants.Rows[0].IsNewRow)
                feedback.Add("No variants added.");
            

            // Show feedback in the TextBox
            if (txtFeedback != null)
            {
                if (feedback.Count == 0)
                {
                    txtFeedback.ForeColor = Color.Green;
                    txtFeedback.Text = "Product is ready to publish!";
                }
                else
                {
                    txtFeedback.ForeColor = Color.Red;
                    txtFeedback.Text = "Issues found:\r\n" + string.Join("\r\n", feedback);
                }
            }
        }

        private void SaveProduct()
        {
            // Ensure all GUI controls exist
            if (txtTitle == null || txtDescription == null || txtTags == null ||
                cmbCategory == null || lstImages == null || dgvVariants == null)
                return;

            // Create a new Product instance
            Product product = new Product
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                Tags = new List<string>(txtTags.Text.Split(',')),
                Category = cmbCategory.SelectedItem?.ToString() ?? "",
                Images = new List<string>(),
                Variants = new List<Product.Variant>()
            };

            // Add images from ListBox
            foreach (var item in lstImages.Items)
                product.Images.Add(item.ToString()!);

            // Add variants from DataGridView
            foreach (DataGridViewRow row in dgvVariants.Rows)
            {
                if (row.IsNewRow) continue;

                product.Variants.Add(new Product.Variant
                {
                    Color = row.Cells[0].Value?.ToString() ?? "",
                    Size = row.Cells[1].Value?.ToString() ?? "",
                    Price = row.Cells[2].Value != null ? Convert.ToDecimal(row.Cells[2].Value) : 0
                });
            }

            // Serialize to JSON
            string json = System.Text.Json.JsonSerializer.Serialize(product, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            // Save to file
            System.IO.File.WriteAllText("product.json", json);

            MessageBox.Show("Product saved to product.json", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void LoadProduct()
        {
            // Check if file exists
            if (!File.Exists("product.json"))
            {
                MessageBox.Show("No saved product found.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Read JSON content
            string json = File.ReadAllText("product.json");

            // Deserialize into Product object
            Product product = JsonSerializer.Deserialize<Product>(json)!;

            if (product == null)
            {
                MessageBox.Show("Failed to load product.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Fill GUI fields
            txtTitle!.Text = product.Title;
            txtDescription!.Text = product.Description;
            txtTags!.Text = string.Join(",", product.Tags);
            cmbCategory!.SelectedItem = product.Category;

            lstImages!.Items.Clear();
            foreach (var image in product.Images)
                lstImages.Items.Add(image);

            dgvVariants!.Rows.Clear();
            foreach (var variant in product.Variants)
                dgvVariants.Rows.Add(variant.Color, variant.Size, variant.Price);
        }
    }
}