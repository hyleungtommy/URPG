using System;
namespace RPG{
    [Serializable]
    public class ElementalTemplate{
        public int fire;
        public int ice;
        public int lighting;
        public int wind;
        public int earth;
        public int light;
        public int dark;

        public ElementalTemplate plus(ElementalTemplate other){
            ElementalTemplate newTemplate = new ElementalTemplate();
            newTemplate.fire = fire + other.fire;
            newTemplate.ice = ice + other.ice;
            newTemplate.lighting = lighting + other.lighting;
            newTemplate.wind = wind + other.wind;
            newTemplate.earth = earth + other.earth;
            newTemplate.light = light + other.light;
            newTemplate.dark = dark + other.dark;
            return newTemplate;
        }

        public override string ToString()
        {
            return string.Format("[ElementalTemplate: fire={0}, ice={1}, lighting={2}, wind={3}, earth={4}, light={5}, dark={6}]"
            , fire, ice, lighting, wind, earth, light, dark);
        }

    }
}