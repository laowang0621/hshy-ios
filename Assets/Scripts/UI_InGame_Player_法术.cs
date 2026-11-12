using DSB;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InGame_Player_法术 : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public Animator _Ani;

	protected Camera _MainCamera;

	public RectTransform _RTsf;

	private void OnEnable()
	{
		_Ani.Play("默认", 0, 0f);
	}

	private void Start()
	{
		_MainCamera = Camera.main;
		DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.右扳机.started += delegate
		{
			OnPointerDown(null);
		};
		DSB_MonoSingleton<MyInputManager>.Instance._HMLInput.HMLPaly.右扳机.canceled += delegate
		{
			OnPointerUp(null);
		};
		if (Lunch._关闭多余UI && base.gameObject.TryGetComponent<CanvasGroup>(out var component))
		{
			component.alpha = 0f;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_Ani.SetTrigger("打开");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_Ani.SetTrigger("关闭");
		if (eventData == null)
		{
			return;
		}
		Vector2 vector = _RTsf.position;
		Vector2 position = eventData.position;
		Vector2 vector2 = position - vector;
		float num = Vector2.Distance(vector, position);
		if (num < 230f && num > 30f)
		{
			if (Mathf.Abs(vector2.x) > Mathf.Abs(vector2.y))
			{
				if (vector2.x > 0f)
				{
					DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_化形();
				}
				else
				{
					DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_定身术();
				}
			}
			else if (vector2.y > 0f)
			{
				DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_隐身();
			}
			else
			{
				DSB_MonoSingleton<MyInputManager>.Instance.F_Key_Down_猴子猴孙();
			}
		}
		else
		{
			MyLog.LogError("取消释放技能");
		}
	}
}
