namespace RPG{
    public class EnchantmentEffect{
        public int id{get;set;}
        public string name{get;set;}
        public string desc{get;set;}
        public int lv{get;set;}
        public float modifier{get;set;}
        public EnchantmentEffect(){
            
        }

        public string onSave(){
            return id + "'" + lv;
        }
        
    }
}