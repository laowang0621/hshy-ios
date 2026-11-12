using UnityEngine;

public class C_划沙器 : MonoBehaviour
{
	public C_触发器 _入口;

	public C_触发器 _出口;

	public Transform _Tsf_Start;

	private void Start()
	{
		_入口._Enter += delegate(Collider2D co)
		{
			if (co.TryGetComponent<PlayerController>(out var component2))
			{
				component2.F_SetPos(_Tsf_Start.position);
				component2.F_进入滑沙场地();
				EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.滑沙, 是进入: true);
			}
		};
		_出口._Exit += delegate(Collider2D co)
		{
			if (co.TryGetComponent<PlayerController>(out var component))
			{
				component.F_离开滑沙场地();
				EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.滑沙, 是进入: false);
			}
		};
	}

	private void OnDisable()
	{
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.滑沙, 是进入: false);
	}
}
