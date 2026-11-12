using UnityEngine;

public class EnemyState_狸侍长_散步 : EnemyState_狼剑客
{
	private Vector2 _StartPos;

	private float _Speed = 2f;

	private EnemyAI_狸侍长 _Mono_狸侍长;

	private Vector3[] _巡逻路径;

	private float _距离;

	private int _当前巡逻路径目标点 = -1;

	public override EnemyState_狼剑客 Init(Enemy_StateData_狼剑客 data)
	{
		base.Init(data);
		_Mono_狸侍长 = (EnemyAI_狸侍长)base._Mono;
		_巡逻路径 = new Vector3[_Mono_狸侍长._巡逻路径.positionCount];
		_Mono_狸侍长._巡逻路径.GetPositions(_巡逻路径);
		Vector3 position = _Mono_狸侍长._巡逻路径.transform.position;
		for (int i = 0; i < _巡逻路径.Length; i++)
		{
			_巡逻路径[i] += position;
		}
		return this;
	}

	public override void OnEnter()
	{
		base._Ani.Play("移动");
		_StartPos = base._Mono.transform.position;
		if (_当前巡逻路径目标点 < 0)
		{
			_当前巡逻路径目标点 = 0;
		}
		else
		{
			_当前巡逻路径目标点++;
		}
		_当前巡逻路径目标点 %= _巡逻路径.Length;
		_Mono_狸侍长._巡逻目标位置 = _巡逻路径[_当前巡逻路径目标点];
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3((_Mono_狸侍长._巡逻目标位置.x > _StartPos.x) ? num : (0f - num), num, num);
		_距离 = (_Mono_狸侍长._巡逻目标位置 - _StartPos).magnitude;
		_Timer = 0f;
		base._Mono组件._Tsf_移动目标.transform.position = _Mono_狸侍长._巡逻目标位置;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer += Time.deltaTime * _Speed;
		base._Rb2D.velocity = Vector2.zero;
		base._Rb2D.MovePosition(Vector2.Lerp(_StartPos, _Mono_狸侍长._巡逻目标位置, _Timer / _距离));
		if (_Mono_狸侍长._视野范围有敌人)
		{
			ChangeState(EnemyStateName_Type.追击);
		}
		if (_Timer >= _距离)
		{
			ChangeState(EnemyStateName_Type.站立);
		}
	}
}
