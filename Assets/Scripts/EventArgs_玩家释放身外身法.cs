using System.Collections.Generic;
using DSB;

public class EventArgs_玩家释放身外身法 : DSB_EventArgs
{
	public static readonly int _EventID = typeof(EventArgs_玩家释放身外身法).GetHashCode();

	public List<EnemyAI_猴子猴孙> _List_猴子猴孙 { get; private set; }

	public static void Trigger(List<EnemyAI_猴子猴孙> list_猴子猴孙)
	{
		EventArgs_玩家释放身外身法 eventArgs_玩家释放身外身法 = new EventArgs_玩家释放身外身法();
		eventArgs_玩家释放身外身法._List_猴子猴孙 = list_猴子猴孙;
		DSB_MonoSingleton<DSB_EventManager>.Instance.Trigger(_EventID, eventArgs_玩家释放身外身法);
	}
}
