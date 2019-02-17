using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        private decimal storeProfit = 0;

        BindingSource itemsBinding = new BindingSource();
        BindingSource shoppingCartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListBox.DataSource = itemsBinding;
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            shoppingCartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = shoppingCartBinding;
            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            vendorsBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorsBinding;
            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";
        }

        private void SetupData()
        {
            store.Vendors.Add(new Vendor
            {
                FirstName = "Bill",
                LastName = "Smith"
            });
            store.Vendors.Add(new Vendor
            {
                FirstName = "Sue",
                LastName = "Jones",
                Commission = 0.4
            });

            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 3.75M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Harry Potter and the Philosopher's Stone",
                Description = "A book about a wizard",
                Price = 5.20M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Huckleberry Finn",
                Description = "A book about a boy",
                Price = 2.95M,
                Owner = store.Vendors[0]
            });

            store.Name = "Marty's Book Consignment Shop";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)itemsListBox.SelectedItem;
            shoppingCartData.Add(selectedItem);
            shoppingCartBinding.ResetBindings(false);
        }

        private void removeFromCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)shoppingCartListBox.SelectedItem;
            shoppingCartData.Remove(selectedItem);
            shoppingCartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += item.Price * (decimal)item.Owner.Commission;
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;
            }
            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            storeProfitValue.Text = string.Format("${0}", storeProfit);

            shoppingCartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }

        // Auto created methods
        private void ConsignmentShop_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void shoppingCartListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
