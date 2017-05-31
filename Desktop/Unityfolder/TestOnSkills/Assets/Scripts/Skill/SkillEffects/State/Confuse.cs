using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confuse : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool succeedConfuse = isEffective (this.scaler * skillLevel);
		bool isTaunt = isEffective(this.scaler * skillLevel);

		if (succeedConfuse) {

			//原始物理伤害值
			int originalDamage = (int)(target.attack * target.hurtScaler * target.critScaler);

			//抵消护甲作用后的实际伤害值
			int actualDamage = (int)(originalDamage / (1 + seed * target.amour));

			//抵消的伤害值
			int DamageOffset = originalDamage - actualDamage;

			//目标触发被击中效果
			target.OnTrigger (TriggerType.BeHit, DamageOffset);

			target.health -= actualDamage;

		} else if(isTaunt){
			StateSkillEffect taunt = GameManager.gameManager.GenerateStates ("taunt")[0];
			self.AddState (taunt, skillLevel);
		}
	}


}
