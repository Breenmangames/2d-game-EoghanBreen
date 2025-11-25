using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject DamageTextPrefab;
    public GameObject HealthTextPrefab;
    public Canvas gameCanvas;


    public void CharacterTakenHit(GameObject Player, float damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(Player.transform.position);

        TMP_Text tmpText = Instantiate(DamageTextPrefab,spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject Player, float damageHealed) { }

}
