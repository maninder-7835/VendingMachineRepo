using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
	public class ItemTrays
	{
		private List <Item> _items;
		private int _trayNumber;
		private int _itemCount;

		public List<Item> Items
		{
			get { return _items; }
			set { _items = value; }
		}

		public int TrayNumber
		{
			get { return _trayNumber; }
			set { _trayNumber = value; }
		}

		public int ItemCount
		{
			get { return _itemCount; }
			set { _itemCount = value; }
		}
	}
}
