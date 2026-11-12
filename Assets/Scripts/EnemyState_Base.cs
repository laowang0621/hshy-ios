using System.Collections;
using DSB;
using UnityEngine;

public class EnemyState_Base<TAI, TState, TStateData> : DSB_State<TState, TStateData> where TAI : EnemyAI_Base<TAI, TState, TStateData> where TState : EnemyState_Base<TAI, TState, TStateData> where TStateData : Enemy_StateData_Base<TAI, TState, TStateData>
{
	protected float _Timer;

	public TAI _Mono => _Data._Mono;

	public EnemyMono组件 _Mono组件 => _Data._Mono组件;

	public Rigidbody2D _Rb2D => _Mono组件._Rb2D;

	public Animator _Ani => _Mono组件._Ani;

	public void ChangeState(EnemyStateName_Type stateName)
	{
		_Mono.ChangeState(stateName);
	}

	public IEnumerator Cor_用于子类延迟等待(float time1)
	{
		while (time1 > 0f)
		{
			if (!_Mono._Is被定)
			{
				time1 -= Time.deltaTime;
			}
			yield return null;
		}
	}
}
