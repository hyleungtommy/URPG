using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RPG;
public class FloatingNumberCtrl : MonoBehaviour
{

    public Sprite miss, critical;
    private static float timer = 1.0f;
    private float localTimer = 0.0f;
    private int damageInt = -1;

    void Start()
    {

    }

    void receiveDamageMessage(int d)
    {
        if (damageInt == -1)
            damageInt = d;
        //Debug.Log ("damage int:" + damageInt);
    }


    void setType(BattleMessage message)
    {
        if ((message.type == BattleMessage.Type.NormalAttack || message.type == BattleMessage.Type.Critical) && message.sender is EntityPlayer)
            GetComponent<Image>().sprite = SpriteManager.floatingTextEnemyDamage[damageInt];
        else if ((message.type == BattleMessage.Type.NormalAttack || message.type == BattleMessage.Type.Critical) && message.sender is EntityEnemy)
            GetComponent<Image>().sprite = SpriteManager.floatingTextPlayerDamage[damageInt];
        else if (message.type == BattleMessage.Type.MPAttack)
            GetComponent<Image>().sprite = SpriteManager.floatingTextMpDamage[damageInt];
        else if (message.type == BattleMessage.Type.Heal)
            GetComponent<Image>().sprite = SpriteManager.floatingTextHeal[damageInt];
        else if (message.type == BattleMessage.Type.MPHeal)
            GetComponent<Image>().sprite = SpriteManager.floatingTextMpHeal[damageInt];
        else if (message.type == BattleMessage.Type.Miss)
            GetComponent<Image>().sprite = miss;
    }


    void createCritical()
    {
        GetComponent<Image>().sprite = critical;
    }


    void Update()
    {

        if (localTimer <= timer)
        {
            transform.Translate(new Vector3(0, 0.8f * Time.deltaTime, 0));
            localTimer += Time.deltaTime;
        }
        else
            Destroy(gameObject);

    }
}