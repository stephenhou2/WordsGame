using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGenerator : MonoBehaviour {

//	public static SkillGenerator sharedSkillGenerator;

	public Skill skill;

	public EffectGenerator effectGenerator;

	public Transform effectsContainer;


	public void SetupSkillGenerator(EffectData[] skillEffectsData){
		
		Transform effects = Instantiate (effectsContainer);
		effects.name = "Effects";

		//所有的技能效果数据转为技能效果对象
		effectGenerator.GenerateEffectObjects (skillEffectsData,false,null,effects);

		//初始化创造技能界面
		SetupSkillGeneratorScene ();

	}

	public StateSkillEffect[] GenerateStateSkillEffects(EffectData[] skillEffectsData,string effectName){

		Transform effects = Instantiate (effectsContainer);
		effects.name = "States";

		effectGenerator.GenerateEffectObjects (skillEffectsData, true, effectName,effects);
		StateSkillEffect[] sseArray = null;
		effectGenerator.skillEffectsList.CopyTo (sseArray);
		return sseArray;
	}

	// 根据所选技能效果创造技能
	public void GenerateSkillWithIds(int id_1,int id_2,BattleAgent ba){

		Skill mySkill = Instantiate(skill,ba.skillsContainer);

		BaseSkillEffect skillEffect_1 = Instantiate (effectGenerator.skillEffectsList [id_1],mySkill.transform);
		skillEffect_1.name = "Effect_1";

		BaseSkillEffect skillEffect_2 = Instantiate (effectGenerator.skillEffectsList [id_2],mySkill.transform);
		skillEffect_2.name = "Effect_2";

		mySkill.skillEffects = new BaseSkillEffect[] {
			skillEffect_1,
			skillEffect_2
		};

		mySkill.strengthConsume = skillEffect_1.strengthConsume + skillEffect_2.strengthConsume;
		mySkill.actionConsume = skillEffect_1.actionConsume + skillEffect_2.actionConsume;

		ba.skills.Add (mySkill);

	}



	// 初始化创造技能界面
	private void SetupSkillGeneratorScene(){

		Debug.Log ("进入了技能生成场景");
	}

	public void QuitSkillGenerate(){
		GameObject go = GameObject.Find ("Effects");
		Destroy (go);
	}


}
