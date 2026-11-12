using DSB;
using UnityEngine;

public class PlayerMono组件 : MonoBehaviour
{
	public Transform _Tsf_Model;

	public Transform _Tsf_影子;

	public GameObject _GO_完美闪避残影Prefab;

	public Player_蓄力特效 _Player_蓄力特效;

	public SpriteRenderer _SR_Model;

	public SpriteRenderer _SR_影子;

	public SpriteRenderer _SR_名刀;

	public ParticleSystem _PS_识破特效;

	public C_攻击判断 _C_攻击判断;

	public Rigidbody2D _Rb2D;

	public Animator _Ani;

	public GameObject _GO_拖尾;

	public GameObject _GO_拖尾2;

	public CapsuleCollider2D _Co_自身碰撞器;

	public readonly Vector2 _碰撞器_默认偏移 = new Vector2(0.05f, 0.05f);

	public readonly Vector2 _碰撞器_默认大小 = new Vector2(0.85f, 0.375f);

	public readonly Vector2 _碰撞器_下地狱偏移 = new Vector2(0.05f, 0.7f);

	public readonly Vector2 _碰撞器_下地狱大小 = new Vector2(1.12f, 1.5f);

	[Header("Player")]
	public EnemyAI_猴子猴孙 _PlayerAI_猴子猴孙;

	public EnemyAI_猴子猴孙_聚形散气 _PlayerAI_猴子猴孙_聚形散气;

	public GameObject _GO_烟雾特效Prefab;

	public Transform _TipsUI;

	public C_特效_火花 _特效_火花Prefab;

	public C_出租云 _C_出租云Prefab;

	public GameObject _打飞机云;

	[Header("精魄/化形")]
	public EnemyMono组件 _Mono组件_幽魂;

	public EnemyMono组件 _Mono组件_广智;

	public EnemyMono组件 _Mono组件_巡山鬼;

	public EnemyMono组件 _Mono组件_沙二郎;

	public EnemyMono组件 _Mono组件_不能;

	public EnemyMono组件 _Mono组件_二姐;

	public EnemyMono组件 _Mono组件_寅虎;

	public EnemyMono组件 _Mono组件_黄龙;

	public EnemyMono组件 _Mono组件_毒敌;

	[Header("相关预制件(或者组件)")]
	public C_蛇司药毒瓶 _精魄二姐_毒液_Prefab;

	public C_攻击判断 _C_攻击判断_小呱呱爆炸Prefab;

	public C_弹道_弓箭 _C_弹道_弓箭_精魄鼠弩手Prefab;

	public GameObject _GO_黄龙ModelCPro;

	public C_特效_残影残留 _GO_黄龙_特效_残影残留;

	public ParticleSystem _PS_黄龙_识破特效;

	public C_攻击判断 _C_毒液_Prefab;

	[Header("AK")]
	public GameObject _GO_枪焰;

	public C_弹道 _GO_子弹;

	public Transform _Tsf_枪焰;

	public Transform _Tsf_子弹;

	public float _子弹间隔CD = 0.03f;

	private void Start()
	{
		_PS_识破特效.gameObject.SetActive(value: true);
		_PS_黄龙_识破特效.gameObject.SetActive(value: true);
		_SR_Model.SetAlpha(1f);
		_SR_名刀.gameObject.SetActive(value: true);
		_SR_名刀.SetAlpha(0f);
		F_影藏所有化形和精魄();
		_Mono组件_广智._C_攻击判断._阵营 = 1;
		_Mono组件_广智._C_攻击判断._伤害 = 20f;
		_Mono组件_寅虎._C_攻击判断._阵营 = 1;
		_Mono组件_寅虎._C_攻击判断._伤害 = 30f;
		_Mono组件_黄龙._C_攻击判断._阵营 = 1;
		_Mono组件_黄龙._C_攻击判断._伤害 = 40f;
	}

	public void F_影藏所有化形和精魄()
	{
		_Mono组件_幽魂.gameObject.SetActive(value: false);
		_Mono组件_广智.gameObject.SetActive(value: false);
		_Mono组件_巡山鬼.gameObject.SetActive(value: false);
		_Mono组件_沙二郎.gameObject.SetActive(value: false);
		_Mono组件_不能.gameObject.SetActive(value: false);
		_Mono组件_二姐.gameObject.SetActive(value: false);
		_Mono组件_寅虎.gameObject.SetActive(value: false);
		_Mono组件_黄龙.gameObject.SetActive(value: false);
		_Mono组件_毒敌.gameObject.SetActive(value: false);
	}

	public void F_Ani_Paly_AudioC_轻击()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_轻攻击();
	}

	public void F_Ani_Paly_AudioC_重击()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_重攻击();
	}
}
