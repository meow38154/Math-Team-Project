using System;
using UnityEngine;
using WST.EventBus;
using WST.Events.ItemSkillEvent;

namespace WST.Scripts.Item.ItemSkills
{
    public  class SodaShootSkill : MonoBehaviour
    {
        private void Awake()
        {
            Bus<SodaShootEvent>.OnEvent += HandleSodaShoot;
        }
        
        private void OnDestroy()
        {
            Bus<SodaShootEvent>.OnEvent -= HandleSodaShoot;
        }

        private void HandleSodaShoot(SodaShootEvent evt)
        {
            //스킬 구현
        }
    }
}