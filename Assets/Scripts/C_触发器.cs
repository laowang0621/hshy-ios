using System;
using UnityEngine;

public class C_触发器 : MonoBehaviour
{
	public event Action<Collider2D> _Enter = delegate
	{
	};

	public event Action<Collider2D> _Exit = delegate
	{
	};

	private void OnTriggerEnter2D(Collider2D collision)
	{
		this._Enter(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		this._Exit(collision);
	}
}
