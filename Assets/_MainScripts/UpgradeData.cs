using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [Header("업그레이드 기본 정보")]
    public string upgradeName;
    [TextArea]
    public string description;

    [Header("레벨 정보")]
    public int currentLevel;
    public int maxLevel;

    [Header("업그레이드 비용")]
    public int[] upgradeCosts; // 각 레벨별 비용

    [Header("업그레이드 효과")]
    public float[] upgradeValues; // 각 레벨별 효과 값

    // 현재 레벨의 효과 반환
    public float GetCurrentValue()
    {
        if (currentLevel < upgradeValues.Length)
            return upgradeValues[currentLevel];
        return upgradeValues[upgradeValues.Length - 1];
    }

    // 다음 레벨 업그레이드 비용 반환
    public int GetNextUpgradeCost()
    {
        if (currentLevel < upgradeCosts.Length)
            return upgradeCosts[currentLevel];
        return -1; // 최대 레벨 도달 시
    }
}