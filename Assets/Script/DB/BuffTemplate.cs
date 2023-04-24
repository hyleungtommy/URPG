using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class BuffTemplate
    {
        public int id;
        public string name;
        public string[] effects;
        public string type;
        public string img;
        public int[] replace;
        public bool stackable;

        public override string ToString()
        {
            return "id=" + id + " name=" + name + " effects" + effects + " type=" + type + " img=" + img + " replace=" + replace + " stackable=" + stackable;
        }

    }
}