using DSB;
using UnityEngine;

public class EnemyState_宝箱蟹_散步 : EnemyState_宝箱蟹
{
	public override void OnEnter()
	{
		base._Ani.Play("移动");
		base._Mono.F_反方向移动();
		_Timer = Random.Range(2f, 4f);
		_Data._Mono.transform.localScale = new Vector3((base._Mono._Dir.x < 0f) ? 1 : (-1), 1f, 1f);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer < 0f)
		{
			base._Mono._Dir = (-base._Mono._Dir).Rotate(Random.Range(25, 45) * ((Random.value < 0.5f) ? 1 : (-1)));
			_Timer = Random.Range(1.5f, 3.5f);
		}
		_Data._Mono组件._Rb2D.velocity = base._Mono._Dir * base._Mono._移动速度;
	}

	public override void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 normal = collision.contacts[0].normal;
		base._Mono._Dir = Vector2.Reflect(base._Mono._Dir, normal);
		base._Mono._Dir = base._Mono._Dir.Rotate(Random.Range(-25, 25));
		_Data._Mono.transform.localScale = new Vector3((base._Mono._Dir.x < 0f) ? 1 : (-1), 1f, 1f);
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			base._Mono.ChangeState(EnemyStateName_Type.攻击);
		}
	}
}
