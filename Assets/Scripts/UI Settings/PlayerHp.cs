using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{

    public float hpmax=100, mpmax=100, xpmax=10;

    public float hpvalue, mpvalue, xpvalue;
    public Image hpbar, mpbar, xpbar;

    public GameObject winpanel,loserpanel;
    // Start is called before the first frame update
    void Start()
    {
        hpvalue = hpmax;
        mpvalue = mpmax;
        xpvalue = xpmax;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpvalue > 0)
        {
            hpbar.fillAmount = hpvalue / hpmax;
            mpbar.fillAmount = mpvalue / mpmax;
            xpbar.fillAmount = xpvalue / xpmax;
        }
        else
        {
            hpbar.fillAmount = 0;
            StartCoroutine(PlayerDie());
        }
    }

    public void Addhp(float value)
    {
        if (hpvalue >= 0 && hpvalue <= hpmax)
        {
            hpvalue += value;
        }
        else if (hpvalue < 0)
        {
            hpvalue = 0;
        }
        else if (hpvalue > hpmax)
        {
            hpvalue = hpmax;
        }
        
    }
    public void Addmp(float value)
    {
        if (mpvalue >= 0 && mpvalue <= mpmax)
        {
            mpvalue += value;
        }
        else if (mpvalue < 0)
        {
            mpvalue = 0;
        }
        else if (mpvalue > mpmax)
        {
            mpvalue = mpmax;
        }
      
    }
    public void Addxp(float value)
    {
        if (xpvalue >= 0 && xpvalue <= xpmax)
        {
            xpvalue += value;
        }else if (xpvalue < 0)
        {
            xpvalue = 0;
        }
        else if (xpvalue > xpmax)
        {
            xpvalue = xpmax;
        }
    }

    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(1.5f);
        loserpanel.SetActive(true);
        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<AttackScript>().enabled = false;
        this.enabled = false;
    }

    public void PlayerWin()
    {
        winpanel.SetActive(true);
        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<AttackScript>().enabled = false;
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
          if (other.gameObject.CompareTag("coin"))
        {
            Addhp(500);
            Destroy(other.gameObject);
        }
    }
}
