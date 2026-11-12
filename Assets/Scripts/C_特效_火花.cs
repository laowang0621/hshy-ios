using DSB;
using UnityEngine;

public class C_特效_火花 : C_特效
{
	public Transform _Tsf_Model;

	public void F_Init(Vector2 dir)
	{
		_Tsf_Model.right = Random.insideUnitCircle;
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 0.1f, delegate
		{
			Object.Destroy(base.gameObject);
		});
	}
}
