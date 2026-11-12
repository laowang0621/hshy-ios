using System;
using UnityEngine;

public interface IBoss : IEnemy
{
	bool _开启Boss死亡UI { get; set; }

	event Action _Boss死亡;

	event Action<Vector2> _Boss死亡_带位置;

	event Action _Boss死亡_消失后;

	event Action<Vector2> _Boss死亡_带位置_消失后;

	void F_监听Boss死亡_消失后(Action action)
	{
		if (action != null)
		{
			_Boss死亡_消失后 += action;
		}
	}

	void F_监听Boss死亡_消失后(Action<Vector2> action)
	{
		if (action != null)
		{
			_Boss死亡_带位置_消失后 += action;
		}
	}

	void F_监听Boss死亡(Action action)
	{
		if (action != null)
		{
			_Boss死亡 += action;
		}
	}

	void F_监听Boss死亡(Action<Vector2> action)
	{
		if (action != null)
		{
			_Boss死亡_带位置 += action;
		}
	}

	void F_特殊调用1()
	{
	}
}
