using System;
using TMPro;
using UnityEngine;

public class DiffrenceWindowElement : MonoBehaviour
{
    [SerializeField] protected TMP_Text text = null;

    public void Init(StateCaseData data, DateTime day, StateCaseData dataDiff = null)
    {
        bool showDiff = dataDiff != null;
        var updated = showDiff ? dataDiff.updated : data.updated;
        text.text = $"New cases per day : {data.PerDay}{(showDiff ? HighlightDiffecence(data.PerDay, dataDiff.PerDay) : "")}\nTotal count:{data.Total}{(showDiff ? HighlightDiffecence(data.Total, dataDiff.Total) : "")}\nDate {day:dd/MM/yyyy}\nUpdate {updated:dd/MM/yyyy}";
    }

    private string HighlightDiffecence(int a, int b)
    {
        return a != b ? $"<color=\"red\">{(a > b ? "+" : "")}{a - b}</color>" : "";
    }

}