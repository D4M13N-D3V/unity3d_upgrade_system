using Damien.UpgradeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damien.UpgradeSystem.Example
{
    public class DebugMenu : MonoBehaviour
    {
        public UpgradeManager UpgradeSystem;
        public Transform Content;
        public GameObject Panel;

        public TMPro.TMP_InputField UpgradeGiveInput;
        public TMPro.TMP_InputField UpgradeRemoveInput;
        public TMPro.TMP_InputField BuffInput1;
        public TMPro.TMP_InputField DebuffInput1;
        public TMPro.TMP_InputField BuffInput2;
        public TMPro.TMP_InputField DebuffInput2;
        public TMPro.TMP_InputField BuffInput3;
        public TMPro.TMP_InputField DebuffInput3;

        public int YOffset = 0;
        public int XOffset = 0;
        public int YSpacing = 50;

        private List<GameObject> Upgradepanels = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Loop());
        }

        public void Give()
        {
            UpgradeSystem.GiveUpgrade(UpgradeGiveInput.text);
        }
        public void Remove()
        {
            UpgradeSystem.RemoveUpgrade(UpgradeRemoveInput.text);
        }
        public void Buff()
        {
            if (int.TryParse(BuffInput2.text, out var result) && int.TryParse(BuffInput3.text, out var result2))
            {
                UpgradeSystem.Buff(BuffInput1.text, result, result2);
            }
        }
        public void Debuff()
        {
            if (int.TryParse(DebuffInput2.text, out var result) && int.TryParse(DebuffInput3.text, out var result2))
            {
                UpgradeSystem.Debuff(DebuffInput1.text, result, result2);
            }
        }

        IEnumerator Loop()
        {
            yield return new WaitForSeconds(5);
            foreach (var obj in Upgradepanels)
            {
                Destroy(obj);
            }

            var upgrades = UpgradeSystem.GetCurrentUpgrades();
            var stats = UpgradeSystem.GetStatistics();
            var current = 0;
            foreach (var upgrade in upgrades)
            {
                var obj = Instantiate(Panel, Content);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(XOffset, YOffset + current * YSpacing, 0);
                Upgradepanels.Add(obj);
                current++;
                obj.GetComponent<DebugMenuItem>().Text.text = $"{upgrade.Name}";
                obj.GetComponent<DebugMenuItem>().Image.sprite = upgrade.Icon;
            }
            foreach (var stat in stats)
            {
                var obj = Instantiate(Panel, Content);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(XOffset, YOffset + current * YSpacing, 0);
                Upgradepanels.Add(obj);
                current++;
                obj.GetComponent<DebugMenuItem>().Text.text = $"{stat.Key.Name} {stat.Value}";
                obj.GetComponent<DebugMenuItem>().Image.sprite = stat.Key.Icon;
            }
            StartCoroutine(Loop());
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
