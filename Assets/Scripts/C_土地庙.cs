using DSB;
using UnityEngine;

public class C_土地庙 : MonoBehaviour
{
	public Transform _Tsf_UI;

	public BoxCollider2D _BC2D_传送位置;

	private ItemUI_交互 _ItemUI_存档点;

	private Vector2 _Pos1;

	private Vector2 _Pos2;

	public bool _开启指定ID上香;

	public int _当前香_MapID = -1;

	private void Awake()
	{
		_ItemUI_存档点 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_交互();
		_ItemUI_存档点._可见性 = false;
		_ItemUI_存档点.F_SetPos(_Tsf_UI.position);
		_ItemUI_存档点.F_SetText("上香");
		Vector2 vector = _BC2D_传送位置.transform.position;
		Vector2 vector2 = _BC2D_传送位置.size / 2f;
		_Pos1 = vector - vector2;
		_Pos2 = vector + vector2;
	}

	public Vector2 F_得到范围内随机点()
	{
		return new Vector2(Random.Range(_Pos1.x, _Pos2.x), Random.Range(_Pos1.y, _Pos2.y));
	}

	private void OnDestroy()
	{
		if (_ItemUI_存档点 != null)
		{
			Object.Destroy(_ItemUI_存档点.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_存档点._可见性 = true;
			_ItemUI_存档点.F_SetPos(_Tsf_UI.position);
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_上香;
		}
	}

	private void F_上香()
	{
		if (_开启指定ID上香)
		{
			DSB_MonoSingleton<GameManager>.Instance.F_上香(_当前香_MapID);
		}
		else
		{
			DSB_MonoSingleton<GameManager>.Instance.F_上香();
		}
	}

	protected virtual void LateUpdate()
	{
		if (_ItemUI_存档点._可见性)
		{
			_ItemUI_存档点.F_SetPos(_Tsf_UI.position);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_存档点._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_上香;
		}
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_上香;
	}
}
