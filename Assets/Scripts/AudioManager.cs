using System.Collections.Generic;
using DSB;
using UnityEngine;

public class AudioManager : DSB_MonoSingleton<AudioManager>
{
	public AudioSource _AudioS_BGM;

	public AudioSource _AudioS_UISFX;

	public AudioSource _AudioS_SFX;

	private Dictionary<string, AudioClip> _Dict_Name_AudioC = new Dictionary<string, AudioClip>();

	public void F_OnAwake()
	{
		_AudioS_BGM.ignoreListenerPause = true;
		_AudioS_UISFX.ignoreListenerPause = true;
		foreach (KeyValuePair<string, SO_Item_Audio> item in DSB_MonoSingleton<MyDataManager>.Instance._SOData_Audio._Dict_Audio)
		{
			_Dict_Name_AudioC.Add(item.Key, item.Value._音效);
		}
	}

	private void F_Play_BGM(string audioName)
	{
		if (_Dict_Name_AudioC.TryGetValue(audioName, out var value))
		{
			_AudioS_BGM.clip = value;
			_AudioS_BGM.Play();
		}
		else
		{
			MyLog.LogError("没有BGM>>" + audioName);
		}
	}

	private void F_Play_SFX(string audioName)
	{
		if (_Dict_Name_AudioC.TryGetValue(audioName, out var value))
		{
			_AudioS_SFX.PlayOneShot(value);
		}
		else
		{
			MyLog.LogError("没有音效>>" + audioName);
		}
	}

	private void F_Play_SFX(string audioName, float 音量)
	{
		if (_Dict_Name_AudioC.TryGetValue(audioName, out var value))
		{
			_AudioS_SFX.PlayOneShot(value, 音量);
		}
		else
		{
			MyLog.LogError("没有音效>>" + audioName);
		}
	}

	private void F_Play_UISFX(string audioName)
	{
		if (_Dict_Name_AudioC.TryGetValue(audioName, out var value))
		{
			_AudioS_UISFX.PlayOneShot(value);
		}
		else
		{
			MyLog.LogError("没有音效>>" + audioName);
		}
	}

	public void F_Play_BGM_云宫迅音()
	{
		F_Play_BGM("云宫迅音_8bit版片段_2");
	}

	public void F_Play_BGM_云宫迅音_战斗()
	{
		F_Play_BGM("云宫迅音 战斗");
	}

	public void F_Play_BGM_黑神话_悟空()
	{
		F_Play_BGM("黑神话_悟空");
	}

	public void F_Play_BGM_广智BGM()
	{
		F_Play_BGM("广智小曲");
	}

	public void F_Play_BGM_陕北说书()
	{
		F_Play_BGM("陕北说书");
	}

	public void F_Play_BGM_好运来()
	{
		F_Play_BGM("好运来");
	}

	public void F_Play_BGM_主题曲()
	{
		F_Play_BGM("主题曲");
	}

	public void F_Play_SFX_虎先锋近战1()
	{
		F_Play_SFX("虎先锋近战1");
	}

	public void F_Play_SFX_虎先锋近战2()
	{
		F_Play_SFX("虎先锋近战2");
	}

	public void F_Play_SFX_虎先锋近战3()
	{
		F_Play_SFX("虎先锋近战3");
	}

	public void F_Play_SFX_虎先锋直拳()
	{
		F_Play_SFX("虎先锋直拳");
	}

	public void F_Play_SFX_呱呱跳砸()
	{
		F_Play_SFX("呱呱跳砸");
	}

	public void F_Play_SFX_呱呱电流()
	{
		F_Play_SFX("呱呱电流");
	}

	public void F_Play_SFX_呱呱释放电球()
	{
		F_Play_SFX("呱呱释放电球");
	}

	public void F_Ani_Play_呱呱跳()
	{
		F_Play_SFX("跳跃");
	}

	public void F_Play_SFX_大风车()
	{
		F_Play_SFX("广智大风车");
	}

	public void F_Play_SFX_砸地()
	{
		F_Play_SFX("砸地");
	}

	public void F_Play_SFX_Boss攻击()
	{
		F_Play_SFX("刀狼砍击");
	}

	public void F_Play_SFX_爆炸()
	{
		F_Play_SFX("爆炸");
	}

	public void F_Play_SFX_炸弹桶()
	{
		F_Play_SFX("炸弹桶");
	}

	public void F_Play_SFX_小妖攻击()
	{
		F_Play_SFX("刀狼砍击");
	}

	public void F_Play_SFX_小妖死亡()
	{
		F_Play_SFX("小妖死亡");
	}

	public void F_Play_SFX_小呱呱攻击()
	{
		F_Play_SFX("小呱呱攻击");
	}

	public void F_Play_SFX_小呱呱死亡()
	{
		F_Play_SFX("小呱呱死亡");
	}

	public void F_Play_SFX_撞击()
	{
		F_Play_SFX("撞击");
	}

	public void F_Play_SFX_撞击_音量(float 音量)
	{
		F_Play_SFX("撞击", 音量);
	}

	public void F_Play_SFX_弓狼射箭()
	{
		F_Play_SFX("弓狼射箭");
	}

	public void F_Play_SFX_虎先锋咆哮()
	{
		F_Play_SFX("虎先锋咆哮");
	}

	public void F_Play_SFX_弹反()
	{
		F_Play_SFX("弹反");
	}

	public void F_Play_SFX_拔刀()
	{
		F_Play_SFX("拔刀");
	}

