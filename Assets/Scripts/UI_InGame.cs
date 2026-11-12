using UnityEngine;

public class UI_InGame : UIBase
{
	[Header("预制体")]
	[SerializeField]
	private ItemUI_EnemyHP _ItemUI_EnemyHP_Prefab;

	[SerializeField]
	private ItemUI_EnemyHP_带盾 _ItemUI_EnemyHP_带盾_Prefab;

	[SerializeField]
	private ItemUI_BossHP _ItemUI_BossHP_Prefab;

	[SerializeField]
	private ItemUI_交互 _ItemUI_交互;

	[SerializeField]
	private ItemUI_文本 _ItemUI_文本;

	[SerializeField]
	private ItemUI_出租云 _ItemUI_出租云;

	[Header("面板")]
	public UI_InGame_Main _InGame_Main;

	public UI_InGame_Pause _InGame_Pause;

	public UI_遮罩 _InGame_遮罩;

	public UI_InGame_得胜 _UI_InGame_得胜;

	public UI_InGame_对话 _UI_InGame_对话;

	public UI_InGame_Tips _UI_InGame_Tips;

	public UI_InGame_章节提示 _UI_InGame_章节提示;

	public UI_InGame_获得道具提示 _UI_InGame_获得丹药提示;

	public void F_隐藏所有子面板()
	{
		_InGame_遮罩._可见性 = false;
		_InGame_Main._可见性 = false;
		_InGame_Pause._可见性 = false;
		_UI_InGame_得胜._可见性 = false;
		_UI_InGame_对话._可见性 = false;
		_UI_InGame_Tips._可见性 = false;
		_UI_InGame_章节提示._可见性 = false;
		_UI_InGame_获得丹药提示._可见性 = false;
	}

	public void F_隐藏()
	{
		base._CG.alpha = 0f;
		base._CG.blocksRaycasts = false;
	}

	public void F_设置技能的显示()
	{
		_InGame_Main.F_设置技能的显示();
	}

	public ItemUI_EnemyHP F_Get_ItemUI_EnemyHP()
	{
		return Object.Instantiate(_ItemUI_EnemyHP_Prefab, _InGame_Main._RectBasePos_ItemUI_EnemyHP).F_Init();
	}

	public ItemUI_EnemyHP_带盾 F_Get_ItemUI_EnemyHP_带盾()
	{
		return (ItemUI_EnemyHP_带盾)Object.Instantiate(_ItemUI_EnemyHP_带盾_Prefab, _InGame_Main._RectBasePos_ItemUI_EnemyHP).F_Init();
	}

	public ItemUI_BossHP F_Get_ItemUI_BossHP()
	{
		return Object.Instantiate(_ItemUI_BossHP_Prefab, _InGame_Main._RectBasePos_ItemUI_EnemyHP_Boss).F_Init();
	}

	public ItemUI_交互 F_Get_ItemUI_交互()
	{
		return Object.Instantiate(_ItemUI_交互, _InGame_Main._RectBasePos_Tips).F_Init();
	}

	public ItemUI_文本 F_Get_ItemUI_文本()
	{
		return Object.Instantiate(_ItemUI_文本, _InGame_Main._RectBasePos_Tips).F_Init();
	}

	public ItemUI_出租云 F_Get_ItemUI_出租云()
	{
		return Object.Instantiate(_ItemUI_出租云, _InGame_Main._RectBasePos_Tips).F_Init();
	}
}
