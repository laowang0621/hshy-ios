using System.Collections;
using DSB;
using UnityEngine;

public class PlayerState_精魄 : Player_StateBase
{
	private float _不能基础伤害;

	private DataInfo_Player技能 _技能_幽魂;

	private DataInfo_Player技能 _技能_小呱呱;

	private DataInfo_Player技能 _技能_鼠弩手;

	private DataInfo_Player技能 _技能_沙二郎;

	private DataInfo_Player技能 _技能_寻山鬼;

	private DataInfo_Player技能 _技能_不能;

	private DataInfo_Player技能 _技能_二姐;

	private DataInfo_Player技能 _技能_毒敌大王;

	private Transform _Tsf_精魄_二姐_手;

	protected DataInfo_Enemy招式数值 毒液其他参数;

	protected DataInfo_Enemy招式数值 毒液其他参数2;

	protected DataInfo_Player招式 毒敌毒爆;

	private float _不能招式_攻击架势;

	private float _不能招式_普通拳;

	private float _不能招式_跳砸;

	private float _不能招式_掌波;

	protected Coroutine _Cor_精魄;

	public override Player_StateBase Init(Player_StateData data)
	{
		base.Init(data);
		_技能_幽魂 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1005);
		base._Mono组件._Mono组件_幽魂._C_攻击判断._阵营 = base._Mono._阵营;
		base._Mono组件._Mono组件_幽魂._C_攻击判断._僵直伤害 = _技能_幽魂._持续时间;
		base._Mono组件._Mono组件_幽魂._C_攻击判断._是强破坏力 = true;
		_技能_小呱呱 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1006);
		base._Mono组件._C_攻击判断_小呱呱爆炸Prefab._阵营 = base._Mono._阵营;
		base._Mono组件._C_攻击判断_小呱呱爆炸Prefab._僵直伤害 = _技能_小呱呱._持续时间;
		_技能_鼠弩手 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1007);
		base._Mono组件._C_弹道_弓箭_精魄鼠弩手Prefab._阵营 = base._Mono._阵营;
		base._Mono组件._C_弹道_弓箭_精魄鼠弩手Prefab._僵直伤害 = _技能_鼠弩手._持续时间;
		_技能_沙二郎 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1008);
		base._Mono组件._Mono组件_沙二郎._C_攻击判断._阵营 = base._Mono._阵营;
		base._Mono组件._Mono组件_沙二郎._C_攻击判断._僵直伤害 = _技能_沙二郎._持续时间;
		base._Mono组件._Mono组件_幽魂._C_攻击判断._是强破坏力 = true;
		_技能_寻山鬼 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1009);
		base._Mono组件._Mono组件_巡山鬼._C_攻击判断._阵营 = base._Mono._阵营;
		base._Mono组件._Mono组件_巡山鬼._C_攻击判断._僵直伤害 = _技能_寻山鬼._持续时间;
		_技能_不能 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1010);
		base._Mono组件._Mono组件_不能._C_攻击判断._阵营 = base._Mono._阵营;
		base._Mono组件._Mono组件_不能._C_攻击判断._僵直伤害 = _技能_不能._持续时间;
		_不能招式_攻击架势 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9011)._伤害系数;
		_不能招式_普通拳 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9001)._伤害系数;
		_不能招式_跳砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9010)._伤害系数;
		_不能招式_掌波 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(9004)._伤害系数;
		_技能_二姐 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1011);
		base._Mono组件._Mono组件_二姐._C_攻击判断._阵营 = base._Mono._阵营;
		base._Mono组件._Mono组件_二姐._C_攻击判断._僵直伤害 = _技能_二姐._持续时间;
		_Tsf_精魄_二姐_手 = base._Mono组件._Mono组件_二姐.transform.Find("Tsf_手");
		毒液其他参数 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13008);
		毒液其他参数2 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(13009);
		if (_Tsf_精魄_二姐_手 == null)
		{
			MyLog.LogError("Tsf_精魄_二姐_手 is null, please check the prefab.");
		}
		_技能_毒敌大王 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_技能(1012);
		毒敌毒爆 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.F_Get_Player_招式系数(2017);
		base._Mono组件._Mono组件_毒敌._C_攻击判断._阵营 = base._Mono._阵营;
		base._Mono组件._Mono组件_毒敌._C_攻击判断._僵直伤害 = _技能_毒敌大王._持续时间;
		return this;
	}

	public override void OnEnter()
	{
		int 当前使用精魄 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._当前使用精魄;
		_Data_Player_技能 = MyDataManager.F_Get_Player_技能(当前使用精魄);
		_Data.F_设置技能CD(_Data._精魄TimerID, _Data_Player_技能._冷却时间);
		_Data._释放蓄力状态 = true;
		if (_Cor_精魄 != null)
		{
			base._Mono.StopCoroutine(_Cor_精魄);
		}
		switch (当前使用精魄)
		{
		case 1005:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_幽魂_磕头());
			break;
		case 1006:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_小呱呱_自爆());
			break;
		case 1007:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_鼠弩手());
			break;
		case 1008:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_沙二郎());
			break;
		case 1009:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_巡山鬼());
			break;
		case 1010:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_不能());
			break;
		case 1011:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_二姐_吐毒());
			break;
		case 1012:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_毒敌大王());
			break;
		default:
			_Cor_精魄 = base._Mono.StartCoroutine(Cor_幽魂_磕头());
			break;
		}
	}

	public override void OnExit()
	{
		if (_Cor_精魄 != null)
		{
			base._Mono.StopCoroutine(_Cor_精魄);
			_Cor_精魄 = null;
		}
		_Data._释放蓄力状态 = false;
		base._Mono组件._SR_Model.SetAlpha(1f);
		base._Mono组件.F_影藏所有化形和精魄();
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		base._Rb2D.drag = 5f;
		base._Rb2D.mass = 5f;
	}

	protected IEnumerator Cor_幽魂_磕头()
	{
		base._Rb2D.mass = 999f;
		base._Mono组件._Mono组件_幽魂._C_攻击判断._伤害 = _Data._攻击力 * _技能_幽魂._法力消耗;
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._Mono组件_幽魂.gameObject.SetActive(value: true);
		base._Mono组件._Mono组件_幽魂._Ani.Play("磕头", 0, 0f);
		yield return new WaitForSeconds(1.15f);
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	protected IEnumerator Cor_毒敌大王()
	{
		base._Rb2D.mass = 999f;
		base._Mono组件._Mono组件_毒敌._C_攻击判断._伤害 = _Data._攻击力 * _技能_毒敌大王._法力消耗;
		if (Random.value < 0.8f)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_尾后针();
		}
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._Mono组件_毒敌.gameObject.SetActive(value: true);
		base._Mono组件._Mono组件_毒敌._Ani.Play("攻击_尾后针", 0, 0f);
		bool 当前朝右 = _Data._朝向右边;
		yield return new WaitForSeconds(0.25f);
		for (int i = 0; i <= 3; i++)
		{
			Vector2 pos = base._Mono组件.transform.position + new Vector3((当前朝右 ? 1 : (-1)) * (3 + 3 * i), 0f);
			F_生成毒爆(pos);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.45f);
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	private void F_生成毒爆(Vector2 pos)
	{
		C_攻击判断 c_攻击判断 = Object.Instantiate(base._Mono组件._C_毒液_Prefab, pos, Quaternion.identity);
		c_攻击判断._伤害 = 毒敌毒爆._伤害系数 * _Data._攻击力;
		c_攻击判断._击退力度 = 毒敌毒爆._击退力度;
	}

	protected void F_生成一个毒液()
	{
		C_蛇司药毒瓶 c_蛇司药毒瓶 = Object.Instantiate(base._Mono组件._精魄二姐_毒液_Prefab, _Tsf_精魄_二姐_手.position, Quaternion.identity);
		Vector2 vector = base._Mono组件.transform.position + new Vector3((float)(_Data._朝向右边 ? 1 : (-1)) * 12.5f, 0f);
		Vector2 vector2 = Random.insideUnitCircle * 5f;
		vector2.y *= 0.5f;
		c_蛇司药毒瓶.F_Init(vector + vector2, base._Mono组件._Mono组件_二姐._C_攻击判断._伤害, base._Mono组件._Mono组件_二姐._C_攻击判断._僵直伤害, Random.Range(毒液其他参数._伤害系数, 毒液其他参数._击退力度), 1);
	}

	protected IEnumerator Cor_二姐_吐毒()
	{
		base._Rb2D.mass = 999f;
		base._Mono组件._Mono组件_二姐._C_攻击判断._伤害 = _Data._攻击力 * _技能_二姐._法力消耗;
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._Mono组件_二姐.gameObject.SetActive(value: true);
		base._Mono组件._Mono组件_二姐._Ani.Play("攻击_吐毒", 0, 0f);
		yield return new WaitForSeconds(0.62f);
		int count = 2 + Mathf.CeilToInt(Random.Range(毒液其他参数2._伤害系数, 毒液其他参数2._击退力度));
		for (int i = 0; i < count; i++)
		{
			F_生成一个毒液();
			yield return new WaitForSeconds(0.08f);
		}
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	protected IEnumerator Cor_小呱呱_自爆()
	{
		base._Mono组件._C_攻击判断_小呱呱爆炸Prefab._伤害 = _Data._攻击力 * _技能_小呱呱._法力消耗;
		base._Ani.Play("精魄_小呱呱");
		yield return new WaitForSeconds(0.33f);
		for (int i = 0; i < 3; i++)
		{
			base._Rb2D.velocity = new Vector2((_Data._朝向右边 ? 1 : (-1)) * 25, 0f);
			yield return new WaitForSeconds(0.06f);
		}
		yield return new WaitForSeconds(0.18f);
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._C_攻击判断_小呱呱爆炸Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	protected IEnumerator Cor_鼠弩手()
	{
		base._Ani.Play("精魄_鼠弩手");
		base._Mono组件._C_弹道_弓箭_精魄鼠弩手Prefab._伤害 = _Data._攻击力 * _技能_鼠弩手._法力消耗;
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return new WaitForSeconds(0.75f);
		for (int i = 0; i < 6; i++)
		{
			F_发射弓箭();
			yield return new WaitForSeconds(0.1f);
		}
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	private void F_发射弓箭()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		Object.Instantiate(base._Mono组件._C_弹道_弓箭_精魄鼠弩手Prefab, base._Mono.transform.position, Quaternion.identity).F_Init(_Data._朝向右边 ? Vector2.right : (-Vector2.right));
	}

	protected IEnumerator Cor_巡山鬼()
	{
		base._Mono组件._Mono组件_巡山鬼._C_攻击判断._伤害 = _Data._攻击力 * _技能_寻山鬼._法力消耗;
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._Mono组件_巡山鬼.gameObject.SetActive(value: true);
		base._Mono组件._Mono组件_巡山鬼._Ani.Play("攻击_3", 0, 0f);
		yield return new WaitForSeconds(1.42f);
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	protected IEnumerator Cor_沙二郎()
	{
		base._Mono组件._Mono组件_沙二郎._C_攻击判断._伤害 = _Data._攻击力 * _技能_沙二郎._法力消耗;
		base._Rb2D.drag = 1.8f;
		base._Rb2D.mass = 999f;
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._Mono组件_沙二郎.gameObject.SetActive(value: true);
		base._Mono组件._Mono组件_沙二郎._Ani.Play("冲撞_0", 0, 0f);
		yield return new WaitForSeconds(0.6f);
		base._Mono组件._Mono组件_沙二郎._Ani.Play("冲撞_1", 0, 0f);
		Vector2 currDir = ((!(_Data._Dir == Vector2.zero)) ? _Data._Dir : new Vector2(_Data._朝向右边 ? 1 : (-1), 0f));
		base._Rb2D.velocity = currDir * 42f;
		yield return new WaitForSeconds(0.15f);
		base._Rb2D.velocity = currDir * 38f;
		yield return new WaitForSeconds(0.4f);
		base._Rb2D.drag = 5f;
		base._Rb2D.mass = 5f;
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	protected IEnumerator Cor_不能()
	{
		base._Rb2D.mass = 999f;
		_不能基础伤害 = _Data._攻击力 * _技能_不能._法力消耗;
		Vector2 vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		base._Mono组件._SR_Model.SetAlpha(0f);
		base._Mono组件._Mono组件_不能.gameObject.SetActive(value: true);
		if (_Data._State_攻击连段_判定 == Player_StateName_Type.轻棍2)
		{
			base._Mono组件._Mono组件_不能._C_攻击判断._伤害 = _不能基础伤害 * _不能招式_普通拳;
			base._Mono组件._Mono组件_不能._Ani.Play("攻击_三连拳_2", 0, 0f);
			yield return new WaitForSeconds(0.2f);
			base._Rb2D.velocity = new Vector2(_Data._朝向右边 ? 25 : (-25), 0f);
			yield return new WaitForSeconds(0.6f);
			_Data._State_攻击连段_判定Timer = 0.5f;
			_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍2;
		}
		else if (_Data._State_攻击连段_判定 == Player_StateName_Type.轻棍3)
		{
			base._Mono组件._Mono组件_不能._C_攻击判断._伤害 = _不能基础伤害 * _不能招式_跳砸;
			base._Mono组件._Mono组件_不能._Ani.Play("攻击_跳砸_Player", 0, 0f);
			yield return new WaitForSeconds(0.2f);
			base._Rb2D.velocity = new Vector2(_Data._朝向右边 ? 50 : (-50), 0f);
			yield return new WaitForSeconds(0.6f);
			_Data._State_攻击连段_判定Timer = 0.5f;
			_Data._State_攻击连段_判定 = Player_StateName_Type.轻棍3;
		}
		else if (_Data._State_攻击连段_判定 == Player_StateName_Type.轻棍4)
		{
			base._Mono组件._Mono组件_不能._C_攻击判断._伤害 = _不能基础伤害 * _不能招式_掌波;
			base._Mono组件._Mono组件_不能._Ani.Play("攻击_三连拳_4", 0, 0f);
			yield return new WaitForSeconds(0.1f);
			base._Rb2D.velocity = new Vector2(_Data._朝向右边 ? (-18) : 18, 0f);
			yield return new WaitForSeconds(0.8f);
		}
		else
		{
			base._Mono组件._Mono组件_不能._C_攻击判断._伤害 = _不能基础伤害 * _不能招式_攻击架势;
			base._Mono组件._Mono组件_不能._Ani.Play("攻击架势", 0, 0f);
			yield return new WaitForSeconds(0.9f);
		}
		vector = base._Mono组件.transform.position;
		Object.Instantiate(base._Mono组件._GO_烟雾特效Prefab, vector + new Vector2(0f, 1.15f), Quaternion.identity);
		yield return null;
		_Cor_精魄 = null;
		ChangeState(Player_StateName_Type.正常移动);
	}

	public override void OnUpdate()
	{
	}
}
