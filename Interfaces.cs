using System;
using System.Collections.Generic;

namespace Burgers;

internal interface IForKurs
{
	public int Price { get; init; }

	List<Func<string>> GenerateDelegateList();
}

internal interface IEat
{
	bool IsEaten { get; }

	string Eat();
}

internal interface IUnpackage
{
	bool IsUnpackaged { get; }

	string Unpackage();	
}