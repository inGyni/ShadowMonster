using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager: MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    public Slider slider;
    public Text healthText; 

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        InitializeBar(maxHealth);
    }

    public void SetCurrentHealth(int health)
    {
        if (health > maxHealth)
            health = maxHealth;
        this.currentHealth = health;
        UpdateBar(this.currentHealth);
    }

    public void Damage(int damage)
    {
        if (damage > currentHealth)
            damage = currentHealth;
        if (damage < 0)
            return;
        currentHealth -= damage;
        UpdateBar(this.currentHealth);
    }

    public void Heal(int heal)
    {
        if (heal > maxHealth)
            currentHealth = maxHealth;
        if (heal < 1)
            return;
        currentHealth += heal;
        UpdateBar(this.currentHealth);
    }

    public void HealMax()
    {
        currentHealth = maxHealth;
        UpdateBar(this.currentHealth);
    }
    private void InitializeBar(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        healthText.text = slider.value.ToString() + "%";
    }

    public void UpdateBar(int newHealth)
    {
        slider.value = newHealth;
        healthText.text = newHealth.ToString() + "%";
        if (newHealth < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
