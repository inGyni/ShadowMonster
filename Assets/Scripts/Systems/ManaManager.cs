using UnityEngine;
using UnityEngine.UI;

public class ManaManager: MonoBehaviour
{
    public PlayerController pc;
    private int maxMana;
    private int currentMana;
    [HideInInspector]
    public bool isUsing;
    private bool isRunning;
    public Slider slider;
    public Text ManaText;
    float timer;

    public void Start()
    {
        slider = GetComponent<Slider>();
        ManaText = GetComponentInChildren<Text>();
    }

    public void Update()
    {
        timer += Time.deltaTime * 10;
        if (Mathf.FloorToInt(timer) > 0)
        {
            if (isUsing && currentMana > 0)
            {
                currentMana -= 1;
            }
            else if(!isUsing && currentMana < 100)
            {
                currentMana += 1;
            }

            if(currentMana == 0)
            {
                StopCoroutine(pc.InvisibleEffectOn());
                StartCoroutine(pc.InvisibleEffectOff());
                pc.Invisible = false;
            }

            UpdateBar();
            timer = 0;
        }
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }

    public void SetMaxMana(int maxMana)
    {
        this.maxMana = maxMana;
        this.currentMana = maxMana;
        InitializeBar(maxMana);
    }

    public void SetCurrentMana(int Mana)
    {
        if (Mana > maxMana)
            Mana = maxMana;
        this.currentMana = Mana;
        UpdateBar();
    }

    public void Damage(int damage)
    {
        if (damage > currentMana)
            damage = currentMana;
        if (damage < 0)
            return;
        currentMana -= damage;
        UpdateBar();
    }

    public void Heal(int heal)
    {
        if (heal > maxMana)
            currentMana = maxMana;
        if (heal < 1)
            return;
        currentMana += heal;
        UpdateBar();
    }

    public void HealMax()
    {
        currentMana = maxMana;
        UpdateBar();
    }
    private void InitializeBar(int maxMana)
    {
        slider.maxValue = maxMana;
        slider.value = maxMana;
        ManaText.text = slider.value.ToString() + "%";
    }

    public void UpdateBar()
    {
        slider.value = currentMana;
        ManaText.text = currentMana.ToString() + "%";
    }
}
