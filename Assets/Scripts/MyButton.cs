using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public event Action _Down = delegate
	{
	};

	public event Action _Up = delegate
	{
	};

	public void OnPointerDown(PointerEventData eventData)
	{
		this._Down();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this._Up();
	}
}
