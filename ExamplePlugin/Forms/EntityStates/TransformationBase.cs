using EntityStates;
using RoR2;
using RoR2.Audio;
using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace HedgehogUtils.Forms.EntityStates
{
    public class TransformationBase : BaseSkillState
    {
        public FormDef form;

        protected SuperSonicComponent superSonic;

        public bool fromTeamSuper = false;

        public override void OnEnter()
        {
            base.OnEnter();
            this.superSonic= base.GetComponent<SuperSonicComponent>();
            if (base.isAuthority)
            {
                this.form = superSonic.targetedForm;
            }
        }

        public virtual void Transform()
        {
            if (base.isAuthority)
            {
                this.superSonic.SetNextForm(this.form);
            }
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(fromTeamSuper);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            fromTeamSuper = reader.ReadBoolean();
        }
    }
}