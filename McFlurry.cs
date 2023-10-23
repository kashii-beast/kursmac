namespace Burgers;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class McFlurry : IEat, IForKurs
{
	public enum ETopping
	{
		CHOCOLATE,
		STRAWBERRY,
		CARAMEL,
		RASPBERRY,
		MANGO
	}

	public required int Price { get; init; }

	[JsonInclude]
	public ETopping Topping { get; private set; }

	[JsonInclude]
	public bool IsEaten { get; private set; }

	public McFlurry(ETopping topping)
	{
		Topping = topping;
	}

	public string RefillIcecream(ETopping topping)
	{
		IsEaten = false;
		Topping = topping;

		return $"You got new icecream with {topping.ToString().ToLower()} topping";
	}

	public string Eat()
	{
		if (IsEaten) return "This McFlurry is already eaten";


		IsEaten = true;
		return "This McFlurry has been eated";
	}

	public override string ToString()
	{
		return $"This tasty cold McFlurry costs {Price} and has {Topping.ToString().ToLower()} topping. It is {(!IsEaten ? "not " : "")}eaten";
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { Eat };
	}
}
