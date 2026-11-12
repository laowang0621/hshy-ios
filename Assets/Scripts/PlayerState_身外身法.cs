using System.Collections.Generic;
using DSB;
using UnityEngine;

public class PlayerState_身外身法 : Player_StateBase
{
	private bool _成功释放;

	public PlayerState_身外身法()
	{
		_Data_Player_技能 = MyDataManager.F_Get_Player_技能(104);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("定身术");
		_Data.F_设置技能CD(_Data._身外身法TimerID, _Data_Player_技能._冷却时间);
		_Data._Curr法力 -= _Data_Player_技能._法力消耗;
		_Timer = 0.65f;
		_成功释放 = false;
	}

	public override void OnExit()
	{
		if (!_成功释放)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_力竭();
			return;
		}
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_分身();
		bool 朝向右边 = _Data._朝向右边;
		int num = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.身外身法数量 + Mathf.RoundToInt(DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1502));
		int num2 = 180 / num;
		float num3 = -num2 * num / 2 + ((!朝向右边) ? 180 : 0);
		if ((num & 1) != 1)
		{
			num3 += (float)(num2 / 2);
		}
		int value = Mathf.RoundToInt(DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1503));
		List<DataInfo_猴子猴孙等级> list_猴子猴孙等级 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_PlayerOther._List_猴子猴孙等级;
		DataInfo_猴子猴孙等级 levelData = list_猴子猴孙等级[Mathf.Clamp(value, 0, list_猴子猴孙等级.Count - 1)];
		List<EnemyAI_猴子猴孙> list = new List<EnemyAI_猴子猴孙>();
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2(3f, 0f).Rotate(num3 + (float)(num2 * i));
			Vector2 vector2 = base._Mono组件.transform.position;
			Vector2 vector3 = vector2 + vector;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, vector, 3f, 2048);
			if ((bool)raycastHit2D)
			{
				vector3 = raycastHit2D.point;
			}
			Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector3 + new Vector2(0f, 1.15f), Quaternion.identity);
			EnemyAI_猴子猴孙 enemyAI_猴子猴孙 = Object.Instantiate(base._Mono组件._PlayerAI_猴子猴孙, vector3, Quaternion.identity);
			list.Add(enemyAI_猴子猴孙);
			if (!朝向右边)
			{
				float num4 = Mathf.Abs(base._Mono.transform.localScale.x);
				enemyAI_猴子猴孙.transform.localScale = new Vector3(0f - num4, num4, num4);
				enemyAI_猴子猴孙._Data._朝向右边 = false;
			}
			enemyAI_猴子猴孙.F_绑定玩家(base._Mono, _Data_Player_技能._持续时间 + DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1501), levelData);
		}
		EventArgs_玩家释放身外身法.Trigger(list);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			_成功释放 = true;
			ChangeState(Player_StateName_Type.正常移动);
		}
	}
}
