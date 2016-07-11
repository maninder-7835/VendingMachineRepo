using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
	public class VendMachine
	{
		private int _oneDollarcount;
		private int _twoDollarcount;
		private int _fiveDollarcount;
		private int _tenDollarcount;
		private List<ItemTrays> _tarys;
		private Item _selectedItem;

		public VendMachine()
		{

			// Added $90 when machine starts first time
			_oneDollarcount = 5;
			_twoDollarcount = 5;
			_fiveDollarcount = 5;
			_tenDollarcount = 5;
			_tarys = new List<ItemTrays>();
			_selectedItem = new Item();
			addTrays();
		}

		private void addTrays()
		{
			Item itemCoke = new Item() { itemCode = "001", itemPrice = 2, itemDescription = "Soft Drink", itemName = "Coke", itemQty = 2 };
			Item itemPepsi = new Item() { itemCode = "002", itemPrice = 2, itemDescription = "Soft Drink", itemName = "Pepsi", itemQty = 3 };
			Item itemSprite = new Item() { itemCode = "003", itemPrice = 2, itemDescription = "Soft Drink", itemName = "Sprite", itemQty = 1 };
			Item itemBurger = new Item() { itemCode = "004", itemPrice = 16, itemDescription = "Sides", itemName = "Burger", itemQty = 1 };
			Item itemSandwich = new Item() { itemCode = "005", itemPrice = 16, itemDescription = "Sides", itemName = "Sandwich", itemQty = 4 };
			Item itemFries = new Item() { itemCode = "006", itemPrice = 16, itemDescription = "Sides", itemName = "Fries", itemQty = 3 };
			Item itemPasta = new Item() { itemCode = "007", itemPrice = 28, itemDescription = "Main course", itemName = "Pasta", itemQty = 1 };
			Item itemPizza = new Item() { itemCode = "008", itemPrice = 28, itemDescription = "Main course", itemName = "Pizza", itemQty = 5 };
			Item itemNoodles = new Item() { itemCode = "009", itemPrice = 28, itemDescription = "Main course", itemName = "Noodles", itemQty = 1 };

			List<Item> itemTrayitems = new List<Item>();
			itemTrayitems.Add(itemCoke);
			itemTrayitems.Add(itemPepsi);
			itemTrayitems.Add(itemSprite);

			Trays.Add(new ItemTrays()
			{
				TrayNumber = 1,
				Items = itemTrayitems,
				ItemCount = 3
			});

			itemTrayitems = new List<Item>();
			itemTrayitems.Add(itemBurger);
			itemTrayitems.Add(itemSandwich);
			itemTrayitems.Add(itemFries);

			Trays.Add(new ItemTrays()
			{
				TrayNumber = 2,
				Items = itemTrayitems,
				ItemCount = 3
			});

			itemTrayitems = new List<Item>();
			itemTrayitems.Add(itemPasta);
			itemTrayitems.Add(itemPizza);
			itemTrayitems.Add(itemNoodles);

			Trays.Add(new ItemTrays()
			{
				TrayNumber = 3,
				Items = itemTrayitems,
				ItemCount = 3
			});
		}

		public List<ItemTrays> Trays
		{
			get { return _tarys; }
			set { _tarys = value; }
		}

		public Item SelectedItem
		{
			get { return _selectedItem; }
			set { _selectedItem = value; }
		}

		public int OneDollarCount
		{
			get { return _oneDollarcount; }
			set { _oneDollarcount = value; }
		}

		public int TwoDollarcount
		{
			get { return _twoDollarcount; }
			set { _twoDollarcount = value; }
		}

		public int FiveDollarcount
		{
			get { return _fiveDollarcount; }
			set { _fiveDollarcount = value; }
		}

		public int TenDollarcount
		{
			get { return _tenDollarcount; }
			set { _tenDollarcount = value; }
		}

		internal string TotalMoneyLeft()
		{
			return ((OneDollarCount * 1) + (TwoDollarcount * 2) + (FiveDollarcount * 5) + (TenDollarcount * 10)).ToString();
		}

		internal void AcceptMoney(int money)
		{
			while (money >= 10)
			{
				TenDollarcount++;
				money = money - 10;
			}

			while (money >= 5)
			{
				FiveDollarcount++;
				money = money - 5;
			}

			while (money >= 2)
			{
				TwoDollarcount++;
				money = money - 2;
			}

			while (money >= 1)
			{
				OneDollarCount++;
				money = money - 1;
			}
		}

		internal void ReturnMoney(int money)
		{
			while (money >= 10)
			{
				TenDollarcount--;
				money = money - 10;
			}

			while (money >= 5)
			{
				FiveDollarcount--;
				money = money - 5;
			}

			while (money >= 2)
			{
				TwoDollarcount--;
				money = money - 2;
			}

			while (money >= 1)
			{
				OneDollarCount--;
				money = money - 1;
			}
		}

		internal bool isValidItemCode(string code)
		{
			bool isValid = false;

			foreach (ItemTrays tray in Trays)
			{
				var result = tray.Items.Where(item => item.itemCode == code).FirstOrDefault();
				if (result != null)
				{
					isValid = true;
					SelectedItem = result;
					break;
				}
				else
				{
					SelectedItem = null;
				}
			}
			return isValid;
		}

		internal bool isItemExistsInInventory(string code)
		{
			bool isInventoryExists = false;
			if (SelectedItem != null && SelectedItem.itemQty > 0)
			{
				isInventoryExists = true;
			}

			return isInventoryExists;
		}

		internal void updateInventory(string code)
		{
			if (SelectedItem != null && SelectedItem.itemQty > 0)
			{
				SelectedItem.itemQty--;
				AcceptMoney(SelectedItem.itemPrice);
			}
		}

		internal int ReturnMoney(string code, int money)
		{
			int returnMoney = 0;
			returnMoney = money - SelectedItem.itemPrice;
			return returnMoney;
		}

		internal int TrayItemCount(int trayNumber)
		{
			var result = Trays.Where(item => item.TrayNumber == trayNumber).FirstOrDefault();
			int counter = 0;
			if (result != null)
			{
				foreach (Item item in result.Items)
				{
					counter += item.itemQty;
				}
			}


			return counter;
		}

		internal void InsertInventory(string code, int quantity, int price, int trayNumber, string itemName)
		{
			var result = Trays.Where(item => item.TrayNumber == trayNumber).FirstOrDefault();
			if (result != null)
			{
				var searcheditem = result.Items.Where(item => item.itemCode == code).FirstOrDefault();
				if (searcheditem != null)
				{
					searcheditem.itemQty += quantity;
				}
				else
				{
					Item insertItem = new Item();
					insertItem.itemCode = code;
					insertItem.itemPrice = price;
					insertItem.itemQty = quantity;
					insertItem.itemName = itemName;
					result.Items.Add(insertItem);
				}
			}
		}
	}
}