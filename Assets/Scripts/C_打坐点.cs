using DSB;
using UnityEngine;

public class C_打坐点 : MonoBehaviour
{
	public Transform _Tsf_UI;

	private ItemUI_交互 _ItemUI_打坐点;

	public int _打坐点ID = -1;

	private void Start()
	{
		_ItemUI_打坐点 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_交互();
		_ItemUI_打坐点._可见性 = false;
		_ItemUI_打坐点.F_SetText("打坐");
		_ItemUI_打坐点.F_SetPos(_Tsf_UI.position);
	}

	private void OnDestroy()
	{
		if (_ItemUI_打坐点 != null)
		{
			Object.Destroy(_ItemUI_打坐点.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_打坐点.F_SetPos(_Tsf_UI.position);
			_ItemUI_打坐点._可见性 = true;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_打坐;
		}
	}

	private void F_打坐()
	{
		_ItemUI_打坐点._可见性 = false;
		DSB_MonoSingleton<GameManager>.Instance.F_打坐(_打坐点ID, base.transform.position);
	}

	protected virtual void LateUpdate()
	{
		if (_ItemUI_打坐点._可见性)
		{
			_ItemUI_打坐点.F_SetPos(_Tsf_UI.position);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_ItemUI_打坐点._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_打坐;
		}
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_打坐;
	}
}
