using UnityEngine;

public class C_是马楼就下地狱100层_死亡区域_上 : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 3 && collision.gameObject.CompareTag("Player") && collision.transform.TryGetComponent<PlayerController>(out var component) && !component.F_玩家已经死亡())
		{
			float atk_生命 = component._Data._Max生命 * 0.19f;
			component.BeAttack(new AttackInfo
			{
				atk_生命 = atk_生命,
				atk_僵直 = 10f,
				atk_击退力度 = 10f,
				atk_Pos = base.transform.position,
				atk_Is远程 = false,
				atk_Is地面 = false,
				atk_强破坏力 = false,
				atk_有僵直 = false,
				atk_可以被闪避 = false
			});
			component.F_地狱100层_碰撞器关闭一段时间(0.2f);
		}
	}
}
