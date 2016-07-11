using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
	public class Item
	{

		public string itemCode
		{
			get;
			set;
		}

		public string itemName
		{
			get;
			set;
		}

		public string itemDescription
		{
			get;
			set;
		}

		public int itemPrice
		{
			get;
			set;
		}

		public int itemQty
		{
			get;
			set;
		}
	}
}
