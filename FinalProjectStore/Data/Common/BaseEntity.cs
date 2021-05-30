
namespace FinalProjectStore.Data.Common
{
    //Ashaghda BaseEntity class-i teqdim olunub. BaseEntity class-i bize muxtelif class-larda eyno property-lari istifade etmek shansini yaradir.
    public abstract class BaseEntity
    {
        public int Number { get; set; }
        public int Quantity { get; set; }
        public int Code { get; set; }

    }
}
