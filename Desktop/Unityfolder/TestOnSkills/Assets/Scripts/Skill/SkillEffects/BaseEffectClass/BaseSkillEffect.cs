﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseSkillEffect:MonoBehaviour {

	//*************需要从本地读取的技能效果数据属性***************//
	public string effectName;//魔法效果名称

	public string description;//魔法效果描述

	public int id;//魔法效果id

	public int strengthConsume;//消耗的气力数

	public int effectDuration;//效果持续回合数

	public int actionConsume;//效果消耗行动数

	public SkillEffectTarget effectTarget;//效果目标

	public float scaler;//影响系数

	public bool isStateEffect;//是否是状态类效果

	public bool canOverlay;//是否可以叠加

	public StartTurn startTurn;//效果开始轮

	public EffectType effectType;//物理攻击、魔法攻击、混乱攻击
	//*************需要从本地读取的技能效果数据属性***************//

	public float seed = 0.01f;//计算伤害和闪避时的种子数

	public int actionCount = 0;//该效果已持续的回合数

	public int skillLevel;//效果等级

	 
	//技能实际效果
	public abstract void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isTriggered,
		TriggerType triggerType,
		int attachedInfo);


	// 判断概率性技能是否生效
	protected bool isEffective(float chance){
		float randomNum = Random.Range (0, 100)/100f;
		return randomNum <= chance;
	}

}
