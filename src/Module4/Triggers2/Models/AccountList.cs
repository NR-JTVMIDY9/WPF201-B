using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Triggers2.Models {
	public class Account {
		public int ID { get; set; }
		public string Name { get; set; }
		public AccountType Type { get; set; }
		public AccountLocation Location { get; set; }
		public decimal Balance { get; set; }
		public override string ToString() {
			return string.Format(
			"{0},{1},{2},{3},{4}",
			ID, Name, Type, Location, Balance);
		}
	}
	public enum AccountType {
		Savings,
		FixedDeposit,
		Checking
	}
	public enum AccountLocation {
		Local,
		SameState,
		SameCountry,
		Foreign
	}
	public class AccountList : ObservableCollection<Account> {
		public AccountList() {
			Add(new Account {
				ID = 100,
				Name = "Sally",
				Type = AccountType.Savings,
				Location = AccountLocation.Local,
				Balance = 10000.00m
			});
			Add(new Account {
				ID = 200,
				Name = "John",
				Type = AccountType.FixedDeposit,
				Location = AccountLocation.Local,
				Balance = 50000.00m
			});
			Add(new Account {
				ID = 300,
				Name = "Mary",
				Type = AccountType.Checking,
				Location = AccountLocation.SameState,
				Balance = 25000.00m
			});
			Add(new Account {
				ID = 300,
				Name = "Janet",
				Type = AccountType.Savings,
				Location = AccountLocation.SameState,
				Balance = 5000.00m
			});
		}
	}
}
