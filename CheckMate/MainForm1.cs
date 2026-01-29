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
        private ComboBox? cmbSavedProducts;

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
            lstImages.Size = new Size(220, 120);
            this.Controls.Add(lstImages);

            pnlImages = new FlowLayoutPanel();
            pnlImages.Location = new Point(leftMargin + lstImages.Width + 20, top + lblImages.Height + labelOffset);
            pnlImages.Size = new Size(360, 160);
            pnlImages.AutoScroll = true;
            pnlImages.BorderStyle = BorderStyle.FixedSingle;
            pnlImages.BackColor = Color.WhiteSmoke;
            this.Controls.Add(pnlImages);

            Button btnAddImage = new Button();
            btnAddImage.Name = "btnAddImage";
            btnAddImage.Text = "Add Image";
            btnAddImage.Size = new Size(120, 30);
            btnAddImage.Location = new Point(
                pnlImages.Left,
                pnlImages.Bottom + 8
            );
            btnAddImage.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Select Product Images";
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    ofd.Multiselect = true; // Multiple images can be selected

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in ofd.FileNames)
                        {
                            lstImages.Items.Add(file);

                            PictureBox pb = new PictureBox();
                            pb.Image = Image.FromFile(file);
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Size = new Size(80, 80);
                            pb.Margin = new Padding(5);
                            pb.Tag = file;
                            pnlImages.Controls.Add(pb);
                        }
                    }
                }
            };
            this.Controls.Add(btnAddImage);
            
            Button btnRemoveImage = new Button();
            btnRemoveImage.Text = "Remove Image";
            btnRemoveImage.Size = new Size(120, 30);
            btnRemoveImage.Location = new Point(
                btnAddImage.Left + btnAddImage.Width + 10,
                btnAddImage.Top
            );
            btnRemoveImage.Click += (s, e) =>
            {
                if (lstImages.SelectedItem == null)
                {
                    MessageBox.Show("Please select an image to remove.");
                    return;
                }

                string selectedPath = lstImages.SelectedItem.ToString()!;

                // ListBox'tan sil
                lstImages.Items.Remove(selectedPath);

                // Panel'deki PictureBox'ı bul ve sil
                PictureBox? toRemove = null;
                foreach (Control ctrl in pnlImages.Controls)
                {
                    if (ctrl is PictureBox pb && pb.Tag?.ToString() == selectedPath)
                    {
                        toRemove = pb;
                        break;
                    }
                }

                if (toRemove != null)
                {
                    pnlImages.Controls.Remove(toRemove);
                    toRemove.Image.Dispose();
                    toRemove.Dispose();
                }
            };
            this.Controls.Add(btnRemoveImage);

            top = Math.Max(lstImages.Bottom, btnAddImage.Bottom) + 30;

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

            Button btnRenew = new Button();
            btnRenew.Location = new Point(txtTitle.Right + 10, txtTitle.Top); // Title'ın sağında
            btnRenew.Size = new Size(100, txtTitle.Height); // Title ile aynı yükseklik
            btnRenew.Text = "Renew";
            btnRenew.Click += (s, e) => ResetForm();
            this.Controls.Add(btnRenew);

            //Button positioning
            int buttonsTop = top + dgvVariants.Height + 20;
            int buttonWidth = 120;
            int spacing = 20;

            btnSave.Location = new Point(leftMargin, buttonsTop);
            btnLoad.Location = new Point(leftMargin + buttonWidth + spacing, buttonsTop);
            btnCheck.Location = new Point(leftMargin + 2 * (buttonWidth + spacing), buttonsTop);

            // Saved Products ComboBox
            Label lblSavedProducts = new Label();
            lblSavedProducts.Text = "Saved Products";
            lblSavedProducts.Font = new Font("Segoe UI", 9, FontStyle.Regular); // smaller, cleaner font
            lblSavedProducts.Location = new Point(btnCheck.Right + 30, btnCheck.Top + 5); // slight downward shift
            lblSavedProducts.AutoSize = true;
            this.Controls.Add(lblSavedProducts);

            cmbSavedProducts = new ComboBox();
            cmbSavedProducts.Location = new Point(lblSavedProducts.Left, lblSavedProducts.Bottom + 3); // small gap
            cmbSavedProducts.Size = new Size(180, 25);
            this.Controls.Add(cmbSavedProducts);

            // Load saved products into ComboBox
            LoadSavedProducts();
            
            //Feedback TextBox
            txtFeedback = new TextBox();
            txtFeedback.Name = "txtFeedback";
            txtFeedback.Multiline = true;
            txtFeedback.ReadOnly = false;
            txtFeedback.TabStop = false;
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
            // Create a list to store feedback messages
            List<string> feedback = new List<string>();

            // Check Title
            if (string.IsNullOrWhiteSpace(txtTitle?.Text) || txtTitle.Text.Length < 5)
                feedback.Add("Title is too short or empty.");

            // Check Description
            if (string.IsNullOrWhiteSpace(txtDescription?.Text) || txtDescription.Text.Length < 10)
                feedback.Add("Description is too short or empty.");

            // Check Category
            if (cmbCategory?.SelectedIndex == -1)
                feedback.Add("No category selected.");

            // Check Tags
            if (string.IsNullOrWhiteSpace(txtTags?.Text))
                feedback.Add("Tags are empty.");

            // Check Images
            if (lstImages == null || lstImages.Items.Count == 0)
                feedback.Add("No images added.");

            // Check Variants
            if (dgvVariants == null || dgvVariants.Rows.Count == 0 || dgvVariants.Rows[0].IsNewRow)
                feedback.Add("No variants added.");

            // Show feedback in the TextBox with color coding
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
            if (txtTitle == null || txtDescription == null || txtTags == null ||
                cmbCategory == null || lstImages == null || dgvVariants == null)
                return;

            string productsDir = "Products";
            if (!Directory.Exists(productsDir))
                Directory.CreateDirectory(productsDir);

            Product product = new Product
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                Tags = new List<string>(txtTags.Text.Split(',')),
                Category = cmbCategory.SelectedItem?.ToString() ?? "",
                Images = new List<string>(),
                Variants = new List<Product.Variant>()
            };

            foreach (var item in lstImages.Items)
                product.Images.Add(item.ToString()!);

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

            string safeTitle = string.Join("_", product.Title.Split(Path.GetInvalidFileNameChars()));
            string filename = Path.Combine(productsDir, $"{safeTitle}_{DateTime.Now:yyyyMMddHHmmss}.json");

            string json = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);

            MessageBox.Show($"Product saved to {filename}", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void LoadProduct()
        {
            // Get the selected product from the ComboBox
            if (cmbSavedProducts == null || cmbSavedProducts.SelectedItem == null)
            {
                MessageBox.Show("No product selected.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = cmbSavedProducts.SelectedItem.ToString()!;
            string filePath = Path.Combine("Products", fileName + ".json");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Selected product file not found.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Read the product JSON
            string json = File.ReadAllText(filePath);
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

            // Clear previous images and add new ones
            lstImages!.Items.Clear();
            pnlImages!.Controls.Clear(); // remove old images
            foreach (var image in product.Images)
            {
                lstImages.Items.Add(image);

                PictureBox pb = new PictureBox();
                pb.Image = Image.FromFile(image);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Size = new Size(80, 80);
                pb.Margin = new Padding(5);
                pnlImages.Controls.Add(pb);
            }

            // Fill variants in DataGridView
            dgvVariants!.Rows.Clear();
            foreach (var variant in product.Variants)
                dgvVariants.Rows.Add(variant.Color, variant.Size, variant.Price);
        }
        
        // Method to load all saved product files into the ComboBox
        private void LoadSavedProducts()
        {
            // Ensure the ComboBox exists
            if (cmbSavedProducts == null) return;

            cmbSavedProducts.Items.Clear();

            string productsFolder = Path.Combine(Application.StartupPath, "Products");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(productsFolder))
                Directory.CreateDirectory(productsFolder);

            // Get all JSON files in the folder
            string[] files = Directory.GetFiles(productsFolder, "*.json");

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                cmbSavedProducts.Items.Add(fileName);
            }
        }
        
        private void ResetForm()
        {
            // Clear textboxes
            txtTitle!.Text = "";
            txtDescription!.Text = "";
            txtTags!.Text = "";

            // Reset category selection
            cmbCategory!.SelectedIndex = -1;

            // Clear images list and panel
            lstImages!.Items.Clear();
            pnlImages.Controls.Clear();

            // Clear variants grid
            dgvVariants!.Rows.Clear();

            // Reset feedback
            txtFeedback!.Text = "";
            txtFeedback.ForeColor = Color.Red;

            // Optionally, reset saved product selection
            cmbSavedProducts!.SelectedIndex = -1;
        }

        private FlowLayoutPanel pnlImages;
        private void btnAddImage_Click(object? sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    lstImages.Items.Add(file);

                    // PictureBox
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromFile(file);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Size = new Size(80, 80);
                    pb.Margin = new Padding(5);
                    pnlImages.Controls.Add(pb);
                }
            }
        }
    }
}