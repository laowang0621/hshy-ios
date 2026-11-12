using System;
using System.Collections;
using DSB;
using UnityEngine;

public class EnemyMono组件 : MonoBehaviour
{
	public Rigidbody2D _Rb2D;

	public Animator _Ani;

	public C_攻击判断 _C_攻击判断;

	public C_AI_范围检测 _视野范围;

	public C_AI_范围检测 _攻击范围;

	public GameObject _GO_定身术特效;

	public SpriteRenderer _SR_Model;

	public SpriteRenderer _SR_影子;

	public Collider2D _Co2D_自身碰撞器;

	public Transform _Tsf_移动目标;

	public Transform _Tsf_HP;

	public Enum_Enemy _EnemyID;

	[Header("多用于持盾角色")]
	public Enum_Enemy _EnemyID2;

	[Header("可有可无，用于限制活动范围")]
	public C_活动范围 _C_活动范围;

	[Header("狼弓手用于射击")]
	public C_弹道_弓箭 _弓箭Prefab;

	[Header("--幽魂(金池)")]
	public GameObject _拍地板特效Prefab;

	[Header("--虎先锋")]
	public C_弹道_剑气 _C_弹道_剑气Prefab;

	[Header("精魄附带的相关功能")]
	public bool _死亡后跳 = true;

	[Header("同伴相关,放这应该没问题")]
	public Transform _Tsf_烟雾;

	public Transform _Tsf_血包;

	public GameObject _烟雾_Prefab;

	public C_Test_血包 _C_Test_血包;

	public Action _Event_Evemy隐藏 = delegate
	{
	};

	public Action<Action> _Event_Evemy显示 = delegate
	{
	};

	public Action _Event_Evemy死亡 = delegate
	{
	};

	public Coroutine _Cor_死亡;

	public void F_发射弹道(C_弹道 prefab, Vector2 startPos, Vector2 endPos, float atk, IBeAttack 排除对象)
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		UnityEngine.Object.Instantiate(prefab, startPos, Quaternion.identity).F_Init((endPos - startPos).normalized, atk, 排除对象);
	}

	public void F_死亡(Vector2 beAttack_Pos)
	{
		if (_Cor_死亡 != null)
		{
			StopCoroutine(_Cor_死亡);
		}
		_Cor_死亡 = StartCoroutine(Cor_死亡(beAttack_Pos));
	}

	protected IEnumerator Cor_死亡(Vector2 beAttack_Pos)
	{
		if (_死亡后跳)
		{
			_Ani.Play("死亡前跳跃");
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小妖死亡();
			_Rb2D.velocity = 18f * new Vector2(base.transform.position.x - beAttack_Pos.x, 0f).normalized;
			float num = Mathf.Abs(base.transform.localScale.x);
			base.transform.localScale = new Vector3((beAttack_Pos.x > base.transform.position.x) ? num : (0f - num), num, num);
			yield return new WaitForSeconds(0.5f);
			_Co2D_自身碰撞器.enabled = false;
			_Rb2D.velocity = Vector2.zero;
			_Ani.Play("倒地");
			yield return new WaitForSeconds(1f);
			WaitForSeconds t1 = new WaitForSeconds(0.1f);
			for (int i = 0; i < 5; i++)
			{
				_SR_Model.SetAlpha(0f);
				_SR_影子.SetAlpha(0f);
				yield return t1;
				_SR_Model.SetAlpha(1f);
				_SR_影子.SetAlpha(1f);
				yield return t1;
			}
			_Cor_死亡 = null;
			yield return null;
			base.gameObject.SetActive(value: false);
		}
		else
		{
			_Co2D_自身碰撞器.enabled = false;
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小妖死亡();
			_Rb2D.velocity = Vector2.zero;
			_Ani.Play("受击");
			float num2 = Mathf.Abs(base.transform.localScale.x);
			base.transform.localScale = new Vector3((beAttack_Pos.x > base.transform.position.x) ? num2 : (0f - num2), num2, num2);
			yield return new WaitForSeconds(0.35f);
			_Ani.Play("倒地");
			yield return new WaitForSeconds(0.85f);
			WaitForSeconds t1 = new WaitForSeconds(0.1f);
			for (int i = 0; i < 5; i++)
			{
				_SR_Model.SetAlpha(0f);
				_SR_影子.SetAlpha(0f);
				yield return t1;
				_SR_Model.SetAlpha(1f);
				_SR_影子.SetAlpha(1f);
				yield return t1;
			}
			_Cor_死亡 = null;
			yield return null;
			base.gameObject.SetActive(value: false);
		}
	}

	public void F_Ani_Paly_AudioC_轻击()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_轻攻击();
	}

	public void F_Ani_Paly_AudioC_重击()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_重攻击();
	}

	public void F_Ani_Paly_AudioC_砸地()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_砸地();
	}

	public void F_Ani_生成拍地板特效(float size)
	{
		UnityEngine.Object.Instantiate(_拍地板特效Prefab, base.transform.position, Quaternion.identity).transform.localScale = new Vector3(size, size);
	}

	public void F_Ani_屏幕震动(float 持续时间)
	{
		DSB_MonoSingleton<CameraController>.Instance.F_屏幕震动(持续时间, 0.25f, 13);
	}

	public void F_Ani_Play_攻击音效1()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_Boss攻击();
	}

	public void F_Ani_Play_大风车()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大风车();
	}

	public void F_Ani_Play_呱呱跳砸()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_呱呱跳砸();
	}

	public void F_Ani_Play_呱呱跳()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Ani_Play_呱呱跳();
	}

	public void F_Ani_生成地裂(float 偏移距离)
	{
		float x = base.transform.localScale.x;
		UnityEngine.Object.Instantiate(_拍地板特效Prefab, base.transform.position + new Vector3((x > 0f) ? 偏移距离 : (0f - 偏移距离), 0f, 0f), Quaternion.identity).transform.localScale = new Vector3((x > 0f) ? 1 : (-1), 1f, 1f);
	}

	public void F_Ani_Play_挥砍()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_挥砍();
	}

	public void F_Ani_Play_拔刀()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_拔刀();
	}

	public void F_Ani_Play_AudioC_小呱呱死亡()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小呱呱死亡();
	}

	public void F_Ani_Play_AudioC_撞击()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_撞击();
	}

	public void F_Ani_Play_AudioC_撞击_音量(float 音量)
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_撞击_音量(音量);
	}

	public void F_Ani_Play_虎先锋直拳()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_虎先锋直拳();
	}

	public void F_Play_SFX_虎先锋咆哮()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_虎先锋咆哮();
	}

	public void F_Ani_Play_SFX_投掷()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_投掷();
	}

	public void F_Ani_Play_SFX_重砸()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_重砸();
	}

	public void F_Ani_Play_SFX_大落雷()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_大落雷();
	}

	public void F_Ani_Play_SFX_小电流()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小电流();
	}

	public void F_Ani_Play_SFX_打铁_1()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_打铁_1();
	}
}
