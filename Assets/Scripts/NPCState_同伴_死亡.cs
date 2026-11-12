public class NPCState_同伴_死亡 : NPCState_同伴
{
	public override void OnEnter()
	{
		_Mono_同伴.gameObject.SetActive(value: false);
	}

	public override void OnExit()
	{
		_Mono_同伴.gameObject.SetActive(value: true);
	}

	public override void OnUpdate()
	{
	}
}
