using DSB;
using UnityEngine;

public class EnemyAI_紫蛛儿_眼睛 : MonoBehaviour
{
	public float _眩晕时长 = 5f;

	public Animator _Ani;

	private bool _本次已经闪瞎玩家 = true;

	private bool _玩家进入范围;

	public void F_启动()
	{
		_本次已经闪瞎玩家 = false;
		_Ani.Play("启动");
	}

	private void Update()
	{
		if (_玩家进入范围 && !_本次已经闪瞎玩家)
		{
			float num = base.transform.position.x - DSB_MonoSingleton<GameManager>.Instance._PC.transform.position.x;
			if (DSB_MonoSingleton<GameManager>.Instance._PC._Data._朝向右边 != num < 0f)
			{
				_本次已经闪瞎玩家 = true;
				DSB_MonoSingleton<GameManager>.Instance._PC.BeAttack(new AttackInfo
				{
					atk_Is地面 = false,
					atk_可以被闪避 = false,
					atk_有僵直 = true,
					atk_Is远程 = false,
					atk_硬直是虚弱动作 = true,
					atk_强制受击 = true,
					atk_Pos = base.transform.position,
					atk_受击时长 = _眩晕时长
				});
			}
		}
	}

	public void F_关闭()
	{
		_Ani.Play("关闭");
	}

	public void F_强制关闭()
	{
		_玩家进入范围 = false;
		_Ani.Play("强制关闭", 0, 0f);
	}

	public void F_同步位置(Vector3 pos)
	{
		base.transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_玩家进入范围 = true;
		}
		if (collision.gameObject.layer == 10 && collision.TryGetComponent<IBeAttack>(out var component))
		{
			component.BeAttack(new AttackInfo
			{
				atk_生命 = 999999f,
				atk_击退力度 = 0f,
				atk_Is地面 = false,
				atk_可以被闪避 = false,
				atk_有僵直 = false,
				atk_Is远程 = false,
				atk_Pos = base.transform.position,
				atk_受击时长 = _眩晕时长
			});
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_玩家进入范围 = false;
		}
	}
}
