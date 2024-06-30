using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG{
public class Requirement:Displayable
{
    public enum Type{
        Enemy,Item
    }
    public Displayable requireItem{set;get;}
    public EnemyTemplate requireEnemy{set;get;}
    public int requireQty{set;get;}
    public int currentQty{set;get;}
    public Type type{set;get;}
    public Requirement(Displayable requireItem,int requireQty,Type type):base(requireItem.img){
        this.requireItem = requireItem;
        this.requireQty = requireQty;
        this.type = type;
        this.currentQty = 0;
    }

    public Requirement(EnemyTemplate enemyTemplate,int requireQty):base(null){
        this.requireEnemy = enemyTemplate;
        this.requireQty = requireQty;
        this.type = Type.Enemy;
        this.currentQty = 0;
    }
}

}
