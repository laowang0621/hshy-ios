using DSB;
using UnityEngine;

public class EnemyAI_黄风带圣_龙卷风 : MonoBehaviour
{
	public Collider2D _自身碰撞器;

	public Collider2D _伤害触发器;

	public Rigidbody2D _Rb2D;

	public float _伤害判定频率;

	public float _移动速度1 = 4f;

	public float _移动速度2 = 7f;

	private float _Speed;

	private Vector2 _MoveDir;

	public C_攻击判断 _C_攻击判断;

	private float _Timer;

	public float _生命时间1 = 10f;

	public float _生命时间2 = 20f;

	public void F_Init(float 伤害)
	{
		_C_攻击判断._伤害 = 伤害;
	}

	private void Start()
	{
		Collider2D component = DSB_MonoSingleton<GameManager>.Instance._PC.GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(_自身碰撞器, component, ignore: true);
		_Speed = Random.Range(_移动速度1, _移动速度2);
		_MoveDir = Random.insideUnitCircle;
		_Timer = Random.Range(3f, 5f);
		Invoke("F_停止龙卷风", Random.Range(_生命时间1, _生命时间2));
	}

	private void F_停止龙卷风()
	{
		_自身碰撞器.enabled = false;
		_伤害触发器.enabled = false;
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ParticleSystem.EmissionModule emission = componentsInChildren[i].emission;
			emission.enabled = false;
		}
		Invoke("F_销毁", 5f);
	}

	private void F_销毁()
	{
		Object.Destroy(base.gameObject);
	}

	private void FixedUpdate()
	{
		_Rb2D.velocity = Vector2.Lerp(_Rb2D.velocity, _MoveDir * _Speed, Time.fixedDeltaTime * 10f);
		if (_Timer > 0f)
		{
			_Timer -= Time.fixedDeltaTime;
			return;
		}
		_Timer = Random.Range(3f, 5f);
		_Speed = Random.Range(_移动速度1, _移动速度2);
		_MoveDir = _MoveDir.Rotate(Random.Range(20f, 70f) * (float)((Random.value < 0.5f) ? 1 : (-1)));
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 normal = collision.contacts[0].normal;
		_MoveDir = Vector2.Reflect(_MoveDir, normal);
		_Speed = Random.Range(_移动速度1, _移动速度2);
	}
}
