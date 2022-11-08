using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG
{
    public class Displayable
    {
        public Sprite img { get; set; }
        public Displayable(Sprite img)
        {
            this.img = img;
        }
    }
}