	public void F_Play_SFX_挥砍()
	{
		float value = Random.value;
		if (value < 0.33f)
		{
			F_Play_SFX("挥砍_1");
		}
		else if (value < 0.66f)
		{
			F_Play_SFX("挥砍_2");
		}
		else
		{
			F_Play_SFX("挥砍_3");
		}
	}

	public void F_Play_SFX_波浪咆哮()
	{
		F_Play_SFX("波浪咆哮");
	}

	public void F_Play_SFX_我来助你()
	{
		F_Play_SFX("我来助你");
	}

	public void F_Play_SFX_地震()
	{
		F_Play_SFX("地震");
	}

	public AudioSource F_GO_Play_SFX_金池长老入场()
	{
		if (_Dict_Name_AudioC.TryGetValue("金池开场语音", out var value))
		{
			AudioSource audioSource = Object.Instantiate(_AudioS_SFX, base.transform);
			audioSource.clip = value;
			audioSource.Play();
			return audioSource;
		}
		MyLog.LogError("没有音效>>金池开场语音");
		return null;
	}

	public void F_Play_SFX_爆炸电流()
	{
		F_Play_SFX("爆炸电流");
	}

	public void F_Play_SFX_大落雷()
	{
		F_Play_SFX("大落雷");
	}

	public void F_Play_SFX_尾后针()
	{
		F_Play_SFX("尾后针");
	}

	public void F_Play_SFX_小电流()
	{
		F_Play_SFX("小电流");
	}

	public void F_Play_SFX_发射()
	{
		F_Play_SFX("发射");
	}

	public void F_Play_SFX_落雷()
	{
		F_Play_SFX("落雷");
	}

	public void F_Play_SFX_敲地()
	{
		F_Play_SFX("敲地");
	}

	public void F_Play_SFX_投掷()
	{
		F_Play_SFX("投掷");
	}

	public void F_Play_SFX_重砸()
	{
		F_Play_SFX("重砸");
	}

	public void F_Play_SFX_打铁_1()
	{
		F_Play_SFX("打铁_1");
	}

	public void F_Play_SFX_打铁_2()
	{
		F_Play_SFX("打铁_2");
	}

	public void F_Play_SFX_解冻音效0()
	{
		F_Play_SFX("解冻音效0");
	}

	public void F_Play_SFX_解冻音效1()
	{
		F_Play_SFX("解冻音效1");
	}

	public void F_Play_SFX_解冻音效2()
	{
		F_Play_SFX("解冻音效2");
	}

	public void F_Play_SFX_解冻音效3()
	{
		F_Play_SFX("解冻音效3");
	}

	public void F_Play_SFX_广智闪避()
	{
		F_Play_SFX("广智闪避");
	}

	public void F_Play_SFX_破隐一击()
	{
		F_Play_SFX("破隐一击");
	}

	public void F_Play_SFX_分身()
	{
		F_Play_SFX("分身");
	}

	public void F_Play_SFX_打碎罐子()
	{
		F_Play_SFX("罐子");
	}

	public void F_Play_SFX_反弹箭()
	{
		F_Play_SFX("反 弹箭");
	}

	public void F_Play_SFX_收集灵韵()
	{
		F_Play_SFX("收集灵韵");
	}

	public void F_Play_SFX_玩家_轻攻击()
	{
		F_Play_SFX("轻击");
	}

	public void F_Play_SFX_玩家_重攻击()
	{
		F_Play_SFX("重击");
	}

	public void F_Play_SFX_玩家_识破()
	{
		F_Play_SFX("识破");
	}

	public void F_Play_SFX_玩家_喝酒()
	{
		F_Play_SFX("喝药时");
	}

	public void F_Play_SFX_玩家_完美闪避()
	{
		F_Play_SFX("完美闪避");
	}

	public void F_Play_SFX_玩家_定身术()
	{
		F_Play_SFX("定身");
	}

	public void F_Play_SFX_玩家_死亡()
	{
		F_Play_SFX("死亡");
	}

	public void F_Play_SFX_玩家_名刀司命()
	{
		F_Play_SFX("名刀司命");
	}

	public void F_Play_SFX_玩家_跳跃_翻滚()
	{
		F_Play_SFX("跳跃");
	}

	public void F_Play_SFX_玩家_蓄力加豆子时()
	{
		F_Play_SFX("蓄力加豆子");
	}

	public void F_Play_SFX_玩家_力竭()
	{
		F_Play_SFX("错误");
	}

	public void F_Play_SFX_玩家_蓄力时()
	{
		F_Play_SFX("蓄力时");
	}

	public void F_Play_SFX_玩家_等级()
	{
		F_Play_SFX("升级");
	}

	public void F_Play_SFX_玩家_上香()
	{
		F_Play_UISFX("上香");
	}

	public void F_Play_SFX_玩家_敲钟()
	{
		F_Play_SFX("敲钟");
	}

	public void F_Play_SFX_玩家_精魄()
	{
	}

	public void F_Play_SFX_玩家_变身广智()
	{
		F_Play_SFX("变身广智");
	}

	public void F_Play_SFX_玩家_AK()
	{
		F_Play_SFX("AK");
	}

	public void F_Play_SFX_玩家_跳跃加速()
	{
		F_Play_SFX("跳跃加速");
	}

	public void F_Play_UISFX_点击()
	{
		F_Play_UISFX("点击");
	}

	public void F_Play_UISFX_三连()
	{
		F_Play_UISFX("三连");
	}

	public void F_Play_UISFX_使用筋斗云()
	{
		F_Play_UISFX("筋斗云");
	}

	public void F_Play_UISFX_修行_升级()
	{
		F_Play_UISFX("修行_升级");
	}

	public void F_Play_UISFX_修行_满级()
	{
		F_Play_UISFX("修行_满级");
	}
}
