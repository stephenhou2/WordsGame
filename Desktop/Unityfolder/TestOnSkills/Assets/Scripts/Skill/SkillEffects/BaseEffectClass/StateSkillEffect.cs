using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateSkillEffect : BaseSkillEffect {



	// 状态管理方法，提供状态类效果的等级信息和作用的单位信息
	public void ManageState (BattleAgent self, BattleAgent target, int skillLevel,TriggerType triggerType,int attachedInfo)
	{
		this.skillLevel = skillLevel;//效果等级等于技能等级

		// 根据技能指向将状态加到指定的对象身上
		switch (effectTarget) {
		case SkillEffectTarget.Self:
			AddStateTo (self,this);
			break;
		case SkillEffectTarget.Enemy:
			AddStateTo (target,this);
			break;
		case SkillEffectTarget.Both:
			AddStateTo (self,this);
			AddStateTo (target,this);
			break;
		default:
			break;
		}
	}

	//管理单位上每种状态的生命周期，添加和删除状态时会重新计算单位的各种属性
	public virtual void AddStateTo(BattleAgent ba,StateSkillEffect sse){
		actionCount++;
		Debug.Log ("action count" + actionCount + this.effectName);

		StateSkillEffect effect = null;

		if (actionCount > effectDuration) {
			actionCount = 0;
			//到达持续回合数移除状态(按照技能效果名称查询)
			ba.RemoveState (effect);
			Debug.Log (ba.agentName + "remove" + this.name);
			if (effect != null) {
				Destroy (effect);
			}
			return;
		}


		//添加状态
		if(!canOverlay && actionCount != 1) {
			return;
		} 
		effect = Instantiate (sse);
		ba.AddState (effect,effect.skillLevel);
		Debug.Log (ba.agentName + "add" + effect.name);




	}

}
