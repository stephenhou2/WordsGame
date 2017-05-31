using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtReflect : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.BeHit || triggerType == TriggerType.Debuff) {
			
			float reflectScaler = this.scaler * skillLevel;

			float dodge = seed * target.agility / (1 + seed * target.agility);

			//判断对方是否闪避成功
			if (isEffective (dodge)) {
				Debug.Log ("enemy dodge your attack");
				//目标触发闪避成功效果
				target.OnTrigger (TriggerType.Dodge, 0);
				return;
			}
			//护甲和魔抗所抵消的伤害值 
			target.health -= (int)(attachedInfo * reflectScaler);

		}


	}
}
