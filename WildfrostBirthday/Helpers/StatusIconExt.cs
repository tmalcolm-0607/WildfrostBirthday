using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace WildfrostBirthday.Helpers
{
    public class ButtonExt : Button
    {
        internal StatusIconExt? Icon => GetComponent<StatusIconExt>();

        internal static ButtonExt? dragBlocker = null;

        internal Entity? Entity => Icon?.target;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            dragBlocker = this;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            DisableDragBlocking();
        }

        public void DisableDragBlocking()
        {
            if (dragBlocker == this)
            {
                dragBlocker = null;
            }
        }

        public static void DisableDrag(ref Entity arg0, ref bool arg1)
        {
            if (dragBlocker == null || arg0 != dragBlocker.Entity)
            {
                return;
            }
            arg1 = false;
        }
    }

    public interface IStatusToken
    {
        void ButtonCreate(StatusIconExt icon);
        void RunButtonClicked();
        IEnumerator ButtonClicked();
    }

    public class StatusIconExt : StatusIcon
    {
        public ButtonAnimator? animator;
        public ButtonExt? button;
        private IStatusToken? effectToken;

        public override void Assign(Entity entity)
        {
            base.Assign(entity);
            SetText();
            onValueDown.AddListener(delegate { Ping(); });
            onValueUp.AddListener(delegate { Ping(); });
            afterUpdate.AddListener(SetText);
            onValueDown.AddListener(CheckDestroy);

            StatusEffectData effect = entity.FindStatus(type);
            if (effect is IStatusToken effect2)
            {
                effectToken = effect2;
                effect2.ButtonCreate(this);
                button?.onClick.AddListener(effectToken.RunButtonClicked);
                onDestroy.AddListener(DisableDragBlocker);
            }
        }

        public void DisableDragBlocker()
        {
            button?.DisableDragBlocking();
        }
    }
}
