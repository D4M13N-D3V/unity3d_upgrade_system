using Damien.UpgradeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public UpgradeSystem UpgradeSystem;
    public Transform Content;
    public GameObject Panel;

    public int YOffset = 0;
    public int XOffset = 0;
    public int YSpacing = 50;

    private List<GameObject> Upgradepanels = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loop());
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
            obj.GetComponent<StatSystemTestMenuITem>().Text.text = $"{upgrade.Name}";
            obj.GetComponent<StatSystemTestMenuITem>().Image.sprite = upgrade.Icon;
        }
        foreach (var stat in stats)
        {
            var obj = Instantiate(Panel, Content);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(XOffset, YOffset + current * YSpacing, 0);
            Upgradepanels.Add(obj);
            current++;
            obj.GetComponent<StatSystemTestMenuITem>().Text.text = $"{stat.Key.Name} {stat.Value}";
            obj.GetComponent<StatSystemTestMenuITem>().Image.sprite = stat.Key.Icon;
        }
        StartCoroutine(Loop());
    }
    // Update is called once per frame
    void Update()
    {

    }
}
