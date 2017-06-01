using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sacrifice : StateSkillEffect {


	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int arg)
	{
		bool succeedSacrifice = isEffective (this.scaler * skillLevel);

		if (succeedSacrifice) {
			foreach (Button btn in GameManager.gameManager.battleManager.GetComponent<BattleManager>().skillButtons) {
				btn.interactable = true;
			}
			foreach (Skill s in self.skills) {
				s.isAvalible = true;
			}
		}


	}

}
