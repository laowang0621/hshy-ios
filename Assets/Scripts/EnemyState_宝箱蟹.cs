using UnityEngine;

public abstract class EnemyState_宝箱蟹 : EnemyState_Base<EnemyAI_宝箱蟹, EnemyState_宝箱蟹, Enemy_StateData_宝箱蟹>
{
	public virtual void OnCollisionEnter2D(Collision2D collision)
	{
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
	}

	public virtual void OnTriggerExit2D(Collider2D collision)
	{
	}
}
