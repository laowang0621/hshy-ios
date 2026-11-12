using UnityEngine;
using UnityEngine.UI;

public class UI_版本号 : UIBase
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		GetComponentInChildren<Text>().text = "v" + Application.version;
	}
}
