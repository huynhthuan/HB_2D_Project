using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField]
    Text hpText;

    // Start is called before the first frame update
    void Start() { }

    public void OnInit(float dame)
    {
        hpText.text = dame.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() { }
}
