using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateSkillEffect : BaseSkillEffect {



	// 状态管理方法，提供状态类效果的等级信息和作用的单位信息
	public void ManageState (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered,TriggerType triggerType,int attachedInfo)
	{
		this.skillLevel = skillLevel;
		switch (effectTarget) {
		case SkillEffectTarget.Self:
			AddStateTo (self);
			break;
		case SkillEffectTarget.Enemy:
			AddStateTo (target);
			break;
		default:
			break;
		}
	}

	//管理单位上每种状态的生命周期，添加和删除状态时会重新计算单位的各种属性
	public virtual void AddStateTo(BattleAgent ba){
		actionCount++;
		if (actionCount > effectDuration) {
			actionCount = 0;
			//到达持续回合数移除状态
			ba.RemoveState (this);
//			Destroy (this);
			return;
		}
		//添加状态
		if(!canOverlay && actionCount != 1) {
			return;
		} 
		ba.AddState (this, this.skillLevel);
	}

}
