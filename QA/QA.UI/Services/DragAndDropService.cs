namespace QA.UI.Services
{
    public class DragAndDropService
    {
        public object Data { get; set; }
        public int Zone { get; set; }

        public void StartDrag(object data, int zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        public bool Accepts(int zone)
        {
            return this.Zone == zone;
        }
    }
}


//namespace QA.UI.Services
//{
//    public class DragAndDropService
//    {
//        public object Data { get; set; }
//        public int Quantity { get; set; }
//        public int Zone { get; set; }

//        public void StartDrag(object data, int quantity, int zone)
//        {
//            this.Data = data;
//            this.Quantity = quantity;
//            this.Zone = zone;
//        }

//        public bool Accepts(int zone)
//        {
//            return this.Zone == zone;
//        }
//    }
//}

