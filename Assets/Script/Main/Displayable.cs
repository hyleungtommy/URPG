using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG
{
    /// <summary>
    /// An abstract class that define an item that is able to display on UI as an image
    /// </summary>
    public class Displayable
    {
        public Sprite img { get; set; }
        public Displayable(Sprite img)
        {
            this.img = img;
        }
    }
}