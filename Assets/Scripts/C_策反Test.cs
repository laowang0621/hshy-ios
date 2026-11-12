using UnityEngine;

public class C_策反Test : MonoBehaviour
{
	[HideInInspector]
	public EnemyAI_猴子猴孙 _策反目标_EnemyAI_猴子猴孙;

	private Vector2 _TargetPos;

	private bool _完成移动;

	public void F_绑定猴子猴孙(EnemyAI_猴子猴孙 enemyAI_猴子猴孙)
	{
		_策反目标_EnemyAI_猴子猴孙 = enemyAI_猴子猴孙;
		_TargetPos = _策反目标_EnemyAI_猴子猴孙._Tsf_策反特效Pos.position;
	}

	private void Update()
	{
		if (_完成移动)
		{
			return;
		}
		if (_策反目标_EnemyAI_猴子猴孙 != null)
		{
			_TargetPos = _策反目标_EnemyAI_猴子猴孙._Tsf_策反特效Pos.position;
		}
		Vector2 vector = _TargetPos - (Vector2)base.transform.position;
		float magnitude = vector.magnitude;
		if (magnitude < 0.45f)
		{
			if (_策反目标_EnemyAI_猴子猴孙 != null)
			{
				_策反目标_EnemyAI_猴子猴孙.F_策反();
				base.transform.parent = _策反目标_EnemyAI_猴子猴孙._Tsf_策反特效Pos;
				base.transform.localPosition = Vector3.zero;
				_完成移动 = true;
				base.enabled = false;
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			base.transform.Translate(vector / magnitude * Time.deltaTime * 15f);
		}
	}
}
