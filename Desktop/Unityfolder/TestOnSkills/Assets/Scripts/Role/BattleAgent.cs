using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BattleAgent : MonoBehaviour {

	public string agentName;

	public BattleAgent enemy;

	//*****玩家初始信息********//
	public int originalMaxHealth;
	public int originalMaxStrength;
	public int originalHealth;
	public int originalStrength;
	public int originalAttack;
	public int originalPower;
	public int originalMagic;
	public int originalCrit;
	public int originalAgility;
	public int originalAmour;
	public int originalMagicResist;
	//*****玩家初始信息********//


	public int maxHealth;//最大血量
	public int maxStrength;//最大气力值

	public int health;//实际血量
	public int strength;//实际气力值

	public int attack;//攻击力
	public int power;//力量
	public int magic;//魔法
	public int agility;//敏捷
	public int amour;//护甲
	public int magicResist;//魔抗
	public int crit;//暴击


	public int healthGainScaler = 1;//力量对最大血量的加成系数

	public float strengthGainScaler = 0.05f; //力量对最大气力的加成系数

	public ValidActionType validActionType;// 有效的行动类型

	public EffectType effectType;//

	public float hurtScaler = 1.0f;//伤害系数

	public float critScaler = 1.0f;//暴击伤害系数

	public float healthAbsorbScalser = 0f;//回血比例

	public List<Skill> skills = new List<Skill>();//技能数组

	public List<Item> items = new List<Item>();//物品数组

	public List<StateSkillEffect> states = new List<StateSkillEffect>();//状态数组

	public int attackTime = 1;//攻击次数

	public Transform statesContainerModel;//状态容器,用于使层级视图更加整洁

	public Transform skillsContainerModel;

	[HideInInspector]public Transform skillsContainer;

	[HideInInspector]public Transform statesContainer;

	public virtual void Awake(){
		statesContainer = Instantiate (statesContainerModel, this.transform);
		statesContainer.name = "States";
		skillsContainer = Instantiate (skillsContainerModel, this.transform);
		skillsContainer.name = "Skills";
	}

	//添加状态 
	public void AddState(StateSkillEffect sse,int skillLevel){
		states.Add (sse);
		ReCaculateProperty (false);
	}
	//删除状态
	public void RemoveState(StateSkillEffect sse){
		for(int i = 0;i<states.Count;i++){
			if (sse.effectName == states[i].effectName) {
//				states.Remove (sse);
				states.RemoveAt(i);
				Destroy (sse);
				ReCaculateProperty (false);
				return;
			}
		}
	}

	//根据状态重新计算各属性值
	public void ReCaculateProperty(bool toOriginalState){
		ResetBattleAgentProperties (toOriginalState);
		foreach (StateSkillEffect sse in states) {
			if (sse.startTurn == StartTurn.Next) {
				sse.startTurn = StartTurn.Current;
				return;
			}
			sse.AffectAgent (this, this.enemy, sse.skillLevel, TriggerType.None, 0);
		}
	}
	// 状态效果触发执行的方法
	public void OnTrigger(TriggerType triggerType,int arg){
		foreach(StateSkillEffect sse in states){
			if (sse.triggerType == TriggerType.None) {
				return;
			}
			sse.AffectAgent (this, this.enemy, sse.skillLevel, triggerType, arg);
		}

	}

	// 仅根据物品重新计人物的属性，其余属性重置为初始状态
	public void ResetBattleAgentProperties (bool toOriginalState)
	{

		if (items.Count != 0) {

			foreach (Item i in items) {

				attack = originalAttack + i.attackGain;
				power = originalPower + i.powerGain;
				magic = originalMagic + i.magicGain;
				crit = originalCrit + i.critGain;
				amour = originalAmour + i.amourGain;
				magicResist = originalMagicResist + i.magicResistGain;
				agility = originalAgility + i.agilityGain;

			}
		} else {

			attack = originalAttack;
			power = originalPower;
			magic = originalMagic;
			crit = originalCrit;
			amour = originalAmour;
			magicResist = originalMagicResist;
			agility = originalAgility;
		}


		maxHealth = originalMaxHealth + healthGainScaler * power;
		maxStrength = originalMaxStrength + (int)(strengthGainScaler * power);
		validActionType = ValidActionType.All;
		hurtScaler = 1.0f;//伤害系数
		critScaler = 1.0f;//暴击伤害系数
		healthAbsorbScalser = 0f;//回血比例
		attackTime = 1;


		if (toOriginalState) {
			health = maxHealth;
			strength = maxStrength;
		}
	}

	public override string ToString ()
	{
		return string.Format ("[player]" + 
			"\n[attack]:" + attack + 
			"\n[power]:" + power + 
			"\n[magic]:" + magic +
			"\n[crit]:" + crit +
			"\n[amour]:" + amour +
			"\n[magicResist]:" + magicResist +
			"\n[agiglity]:" + agility +
			"\n[maxHealth]:" + maxHealth +
			"\n[maxStrength]:" + maxStrength);
	}
}
