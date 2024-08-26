namespace RPG{
    public class EnchantmentEffect{
        public int id{get;set;}
        public string name{get;set;}
        public string desc{get;set;}
        public int lv{get;set;}
        public float modifier{get;set;}
        public bool[] equipTypeWhiteList {get; set;}
        public EnchantmentEffect(){
            
        }

        public ElementalTemplate GetElementalMatrix(){
            ElementalTemplate elemental = new ElementalTemplate();
            if(id == 9 || id == 16) elemental.fire = (int)(modifier * 100);
            else if(id == 10 || id == 17) elemental.ice = (int)(modifier * 100);
            else if(id == 11 || id == 18) elemental.lighting = (int)(modifier * 100);
            else if(id == 12 || id == 19) elemental.earth = (int)(modifier * 100);
            else if(id == 13 || id == 20) elemental.wind = (int)(modifier * 100);
            else if(id == 14 || id == 21) elemental.light = (int)(modifier * 100);
            else if(id == 15 || id == 22) elemental.dark = (int)(modifier * 100);
            return elemental;
        }

        public string onSave(){
            return id + "'" + lv;
        }
        
    }
}