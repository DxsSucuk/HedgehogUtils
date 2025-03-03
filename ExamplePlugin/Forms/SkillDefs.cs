using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using RoR2;
using RoR2.Skills;

namespace HedgehogUtils.Forms
{
    public class SkillDefs
    {
        public class RequiresFormSkillDef : SkillDef
        {
            public FormDef requiredForm;

            private GenericSkill skillSlot;
            private object source;
            private GenericSkill.SkillOverridePriority priority;
            
            public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
            {
                InstanceData instanceData = new RequiresFormSkillDef.InstanceData
                {
                    formComponent = skillSlot.GetComponent<FormComponent>()
                };

                this.skillSlot = skillSlot;
                this.source = skillSlot.skillOverrides[skillSlot.currentSkillOverride].source;
                this.priority = skillSlot.skillOverrides[skillSlot.currentSkillOverride].priority;

                if (instanceData.formComponent.activeForm != requiredForm)
                {
                    skillSlot.UnsetSkillOverride(source, this, priority);
                }
                else
                {
                    instanceData.formComponent.OnFormChanged += OnFormChanged;
                }

                return instanceData;
            }

            public override void OnUnassigned([NotNull] GenericSkill skillSlot)
            {
                if (skillSlot.skillInstanceData != null && ((InstanceData)skillSlot.skillInstanceData).formComponent)
                {
                    ((InstanceData)skillSlot.skillInstanceData).formComponent.OnFormChanged -= OnFormChanged;
                }
            }

            public void OnFormChanged(FormDef previous, FormDef newForm)
            {
                if (newForm != requiredForm)
                {
                    skillSlot.UnsetSkillOverride(source, this, priority);
                }
            }

            protected class InstanceData : SkillDef.BaseSkillInstanceData
            {
                public FormComponent formComponent;
            }
        }
    }
}
