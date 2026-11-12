using System.Collections;
using UnityEngine;

public class PlayerState_化形_黄龙_重攻击 : PlayerState_化形_Base
{
	private DataInfo_Player招式 识破;

	private DataInfo_Player招式 重击单手突刺;

	private DataInfo_Player招式 空中龙车_第一段;

	private DataInfo_Player招式 空中龙车_第二段;

	private DataInfo_Player招式 空中龙车_第三段;

	private bool _是第三段;

	private bool _成功识破;

	private bool _空中龙车连段可判定;

	private bool _继续空中龙车;

	private Vector2 _空中龙车_目标位置;

	private Vector2 _空中龙车_攻击方向;

	private Vector2 _空中龙车_当前位置;

	private float 剩余空中冲刺时间;

	public PlayerState_化形_黄龙_重攻击()
	{
		识破 = MyDataManager.F_Get_Player_招式系数(200305);
		重击单手突刺 = MyDataManager.F_Get_Player_招式系数(200304);
		空中龙车_第一段 = MyDataManager.F_Get_Player_招式系数(200306);
		空中龙车_第二段 = MyDataManager.F_Get_Player_招式系数(200307);
		空中龙车_第三段 = MyDataManager.F_Get_Player_招式系数(200308);
	}

	public override void OnEnter()
	{
		_Data._释放蓄力状态 = true;
		_成功识破 = false;
		MyLog.LogError($"黄龙_重攻击>>{_Data._重击选项}");
		if (_Data._重击选项 == Enum_化形_重攻击.普通)
		{
			_Data.F_设置攻击(重击单手突刺);
			_Data._Mono组件_化形.StartCoroutine(Cor_普通());
		}
		else if (_Data._重击选项 == Enum_化形_重攻击.识破)
		{
			_Data.F_设置攻击(识破);
			_Data._Mono组件_化形.StartCoroutine(Cor_识破());
		}
		else if (_Data._重击选项 == Enum_化形_重攻击.空中龙车)
		{
			_空中龙车连段可判定 = false;
			_继续空中龙车 = false;
			_是第三段 = false;
			_Data.F_设置攻击(空中龙车_第一段);
			_Data._Mono组件_化形.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_1_起手());
		}
		else
		{
			_Data.F_设置攻击(重击单手突刺);
			_Data._Mono组件_化形.StartCoroutine(Cor_普通());
		}
	}

	public override void OnExit()
	{
		_Data._MonoPlayer.gameObject.layer = 3;
		_Data._重击选项 = Enum_化形_重攻击.普通;
		_Data._释放蓄力状态 = false;
		_Data._Mono组件_化形.StopAllCoroutines();
		_Data._Rb2D.drag = 5f;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (attackInfo.atk_有僵直 && _Data._重击选项 == Enum_化形_重攻击.识破)
		{
			if (!_成功识破)
			{
				_Data._Mono组件Player._PS_黄龙_识破特效.Play();
				_Data._Curr蓄力 += 识破._棍势值;
				_成功识破 = true;
			}
		}
		else
		{
			base.BeAttack(attackInfo);
		}
	}

	public override void F_重攻击Down()
	{
		if (_空中龙车连段可判定)
		{
			_继续空中龙车 = true;
		}
	}

	private IEnumerator Cor_普通()
	{
		MyLog.LogError("Cor_普通>>Player_地面龙车>>1");
		_Data._Mono组件_化形._Ani.Play("Player_地面龙车", 0, 0f);
		yield return new WaitForSeconds(0.75f);
		MyLog.LogError("Cor_普通>>Player_地面龙车>>2");
		_Data.F_Move_不播放动画();
		_Data._MonoPlayer.gameObject.layer = 14;
		Vector2 vector = ((!(_Data._Dir == Vector2.zero)) ? _Data._Dir : new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
		_Data._Rb2D.drag = 0f;
		_Data._Rb2D.velocity = vector * 60f;
		yield return new WaitForSeconds(0.16666f);
		MyLog.LogError("Cor_普通>>Player_地面龙车>>3");
		_Data._Rb2D.drag = 5f;
		_Data._MonoPlayer.gameObject.layer = 3;
		yield return new WaitForSeconds(0.25f);
		MyLog.LogError("Cor_普通>>Player_地面龙车>>4");
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	private IEnumerator Cor_识破()
	{
		_Data._Mono组件_化形._Ani.Play("Player_识破", 0, 0f);
		Vector2 currDir = ((!(_Data._Dir == Vector2.zero)) ? _Data._Dir : new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
		yield return new WaitForSeconds(0.2f);
		_Data._Rb2D.velocity = currDir * 12f;
		yield return new WaitForSeconds(0.25f);
		_Data._Rb2D.velocity = currDir * 12f;
		yield return new WaitForSeconds(0.3f);
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	private void F_更新攻击方向()
	{
		_空中龙车_当前位置 = _Data._MonoPlayer.transform.position;
		Collider2D[] array = Physics2D.OverlapBoxAll(_空中龙车_当前位置, new Vector2(30f, 12f), 0f, LayerMask.GetMask("Enemy"));
		if (array.Length != 0)
		{
			Collider2D collider2D = array[0];
			float num = Vector2.Distance(_空中龙车_当前位置, collider2D.transform.position);
			for (int i = 1; i < array.Length; i++)
			{
				float num2 = Vector2.Distance(_空中龙车_当前位置, array[i].transform.position);
				if (num2 < num)
				{
					num = num2;
					collider2D = array[i];
				}
			}
			_空中龙车_目标位置 = collider2D.transform.position;
		}
		else
		{
			MyLog.LogWarning("范围内无目标");
		}
		_空中龙车_攻击方向 = (_空中龙车_目标位置 - _空中龙车_当前位置).normalized;
		_Data._MonoPlayer.F_玩家转向_强制(_空中龙车_攻击方向.x);
	}

	private void F_空中龙车_继续是否升空()
	{
		_空中龙车连段可判定 = false;
		MyLog.LogError($"F_空中龙车_继续是否升空>>{_继续空中龙车}");
		if (_Data._Curr蓄力 > 0.95f && _继续空中龙车)
		{
			_继续空中龙车 = false;
			if (_是第三段)
			{
				_Data.F_设置攻击(空中龙车_第三段);
			}
			else
			{
				_是第三段 = true;
				_Data.F_设置攻击(空中龙车_第二段);
			}
			_Data._Mono组件_化形.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(剩余空中冲刺时间));
		}
		else
		{
			_Data._Mono组件_化形.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_2_冲刺后落地());
		}
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_1_起手()
	{
		_Data._Mono组件_化形._Ani.Play("攻击_插刀磁暴_空中龙车_四连_1_起手", 0, 0f);
		_Data._Rb2D.velocity = Vector2.zero;
		_空中龙车_目标位置 = _Data._MonoPlayer.transform.position;
		_空中龙车_目标位置.x += (_Data._朝向右边 ? 1 : (-1));
		F_更新攻击方向();
		yield return new WaitForSeconds(1.2f);
		_空中龙车连段可判定 = true;
		_Data._Curr蓄力 -= 1f;
		_Data._MonoPlayer.gameObject.layer = 14;
		yield return new WaitForSeconds(0.3f);
		F_更新攻击方向();
		_Data._Rb2D.drag = 0f;
		float num = Vector2.Distance(_Data._MonoPlayer.transform.position, _空中龙车_目标位置 + _空中龙车_攻击方向 * 4.5f);
		float num2 = 45f;
		_Data._Rb2D.velocity = _空中龙车_攻击方向 * num2;
		float num3 = num / num2;
		剩余空中冲刺时间 = num3 / 6f;
		yield return new WaitForSeconds(剩余空中冲刺时间 * 3f);
		F_空中龙车_继续是否升空();
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_2_冲刺后落地()
	{
		_Data._Rb2D.drag = 5f;
		_继续空中龙车 = false;
		_Data._Mono组件_化形._Ani.Play("攻击_插刀磁暴_空中龙车_四连_2_冲刺后落地", 0, 0f);
		yield return new WaitForSeconds(0.33f);
		_Data._不可受击状态 = true;
		F_更新攻击方向();
		_Data._MonoPlayer.gameObject.layer = 3;
		yield return new WaitForSeconds(0.33f);
		F_化形_ChangeState(TypeName_化形.正常移动);
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_2_冲刺后升空(float 冲刺剩余时间)
	{
		_Data._Rb2D.drag = 3f;
		_Data._Mono组件_化形._Ani.Play("攻击_插刀磁暴_空中龙车_四连_2_冲刺后升空", 0, 0f);
		yield return new WaitForSeconds(冲刺剩余时间 * 2f);
		_Data._不可受击状态 = true;
		F_更新攻击方向();
		yield return new WaitForSeconds(0.5f);
		F_更新攻击方向();
		yield return new WaitForSeconds(0.25f);
		_Data._Mono组件_化形.StartCoroutine(Cor_插刀磁暴_空中龙车_四连_3_空中冲刺());
	}

	private IEnumerator Cor_插刀磁暴_空中龙车_四连_3_空中冲刺()
	{
		_Data._Mono组件_化形._Ani.Play("攻击_插刀磁暴_空中龙车_四连_3_空中冲刺", 0, 0f);
		_Data._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return new WaitForSeconds(0.4f);
		_Data._Curr蓄力 -= 1f;
		_空中龙车连段可判定 = true;
		_Data._不可受击状态 = false;
		_Data._Rb2D.drag = 0f;
		yield return new WaitForSeconds(0.1f);
		F_更新攻击方向();
		float num = Vector2.Distance(_Data._MonoPlayer.transform.position, _空中龙车_目标位置 + _空中龙车_攻击方向 * 3.5f);
		float num2 = 45f;
		_Data._Rb2D.drag = 0f;
		_Data._Rb2D.velocity = _空中龙车_攻击方向 * num2;
		float num3 = num / num2;
		剩余空中冲刺时间 = num3 / 6f;
		yield return new WaitForSeconds(剩余空中冲刺时间 * 3f);
		F_空中龙车_继续是否升空();
	}
}
