using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Burgers;

public class HappyMeal : IUnpackage, IForKurs
{
	public required int Price { get; init; }

	[JsonInclude]
	public bool IsUnpackaged { get; private set; }

	public string Unpackage()
	{
		IsUnpackaged = true;
		return "This HappyMeal has been unpackaged";
	}

	public string Play()
	{
		return IsUnpackaged ? "You have played with toy and become happy!!!" : "First you need to unpack your HappyMeal";
	}
	public override string ToString()
	{
		return $"This HappyMeal costs {Price} and is {(IsUnpackaged ? "unpackaged" : "not unpackaged")}";
	}

	public List<Func<string>> GenerateDelegateList()
	{
		return new List<Func<string>> { Unpackage, Play };
	}

}
