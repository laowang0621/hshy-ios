using DSB;
using UnityEngine;

public class C_对话_猪员外 : MonoBehaviour
{
	[Header("没有就在卧虎寺呗")]
	public bool _在黄金之国;

	public GameObject _GO_对话角色;

	public Collider2D _Co_碰撞;

	public Collider2D _Co_触发;

	public C_黄金之国_门 _黄金之国_门;

	private void OnEnable()
	{
		int 猪员外_阶段 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._猪员外_阶段;
		bool active = false;
		if (_在黄金之国)
		{
			if (猪员外_阶段 == 1 || 猪员外_阶段 == 3)
			{
				active = true;
			}
		}
		else if (猪员外_阶段 == 2)
		{
			active = true;
		}
		_GO_对话角色.SetActive(active);
		_Co_碰撞.enabled = active;
		_Co_触发.enabled = active;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		F_触发对话();
	}

	private void F_触发对话()
	{
		_ = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._猪员外_阶段;
		if (_在黄金之国)
		{
			if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得醒酒石)
			{
				DSB_MonoSingleton<UIManager>.Instance.F_触发对话(Enum_对话.新朱员外任务解锁对话, F_猪员外_四阶段);
			}
			else
			{
				DSB_MonoSingleton<UIManager>.Instance.F_触发对话(Enum_对话.黄袍员外任务1);
			}
		}
		else
		{
			DSB_MonoSingleton<UIManager>.Instance.F_触发对话(Enum_对话.黄袍员外任务3, F_猪员外_三阶段);
		}
	}

	private void F_猪员外_二阶段()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._猪员外_阶段 = 2;
	}

	private void F_猪员外_三阶段()
	{
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._猪员外_阶段 = 3;
	}

	private void F_猪员外_四阶段()
	{
		_Co_触发.enabled = false;
		DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._猪员外_阶段 = 4;
		_黄金之国_门?.F_开门();
	}
}
