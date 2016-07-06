using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseHeroOrMob
{
    public string myName;
    public int level;
    public float maxHP;
    public float curHP;
    public float maxMP;
    public float curMP;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int luck;
    //resistances
    public double resistNormal;
    public double resistFire;
    public double resistIce;
    public double resistElectric;
    public double resistWater;
    public double resistPoison;
    public double resistHoly;
    public double resistEarth;
    public double resistWind;
    public double resistDark;
    public double resistMetal;

    public List<BaseAttack> attacksList = new List<BaseAttack>();


}