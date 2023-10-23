using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Burgers;

internal abstract class Burger : IEat, IForKurs
{
	[DefaultValue(1)]
	public virtual int CutletAmount { get; init; } = 1;

	public abstract bool Sesame { get; }

	public required int Price { get; init; }

	[JsonInclude]
	public bool IsEaten { get; protected set; }

	public abstract List<Func<string>> GenerateDelegateList();

	public abstract string TakeNewOne();

	public abstract string Reheat();

	public abstract string Eat();

}

internal sealed class BigMac : Burger, IUnpackage
{
	[DefaultValue(2)]
	public override int CutletAmount { get; init; } = 2;

	public override bool Sesame => true;

	[JsonInclude]
	public bool IsUnpackaged { get; private set; }

	public string Unpackage()
	{
		if (IsUnpackaged) return "This BigMac is already unpackaged";

		IsUnpackaged = true;
		return "This BigMac now is out of package";
	}

	public override string TakeNewOne()
	{
		IsUnpackaged = false;
		IsEaten = false;

		return "You got new BigMac";
	}

	public override string Reheat()
	{
		return IsEaten ? "There is nothing to reheat, because BigMac is eaten. Take new BigMac!!!" : "This BigMac has been reheated";
	}

	public override string Eat()
	{
		if (IsEaten) return "This BigMac is already eaten";
		if (!IsUnpackaged) return "You have to unpack BigMac first";


		IsEaten = true;
		return "This BigMac has been eated";
	}
	public static string Sing()
	{
		return "Two grilled meat patties, special sauce, cheese, cucumbers, lettuce and onions,\n" +
			" all on a sesame bun, just like that and it's a Big Mac…Two grilled meat patties, special sauce, cheese, cucumbers,\n" +
			" lettuce and onions, all on a sesame bun, just like that and it's a Big Mac…";
	}

	public override string ToString()
	{
		return $"This BigMac costs {Price}, has {CutletAmount} cutlets and sesame. It is {(!IsEaten ? "not " : "")}eaten";
	}

	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { TakeNewOne, Reheat, Unpackage, Eat, Sing };
	}
}

internal class CheeseBurger : Burger
{
	public override bool Sesame => false;

	public bool PutBacon { get; init; }

	public override string TakeNewOne()
	{
		IsEaten = false;

		return "You got new CheeseBurger";
	}

	public override string Reheat()
	{
		return IsEaten ? "There is nothing to reheat, because CheeseBurger is eaten. Take new CheeseBurger!!!" : "This Cheeseburger has been reheated";
	}

	public override string Eat()
	{
		if (IsEaten) return "This CheeseBurger is already eaten";

		IsEaten = true;
		return "Cheeseburger has been eated";
	}
	public override string ToString()
	{
		return $"This Cheeseburger costs {Price}, has {CutletAmount} cutlets{(PutBacon ? ", bacon" : "")} and does not have sesame{(!PutBacon ? "and bacon" : "")}. It is {(!IsEaten ? "not " : "")}eaten";
	}
	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { TakeNewOne, Reheat, Eat };
	}
}

internal sealed class DoubleCheese : CheeseBurger
{
	public override string TakeNewOne()
	{
		IsEaten = false;

		return "You got new DoubleCheeseBurger";
	}

	public override string Reheat()
	{
		return IsEaten ? "There is nothing to reheat, because DoubleCheeseBurger is eaten. Take new DoubleCheeseBurger!!!" : "This DoubleCheeseBurger has been reheated";
	}

	public override string Eat()
	{
		if (IsEaten) return "This DoubleCheeseBurger is already eaten";

		IsEaten = true;
		return "DoubleCheeseBurger has been eated";
	}
	public override string ToString()
	{
		return $"This DoubleCheeseBurger costs {Price}, has {CutletAmount} cutlets{(PutBacon ? ", bacon" : "")} and does not have sesame{(!PutBacon ? "and bacon" : "")}. It is {(!IsEaten ? "not " : "")}eaten";
	}

	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { TakeNewOne, Reheat, Eat };
	}
}

internal sealed class BigTasty : Burger, IUnpackage
{
	public override bool Sesame => true;

	[JsonInclude]
	public bool IsUnpackaged { get; private set; }

	public string Unpackage()
	{
		if (IsUnpackaged) return "This BigTasty is already unpackaged";

		IsUnpackaged = true;
		return "This BigTasty now is out of package";
	}

	public override string TakeNewOne()
	{
		IsUnpackaged = false;
		IsEaten = false;

		return "You got new BigTasty";
	}

	public override string Reheat()
	{
		return IsEaten ? "There is nothing to reheat, because BigTasty is eaten. Take new BigTasty!!!" : "This BigTasty has been reheated";
	}

	public override string Eat()
	{
		if (IsEaten) return "This BigTasty is already eaten";
		if (!IsUnpackaged) return "You have to unpack BigTasty first";

		IsEaten = true;
		return "This BigTasty has been eated";
	}

	public override string ToString()
	{
		return $"This BigTasty costs {Price}, has {CutletAmount} cutlets and sesame. It is {(!IsEaten ? "not " : "")}eaten";
	}

	public override List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { TakeNewOne, Reheat, Unpackage, Eat };
	}
}
