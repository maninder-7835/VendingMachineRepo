using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
	class Program
	{
		static VendMachine objVendMachine = new VendMachine();
		static void Main(string[] args)
		{
			string choice = "";
			while (choice != "0")
			{
				Console.WriteLine("Menu.");
				Console.WriteLine("1. Deposit Money");
				Console.WriteLine("2. Buy Produts.");
				Console.WriteLine("3. Add Inventory.");
				Console.WriteLine("0. Exit");
				choice = Console.ReadLine();

				switch (choice)
				{
					case "1": DepositMoney();
						break;
					case "2": BuyProducts();
						break;
					case "3": UpdateInventory();
						break;
					case "0":
						break;
					default: Console.WriteLine("Please enter valid option");
						break;
				}
			}

			//Console.ReadLine();
		}

		private static void UpdateInventory()
		{
			Console.WriteLine("Following is the avialable spaces in the Trays");
			Console.WriteLine("Tray 1 - " + (10 - objVendMachine.TrayItemCount(1)).ToString());
			Console.WriteLine("Tray 2 - " + (10 - objVendMachine.TrayItemCount(2)).ToString());
			Console.WriteLine("Tray 3 - " + (10 - objVendMachine.TrayItemCount(3)).ToString());

			Console.WriteLine("Please enter the Tray Number in which you would like to add the products");
			string strtrayNumber = Console.ReadLine();
			int trayNumber = 0;

			 while (!Int32.TryParse(strtrayNumber, out trayNumber))
			{
				Console.WriteLine("Please enter valid Tray Number");
				strtrayNumber = Console.ReadLine();

			}

			if(trayNumber<=0 || trayNumber>3)
			{
				Console.WriteLine("Tray Number not valid. Please try again");
				return;
			}

			Console.WriteLine("Please enter the item Code you want to add");
			string code = Console.ReadLine();

			Console.WriteLine("Please enter the item quantity you want to add");
			string strquantity = Console.ReadLine();
			int quantity = 0;
			while (!Int32.TryParse(strquantity, out quantity))
			{
				Console.WriteLine("Please enter valid quanatity");
				strquantity = Console.ReadLine();

			}

			Console.WriteLine("Please enter the item price you want to add");
			string strprice = Console.ReadLine();
			int price = 0;

			while (!Int32.TryParse(strprice, out price))
			{
				Console.WriteLine("Please enter valid price");
				strprice = Console.ReadLine();

			}

			Console.WriteLine("Please enter the item Name you want to add");
			string itemName = Console.ReadLine();

			int avialablespace = 10 - objVendMachine.TrayItemCount(trayNumber);

			if (quantity > avialablespace)
			{
				Console.WriteLine("Not enough space in the tray " + trayNumber + ". Please try again.");
				return;
			}
			else
			{
				objVendMachine.InsertInventory(code, quantity, price, trayNumber, itemName);
				Console.WriteLine("Inventory Updated !!!");
			}
		}

		private static void BuyProducts()
		{
			Console.WriteLine("Avialable items in Tray are :-");

			foreach (ItemTrays itemTray in objVendMachine.Trays)
			{
				Console.WriteLine();
				Console.WriteLine(string.Format("Item Tary {0}", itemTray.TrayNumber));
				Console.WriteLine("ItemCode     ItemPrice    ItemName");

				foreach (Item item in itemTray.Items)
				{
					Console.WriteLine(string.Format("{0}           {1}             {2}", item.itemCode, item.itemPrice, item.itemName));
				}
			}

			Console.WriteLine("Please Enter the itemCode you want to buy");
			string code = Console.ReadLine();

			while (!objVendMachine.isValidItemCode(code))
			{
				Console.WriteLine("Please enter valid Code or press 0 to exit");

				code = Console.ReadLine();
				if (code == "0")
				{
					code = "";
					break;
				}
			}

			if (!string.IsNullOrEmpty(code))
			{
				Console.Write("Please enter the money to buy");
				string strmoney = Console.ReadLine();
				int money = 0;



				while (!Int32.TryParse(strmoney, out money))
				{
					Console.WriteLine("Please enter valid money or press 0 to exit");

					strmoney = Console.ReadLine();

					if (strmoney == "0")
						break;


				}

				if (money != 0 && money < objVendMachine.SelectedItem.itemPrice)
				{
					Console.WriteLine("Entered money is less than the price of he item. Sorry the item cannot be purchased.");
					money = 0;
				}

				if (money != 0)
				{
					bool isItemExists = objVendMachine.isItemExistsInInventory(code);
					if (isItemExists)
					{
						objVendMachine.updateInventory(code);
						int returnMoney = objVendMachine.ReturnMoney(code, money);
						Console.WriteLine("Money Returned - " + returnMoney);

					}
				}
			}
		}

		private static void DepositMoney()
		{
			Console.WriteLine("Please enter the money t be deposited in the machine");
			string strmoney = Console.ReadLine();
			int money = 0;
			while (!Int32.TryParse(strmoney, out money))
			{
				Console.WriteLine("Please enter valid money or press 0 to exit");

				strmoney = Console.ReadLine();

				if (strmoney == "0")
					break;

			}

			if (money != 0)
			{
				objVendMachine.AcceptMoney(money);
				Console.WriteLine(string.Format("Total Money after deposit in the Vending Machine is ${0}", objVendMachine.TotalMoneyLeft()));
				Console.ReadLine();
			}
		}


	}
}
