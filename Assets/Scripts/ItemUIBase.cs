using UnityEngine;

public abstract class ItemUIBase<T> : UIBase where T : ItemUIBase<T>
{
	protected Camera _MainCamera;

	private RectTransform _RTsf;

	public virtual T F_Init()
	{
		_RTsf = GetComponent<RectTransform>();
		_m可见性 = true;
		_MainCamera = Camera.main;
		return this as T;
	}

	public void F_SetPos(Vector2 Pos)
	{
		_RTsf.position = _MainCamera.WorldToScreenPoint(Pos);
	}
}
