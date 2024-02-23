using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 

    public Slider healthSlider; 

    void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthUI(); 
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        UpdateHealthUI(); 
    }

    
    void UpdateHealthUI()
    {
        healthSlider.value = currentHealth; 
    }
}
