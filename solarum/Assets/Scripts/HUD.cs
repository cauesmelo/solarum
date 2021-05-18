using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image BarFill;
    public Image Meteor;
    private float cooldownTime;
    PlayerMovement player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        cooldownTime = player.GetDashCooldown();
        this.Meteor.fillAmount -= 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.CooldownBar(player.GetReadyToDash());
        this.UpdateMeteor(GameManager.instance.GetStar());
    }

    private void CooldownBar(bool dash)
    {

        if (!dash)
        {
            if(this.BarFill.fillAmount >= 1.0f)
            {
                this.BarFill.fillAmount -= 1.0f;
            }
            
            this.BarFill.fillAmount += 1.0f / cooldownTime * Time.deltaTime;
            
        }
        else
        {
            this.BarFill.fillAmount += 1.0f;
        }
    }

    private void UpdateMeteor(int stars)
    {
        this.Meteor.fillAmount = (1.0f / GameManager.instance.GetStarsRequired()) * stars;
    }


}
