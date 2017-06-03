using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confuse : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		bool succeedConfuse = isEffective (this.scaler * skillLevel);
		bool isTaunt = isEffective(this.scaler * skillLevel);

		if (succeedConfuse) {
			
			bool isCrit = isEffective (seed * target.crit / (1 + seed * target.crit));

			if (isCrit) {
				target.critScaler = 2.0f;
			}

			//原始物理伤害值
			int originalDamage = (int)(target.attack * target.hurtScaler * target.critScaler);

			//抵消护甲作用后的实际伤害值
			int actualDamage = (int)(originalDamage / (1 + seed * target.amour));

			//抵消的伤害值
			int DamageOffset = originalDamage - actualDamage;

			//目标触发被击中效果
			target.OnTrigger (TriggerType.BePhysicalHit, DamageOffset);

			target.health -= actualDamage;

			if (target.health < 0) {
				target.health = 0;
			}

			target.critScaler = 1.0f;

		} else if(isTaunt){
			StateSkillEffect taunt = GameManager.gameManager.GenerateStates ("taunt")[0];
			taunt.effectTarget = SkillEffectTarget.Self;
			BattleAgentStatesManager.AddStateCopyToEffectTarget (self, target, taunt, skillLevel);
		}
	}


}
